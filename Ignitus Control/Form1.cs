using System;
using System.IO;
using System.IO.Ports;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace Ignitus_Control
{
	public partial class Form1 : Form
	{
        private readonly string _json_marker = "This is a json marker, the following text is a json.";
        public readonly string _sessionID = Guid.NewGuid().ToString();

        public SerialPort _serialPort;
		public Thread _checkSafetySwitchThread;

		public delegate void writeToSerialDeletage(String message);
		public writeToSerialDeletage _writeToSerialDeletage;
		public delegate void AddDataDelegate(String myString);
		public AddDataDelegate _addDataDelegate;

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

		public Form1()
		{
			InitializeComponent();

			//Serial Port Config
			_serialPort = new SerialPort();
			_serialPort.BaudRate = 9600;
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

			_writeToSerialDeletage = new writeToSerialDeletage(writeToSerial);
			_addDataDelegate = new AddDataDelegate(AddDataMethod);
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
			string goToATMode = "+++";
			string ATIS = "ATIS \r";

			while (true)
			{
                Thread.Sleep(2000);
				if (_serialPort.IsOpen)
				{
					_serialPort.Write(goToATMode);
                    Thread.Sleep(1000);
                    _serialPort.Write(ATIS);
					Thread.Sleep(1000);
                }
			}
		}

		public void AddDataMethod(String myString)
		{
			RxRichTextBox.Text +=  myString;
		}

		private void dataReceived(object sander, SerialDataReceivedEventArgs e)
		{
			SerialPort sp = (SerialPort) sander;
			string s = sp.ReadExisting();

			if (s.Contains("01\r0002\r00\r"))
			{
				updateSafetySwitchStatus(s);
			}
			else if (s.Contains(_json_marker))
			{
				RxRichTextBox.Invoke(this._addDataDelegate, new Object[] { s });
				updateData(s.Replace(_json_marker, "")); 
			}
		}

		private void updateData(string s)
		{
			Data? data = JsonSerializer.Deserialize<Data>(s);

			if (data != null)
				updateUiData(data.tankPressure, 
							 data.tankTemperature, 
							 data.enginePressure, 
							 data.engineTemperature, 
							 data.isBattery, 
							 data.isRocketArmed, 
							 data.isIgnited);
        }

		public void updateSafetySwitchStatus(string immediateSample)
		{
			if (immediateSample == "01\r0002\r00\r0000")
			{
				controlArmedLabel.ForeColor = Color.Red;
				controlArmedLabel.Text = "Control Armed";
				_isControlArmed = true;
			}
			else //if (immediateSample == "01\r0002\r00\r0002")
			{
				controlArmedLabel.ForeColor = Color.Green;
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

				if (connectDeviceToolStripMenuItem.DropDownItems.Count == 0)
				{
                    RxRichTextBox.Text += ("No serial devices detected. \n\r");
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
				connectionStatusLabel.ForeColor = Color.Green;
				commandsToolStripMenuItem.Enabled = true;
                RxRichTextBox.Text += ("Serial connection opened on: " + _serialPort.PortName.ToString() + "\n\r");;
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
				File.WriteAllTextAsync(saveFileDialog.FileName, RxRichTextBox.Text);
			}
		}

		private void logging(Data data)
		{
			//auto save to local temp folder
			string path = $"{_sessionID}/log.csv";

			if (!Directory.Exists($"{_sessionID}"))
				Directory.CreateDirectory($"{_sessionID}");

			string line = DateTime.Now.ToString() + "; " +
						  data.tankPressure.ToString() + "; " +
                          data.tankTemperature.ToString() + "; " +
                          data.enginePressure.ToString() + "; " +
                          data.engineTemperature.ToString() + "; " +
                          data.isBattery.ToString() + "; " +
                          data.isRocketArmed.ToString() + "; " +
                          _isControlArmed.ToString() + "; " +
                          data.isIgnited.ToString() + "; \n\r";

            if (File.Exists(path))
			{
				using (StreamWriter writer = File.CreateText(path))
				{
					string header = "Timestamp; " +
									"Tank Pressure; " +
									"Tank Temperature; " +
									"Engine Pressure; " +
									"Engine Temperature; " +
									"Is Ignition Batterie Good; " +
									"Is Rocket Armed; " +
									"Is Control Armed; " +
									"Is Ignition On; \n\r";

					writer.WriteLine(header + data);
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
                    TxRichTextBox.Text += (message + "\n\r"); ;
                    _serialPort.Write(message);
				}
				else
				{
					MessageBox.Show("No device is connected!", "Error!");
				}
			}
        }

		private void updateUiData(double tankPress, double tankTemp, double enginePress, double engineTemp, bool battery, bool rocketArmed, bool ignited)
		{
			tankPressureLabel.Text = tankPress.ToString();
			tankTemperatureLabel.Text = tankTemp.ToString();
			enginePressureLabel.Text = enginePress.ToString();
			engineTemperatureLabel.Text = engineTemp.ToString();

			if (battery)
				batteryLabel.ForeColor = Color.Green;
			else 
				batteryLabel.ForeColor = Color.Red;

			if (rocketArmed)
			{
				rocketArmedLabel.ForeColor = Color.Red;
				rocketArmedLabel.Text = "Rocket Armed";
			}
			else
			{
                rocketArmedLabel.ForeColor = Color.Green;
                rocketArmedLabel.Text = "Rocket Not Armed";
            }

			if (ignited)
			{
				ignitedLabel.ForeColor = Color.Red;
				ignitedLabel.Text = "Ignited";
			}
			else
			{
				ignitedLabel.ForeColor = Color.Green;
				ignitedLabel.Text = "Not Ignited";
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
                    TxRichTextBox.Text += (command + "\n\r"); ;
                    _serialPort.Write(command);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}