using System;
using System.IO.Ports;

namespace Ignitus_Control
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionStatusLabel.Text = "No device connected";
            connectionStatusLabel.ForeColor = Color.Red;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void connectDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Scan serial device and fill the menu with 'em
            
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}