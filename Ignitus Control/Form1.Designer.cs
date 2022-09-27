namespace Ignitus_Control
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emergencyStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xBeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterATModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.immediateSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refuelingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txTextBox = new System.Windows.Forms.TextBox();
            this.TxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tankPressure = new System.Windows.Forms.Label();
            this.tankTemperature = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.engineTemperature = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.enginePressure = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 973);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1552, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 15);
            this.connectionStatusLabel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.connectDeviceToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1552, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(62, 29);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(181, 34);
            this.saveAsToolStripMenuItem.Text = "Save as..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // connectDeviceToolStripMenuItem
            // 
            this.connectDeviceToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.connectDeviceToolStripMenuItem.Name = "connectDeviceToolStripMenuItem";
            this.connectDeviceToolStripMenuItem.Size = new System.Drawing.Size(150, 29);
            this.connectDeviceToolStripMenuItem.Text = "Connect Device";
            this.connectDeviceToolStripMenuItem.DropDownOpening += new System.EventHandler(this.connectDeviceToolStripMenuItem_DropDownOpening);
            this.connectDeviceToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.connectDeviceToolStripMenuItem_DropDownItemClicked);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emergencyStopToolStripMenuItem,
            this.xBeeToolStripMenuItem,
            this.refuelingToolStripMenuItem,
            this.launchToolStripMenuItem,
            this.ignitionToolStripMenuItem});
            this.commandsToolStripMenuItem.Enabled = false;
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(120, 29);
            this.commandsToolStripMenuItem.Text = "Commands";
            // 
            // emergencyStopToolStripMenuItem
            // 
            this.emergencyStopToolStripMenuItem.Name = "emergencyStopToolStripMenuItem";
            this.emergencyStopToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.emergencyStopToolStripMenuItem.Text = "Emergency Stop";
            this.emergencyStopToolStripMenuItem.Click += new System.EventHandler(this.emergencyStopToolStripMenuItem_Click);
            // 
            // xBeeToolStripMenuItem
            // 
            this.xBeeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterATModeToolStripMenuItem,
            this.immediateSampleToolStripMenuItem});
            this.xBeeToolStripMenuItem.Name = "xBeeToolStripMenuItem";
            this.xBeeToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.xBeeToolStripMenuItem.Text = "XBee";
            // 
            // enterATModeToolStripMenuItem
            // 
            this.enterATModeToolStripMenuItem.Name = "enterATModeToolStripMenuItem";
            this.enterATModeToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.enterATModeToolStripMenuItem.Text = "Enter AT Mode";
            this.enterATModeToolStripMenuItem.Click += new System.EventHandler(this.enterATMode);
            // 
            // immediateSampleToolStripMenuItem
            // 
            this.immediateSampleToolStripMenuItem.Name = "immediateSampleToolStripMenuItem";
            this.immediateSampleToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.immediateSampleToolStripMenuItem.Text = "Immediate Sample";
            this.immediateSampleToolStripMenuItem.Click += new System.EventHandler(this.immediateSample);
            // 
            // refuelingToolStripMenuItem
            // 
            this.refuelingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.refuelingToolStripMenuItem.Name = "refuelingToolStripMenuItem";
            this.refuelingToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.refuelingToolStripMenuItem.Text = "Purge valve";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openPusgeValve);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closePusgeValve);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.closeToolStripMenuItem1});
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.launchToolStripMenuItem.Text = "Main valve";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(158, 34);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openMainValve);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(158, 34);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeMainValve);
            // 
            // ignitionToolStripMenuItem
            // 
            this.ignitionToolStripMenuItem.Name = "ignitionToolStripMenuItem";
            this.ignitionToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.ignitionToolStripMenuItem.Text = "Ignition";
            this.ignitionToolStripMenuItem.Click += new System.EventHandler(this.ignitionTool);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureSerialToolStripMenuItem,
            this.toolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // configureSerialToolStripMenuItem
            // 
            this.configureSerialToolStripMenuItem.Name = "configureSerialToolStripMenuItem";
            this.configureSerialToolStripMenuItem.Size = new System.Drawing.Size(239, 34);
            this.configureSerialToolStripMenuItem.Text = "Configure Serial";
            this.configureSerialToolStripMenuItem.Click += new System.EventHandler(this.configureSerialToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(239, 34);
            this.toolStripMenuItem1.Text = "Info";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.infoToolStripMenu_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.engineTemperature);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.enginePressure);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.tankTemperature);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.tankPressure);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.RxRichTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.TxRichTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1552, 940);
            this.splitContainer1.SplitterDistance = 989;
            this.splitContainer1.TabIndex = 2;
            // 
            // RxRichTextBox
            // 
            this.RxRichTextBox.Enabled = false;
            this.RxRichTextBox.Location = new System.Drawing.Point(9, 28);
            this.RxRichTextBox.Name = "RxRichTextBox";
            this.RxRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RxRichTextBox.Size = new System.Drawing.Size(977, 706);
            this.RxRichTextBox.TabIndex = 1;
            this.RxRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "RECEPTION";
            // 
            // txTextBox
            // 
            this.txTextBox.Location = new System.Drawing.Point(6, 906);
            this.txTextBox.Name = "txTextBox";
            this.txTextBox.PlaceholderText = "SEND";
            this.txTextBox.Size = new System.Drawing.Size(550, 31);
            this.txTextBox.TabIndex = 2;
            this.txTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txTextBoxKeyDown);
            // 
            // TxRichTextBox
            // 
            this.TxRichTextBox.Enabled = false;
            this.TxRichTextBox.Location = new System.Drawing.Point(6, 28);
            this.TxRichTextBox.Name = "TxRichTextBox";
            this.TxRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.TxRichTextBox.Size = new System.Drawing.Size(550, 872);
            this.TxRichTextBox.TabIndex = 1;
            this.TxRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "TRANSMITION";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 746);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 54);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tank Press.";
            // 
            // tankPressure
            // 
            this.tankPressure.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tankPressure.Location = new System.Drawing.Point(232, 746);
            this.tankPressure.Name = "tankPressure";
            this.tankPressure.Size = new System.Drawing.Size(97, 54);
            this.tankPressure.TabIndex = 3;
            this.tankPressure.Text = "0.0";
            // 
            // tankTemperature
            // 
            this.tankTemperature.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tankTemperature.Location = new System.Drawing.Point(232, 800);
            this.tankTemperature.Name = "tankTemperature";
            this.tankTemperature.Size = new System.Drawing.Size(97, 54);
            this.tankTemperature.TabIndex = 5;
            this.tankTemperature.Text = "0.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(12, 800);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 54);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tank Temp.";
            // 
            // engineTemperature
            // 
            this.engineTemperature.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.engineTemperature.Location = new System.Drawing.Point(619, 800);
            this.engineTemperature.Name = "engineTemperature";
            this.engineTemperature.Size = new System.Drawing.Size(97, 54);
            this.engineTemperature.TabIndex = 9;
            this.engineTemperature.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(358, 800);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(260, 54);
            this.label6.TabIndex = 8;
            this.label6.Text = "Engine Temp.";
            // 
            // enginePressure
            // 
            this.enginePressure.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.enginePressure.Location = new System.Drawing.Point(619, 746);
            this.enginePressure.Name = "enginePressure";
            this.enginePressure.Size = new System.Drawing.Size(97, 54);
            this.enginePressure.TabIndex = 7;
            this.enginePressure.Text = "0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(358, 746);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(255, 54);
            this.label8.TabIndex = 6;
            this.label8.Text = "Engine Press.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 995);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Ignitus Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel connectionStatusLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem filesToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem connectDeviceToolStripMenuItem;
        private ToolStripMenuItem commandsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem emergencyStopToolStripMenuItem;
        private ToolStripMenuItem launchToolStripMenuItem;
        private ToolStripMenuItem refuelingToolStripMenuItem;
        private SplitContainer splitContainer1;
        private Label label1;
        private Label label2;
        private RichTextBox RxRichTextBox;
        private RichTextBox TxRichTextBox;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem configureSerialToolStripMenuItem;
        private TextBox txTextBox;
        private ToolStripMenuItem ignitionToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem xBeeToolStripMenuItem;
        private ToolStripMenuItem immediateSampleToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem1;
        private ToolStripMenuItem enterATModeToolStripMenuItem;
        public Label engineTemperature;
        private Label label6;
        public Label enginePressure;
        private Label label8;
        public Label tankTemperature;
        private Label label5;
        public Label tankPressure;
        private Label label3;
    }
}