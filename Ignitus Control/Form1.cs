using System;
using System.IO;
using System.IO.Ports;
using Microsoft.VisualBasic;
using System.Text.Json;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Ignitus_Control
{
	public partial class Form : System.Windows.Forms.Form
	{
		private string _serial_start = "_JSONHEADER_";
		private string _serial_stop = "_JSONFOOTER_";

        public readonly string _sessionID = Guid.NewGuid().ToString();

        public SerialPort _serialPort;
		public Thread _checkSafetySwitchThread;

		public delegate void writeToSerialDeletage(String message);
		public writeToSerialDeletage _writeToSerialDeletage;

		bool _isControlArmed = false;

		enum Commands
		{
			emergency_stop,
			open_purge_valve,
			close_purge_valve,
			open_main_valve,
			close_main_valve,
			ignition
        }

		public Form()
		{
			InitializeComponent();

			//Serial Port Config
			_serialPort = new SerialPort();
			_serialPort.BaudRate = 9600;
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(comIndicator);


            _writeToSerialDeletage = new writeToSerialDeletage(writeToSerial);
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			connectionStatusLabel.Text = "No device connected";
			connectionStatusLabel.ForeColor = Color.Red;
			_checkSafetySwitchThread = new Thread(new ThreadStart(checkSafetySwitch));
			_checkSafetySwitchThread.Start();
		}
		
		public void checkSafetySwitch()
		{
			string command_ATMode = "+++";
			string command_ATIS = "ATIS \r";
            string command_exit_ATMode = "ATCN \r";
            string command_get_data = "data";

			while (true)
			{
				if (_serialPort.IsOpen)
				{
					/*
                    // Getting the state of I/O from the xbee
                    _serialPort.Write(command_ATMode);

                    while (_serialPort.BytesToRead < 3)
					{
						Thread.Sleep(15);
					}

                    _serialPort.Write(command_ATIS);

                    while (_serialPort.BytesToRead < 17)
					{
						Thread.Sleep(15);
					}

                    BeginInvoke((MethodInvoker)delegate () { updateSafetySwitchStatus(_serialPort.ReadExisting()); });

                    _serialPort.Write(command_exit_ATMode);

                    while (_serialPort.BytesToRead < 3)
                    {
                        Thread.Sleep(15);
                    }
					*/

                    Thread.Sleep(1);
                    _serialPort.DiscardInBuffer();

                    // Getting the data from the rocket
                    string buffer = "";

                    string s = _serialPort.ReadExisting();

                    _serialPort.Write(command_get_data);


					int wait = 0;
					while (!buffer.Contains(_serial_stop) && wait < 200)
					{
						if (_serialPort.BytesToRead > 0)
						{
							buffer += _serialPort.ReadExisting();
                            _serialPort.DiscardInBuffer();
                        }

						wait++;
                        Thread.Sleep(1);
                    }

                    _serialPort.DiscardInBuffer();

                    BeginInvoke((MethodInvoker)delegate () { updateData(buffer); });

                    Thread.Sleep(100);
                }
			}
		}

		private void comIndicator(object sender, SerialDataReceivedEventArgs handler)
		{
			if (comLight.ForeColor == Color.DarkRed)
				comLight.ForeColor = Color.Red;
			else
				comLight.ForeColor = Color.DarkRed;
		}

		private void updateData(string s)
		{
			string json = s.Replace(_serial_start, "");
			json = json.Replace(_serial_stop, "");

			system_data? data = null;

            try
			{
				data = JsonSerializer.Deserialize<system_data>(json);
            }
			catch
			{
				Console.WriteLine(s);
			}
			
			if (data != null)
			{
				data = calibrateData(data);
				updateUiData(data);
				logging(data);
            }
        }

		private system_data calibrateData(system_data data)
		{
			if (data.tank1_pressure < 100)
				data.tank1_pressure = 0;
			else
				data.tank1_pressure = data.tank1_pressure - 100;

            if (data.tank2_pressure < 100)
                data.tank2_pressure = 0;
            else
                data.tank2_pressure = data.tank2_pressure - 100;

			return data;
        }

		public void updateSafetySwitchStatus(string immediateSample)
		{
			if (immediateSample.Contains("01\r0002\r00\r0000"))
			{
				controlArmedLabel.ForeColor = Color.Red;
				controlArmedLabel.Text = "Control Armed";
				_isControlArmed = true;
			}
			else //if (immediateSample == "01\r0002\r00\r0002")
			{
				controlArmedLabel.ForeColor = Color.LimeGreen;
				controlArmedLabel.Text = "Control Not Armed";
				_isControlArmed = false;
			}
        }

		private void connectDeviceToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			if (!_serialPort.IsOpen)
			{
				//Scan serial device and fill the menu with 'em
				connectDeviceToolStripMenuItem.DropDownItems.Clear();

				// Get a list of serial port names.
				foreach (string s in SerialPort.GetPortNames())
				{
					connectDeviceToolStripMenuItem.DropDownItems.Add(s);
				}
			}
			else
			{
				connectDeviceToolStripMenuItem.Text = "Connect Device";
				connectionStatusLabel.Text = "No device connected";
				connectionStatusLabel.ForeColor = Color.Red;
				commandsToolStripMenuItem.Enabled = false;
				_serialPort.Close();
			}
		}

		private void connectDeviceToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			_serialPort.PortName = e.ClickedItem.Text;

			try
			{
				//Starting the serial connection
				_serialPort.Open();
				connectDeviceToolStripMenuItem.DropDownItems.Clear();
				connectDeviceToolStripMenuItem.Text = "Disconnect Device";
				connectionStatusLabel.Text = "Device connected";
				connectionStatusLabel.ForeColor = Color.LimeGreen;
				commandsToolStripMenuItem.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void configureSerialToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = Interaction.InputBox("BaudRate:", "Serial Config");
			int br;

			if (Int32.TryParse(s, out br))
			{
				_serialPort.BaudRate = br;
			}
			else
			{
				MessageBox.Show("Input must be a number!", "Error");
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 3;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				// ToDo: save the contant of the temp file to save location
			}
		}

		private void logging(system_data data)
		{
			//auto save to local temp folder
			string path = $"{_sessionID}.csv";

			if (!Directory.Exists($"{_sessionID}"))
				Directory.CreateDirectory($"{_sessionID}");

			string line = DateTime.Now.ToString() + "; " +
						  data.tank1_pressure.ToString() + "; " +
                          data.tank1_temp.ToString() + "; " +
                          data.tank2_pressure.ToString() + "; " +
                          data.tank2_temp.ToString() + "; " +
                          data.battery.ToString() + "; " +
                          data.armed.ToString() + "; " +
                          _isControlArmed.ToString() + "; " +
                          data.ignition.ToString() + ";";

            if (File.Exists(path))
			{
				using (StreamWriter writer = File.CreateText(path))
				{
					string header = "Timestamp; " +
									"Tank1 Pressure; " +
									"Tank1 Temperature; " +
									"Tank2 Pressure; " +
									"Tank2 Temperature; " +
									"Is Ignition Batterie Good; " +
									"Is Rocket Armed; " +
									"Is Control Armed; " +
									"Is Ignition On;";

					writer.WriteLine(header);
					writer.WriteLine(line);
				}
			}
			else
			{
				using (StreamWriter writer = File.AppendText(path))
				{
					writer.WriteLine(data);
				}
			}
		}

		private void writeToSerial(String message)
		{
			lock (this)
			{
				if (_serialPort.IsOpen)
				{
					if (TxRichTextBox.InvokeRequired)
					{
                        TxRichTextBox.BeginInvoke((MethodInvoker) delegate() { TxRichTextBox.Text += message + "\n\r"; });
                        _serialPort.Write(message);
                    }
					else
					{
                        TxRichTextBox.Text += (message + "\n\r"); ;
                        _serialPort.Write(message);
                    }
				}
				else
				{
					MessageBox.Show("No device is connected!", "Error!");
				}
			}
        }

		private void updateUiData(system_data data)
		{
			tankPressureLabel.Text = data.tank1_pressure.ToString();
			tankTemperatureLabel.Text = data.tank1_temp.ToString();
			tank2PressureLabel.Text = data.tank2_pressure.ToString();
			tank2TemperatureLabel.Text = data.tank2_temp.ToString();
			powerLabel.Text = data.power_power.ToString();
			voltageLabel.Text = data.power_voltage.ToString();
			currentLabel.Text = data.power_current.ToString();

			updateBatteryLabel(data.battery);
			updateArmedLabel(data.armed);
            updateIgnitionLabel(data.ignition);
            updateMainValveClosedLabel(data.main_valve);
			updatePurgeValveLabel(data.purge_valve);
        }

		private void updateBatteryLabel(bool state)
		{
            if (state)
                batteryLabel.ForeColor = Color.LimeGreen;
            else
                batteryLabel.ForeColor = Color.Red;
        }

		private void updateArmedLabel(bool state)
		{
            if (state)
            {
                rocketArmedLabel.ForeColor = Color.Red;
                rocketArmedLabel.Text = "Rocket Armed";
            }
            else
            {
                rocketArmedLabel.ForeColor = Color.LimeGreen;
                rocketArmedLabel.Text = "Rocket Not Armed";
            }
        }

		private void updateIgnitionLabel(bool state)
		{
            if (state)
            {
                ignitedLabel.ForeColor = Color.Red;
                ignitedLabel.Text = "Ignited";
            }
            else
            {
                ignitedLabel.ForeColor = Color.LimeGreen;
                ignitedLabel.Text = "Not Ignited";
            }
        }

		private void updateMainValveClosedLabel(bool state)
		{
			if (state)
			{
				mainValveLabel.ForeColor = Color.Red;
				mainValveLabel.Text = "Main Valve Open";
			}
			else
			{
				mainValveLabel.ForeColor = Color.LimeGreen;

			}
		}

        private void updatePurgeValveLabel(bool state)
		{
            if (state)
            {
                purgeValveLabel.ForeColor = Color.Red;
                purgeValveLabel.Text = "Purge Valve Closed";
            }
            else
            {
                purgeValveLabel.ForeColor = Color.LimeGreen;
                purgeValveLabel.Text = "Purge Valve Open";
            }
        }

		/// ALL THE COMMAND DROP DOWN MENU METHOD
		private void emergencyStopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			writeToSerial(Commands.emergency_stop.ToString());
        }

		private void openPusgeValve(object sender, EventArgs e)
		{
            writeToSerial(Commands.open_purge_valve.ToString());
        }

		private void closePusgeValve(object sender, EventArgs e)
		{
            writeToSerial(Commands.close_purge_valve.ToString());
        }

        private void openMainValve(object sender, EventArgs e)
        {
            writeToSerial(Commands.open_main_valve.ToString());
        }

        private void closeMainValve(object sender, EventArgs e)
        {
            writeToSerial(Commands.close_main_valve.ToString());
        }

        private void ignitionTool(object sender, EventArgs e)
		{
            writeToSerial(Commands.ignition.ToString());
        }

        private void enterATMode(object sender, EventArgs e)
        {
            writeToSerial("+++");
        }

        private void immediateSample(object sender, EventArgs e)
		{
			writeToSerial("ATIS \n\r");
		}

		/// <summary>
		/// Event to send a commend using the text box
		/// </summary>
        private void txTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = txTextBox.Text;
                txTextBox.Text = "";

                if (_serialPort.IsOpen)
                {
                    writeToSerial(command);
                }
                else
                {
                    MessageBox.Show("No device is connected!", "Error!");
                }
            }
        }

		/// <summary>
		/// Event to display the info panel
		/// </summary>
        private void infoToolStripMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: Beta 0.1 \n\r" +
                            "By Adam Martineau from UdeS" +
                            "\n\r https://github.com/Adam-Martineau/Ignitus-Control"
                            , "Info");
        }

		/// <summary>
		/// Close it all down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}