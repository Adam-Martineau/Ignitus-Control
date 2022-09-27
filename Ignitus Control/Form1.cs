using System;
using System.IO;
using System.IO.Ports;
using Microsoft.VisualBasic;

namespace Ignitus_Control
{
	public partial class Form1 : Form
	{
        public string _sessionID = DateTime.Now.ToString().Replace(":", "-").Replace(" ", "_");

        public SerialPort _serialPort;
		public string _log = "";
		public Thread _checkSafetySwitchThread;

		public delegate void writeToSerialDeletage(String message);
		public writeToSerialDeletage _writeToSerialDeletage;
		public delegate void AddDataDelegate(String myString);
		public AddDataDelegate _addDataDelegate;

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

			logging(_sessionID);

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

			RxRichTextBox.Invoke(this._addDataDelegate, new Object[] { s });
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
					writeLineTextBox("rx", "No serial devices detected.");
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
				writeLineTextBox("rx", "Serial connection opened on: " + _serialPort.PortName.ToString());
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

		private void writeLineTextBox(string textBox, string s)
		{
			logging(s);

			if (textBox.ToLower() == "rx")
				RxRichTextBox.Text += "<" + DateTime.Now.TimeOfDay.ToString() + "> " + s + "\n\r";
			else if (textBox.ToLower() == "tx")
				TxRichTextBox.Text += "<" + DateTime.Now.TimeOfDay.ToString() + "> " + s + "\n\r";
		}

		private void logging(string s)
		{
			_log += s + "\n\r";

			//auto save to local temp folder
			string path = $"{_sessionID}/log.txt";

			if (!Directory.Exists($"{_sessionID}"))
				Directory.CreateDirectory($"{_sessionID}");

			if (File.Exists(path))
			{
				using (StreamWriter writer = File.CreateText(path))
				{
					writer.WriteLine(s);
				}
			}
			else
			{
				using (StreamWriter writer = File.AppendText(path))
				{
					writer.WriteLine(s);
				}
			}
		}

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

		private void txTextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
                string command = txTextBox.Text;
				txTextBox.Text = "";

                if (_serialPort.IsOpen)
				{
					writeLineTextBox("tx", command);
					_serialPort.Write(command);
				}
				else
				{
					MessageBox.Show("No device is connected!", "Error!");
				}
			}
		}

		private void writeToSerial(String message)
		{
			lock (this)
			{
				if (_serialPort.IsOpen)
				{
					writeLineTextBox("tx", message);
					_serialPort.Write(message);
				}
				else
				{
					MessageBox.Show("No device is connected!", "Error!");
				}
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
	}
}