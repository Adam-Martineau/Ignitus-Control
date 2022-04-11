using System;
using System.IO;
using System.IO.Ports;
using Microsoft.VisualBasic;

namespace Ignitus_Control
{
    public partial class Form1 : Form
    {
        SerialPort _serialPort;
        string _log = "";
        string sessionID = DateTime.Now.ToString().Replace(":", "-").Replace(" ", "_");

        public Form1()
        {
            InitializeComponent();

            //Serial Port Config
            _serialPort = new SerialPort();
            _serialPort.BaudRate = 9600;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            logging(sessionID);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionStatusLabel.Text = "No device connected";
            connectionStatusLabel.ForeColor = Color.Red;
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

        private void dataReceived(object sander, SerialDataReceivedEventArgs e)
        {
            writeLineTextBox("rx", e.ToString());
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
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 3;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter streamWriter = new StreamWriter(myStream);
                    streamWriter.WriteLine(_log);
                    myStream.Close();
                }
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
            string path = $"{sessionID}/log.txt";

            if (!Directory.Exists($"{sessionID}"))
                Directory.CreateDirectory($"{sessionID}");

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
                if (_serialPort.IsOpen)
                {
                    string command = txTextBox.Text;
                    writeLineTextBox("tx", command);
                    _serialPort.Write(command);
                }
                else
                {
                    MessageBox.Show("No device is connected!", "Error!");
                }
            }
        }

        private void emergencyStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeLineTextBox("tx", "emergency_stop");
            _serialPort.Write("emergency_stop");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeLineTextBox("tx", "purge_open");
            _serialPort.Write("purge_open");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeLineTextBox("tx", "purge_close");
            _serialPort.Write("purge_close");
        }

        private void ignitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeLineTextBox("tx", "ignition");
            _serialPort.Write("ignition");
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            writeLineTextBox("tx", "launch");
            _serialPort.Write("launch");
        }
    }
}