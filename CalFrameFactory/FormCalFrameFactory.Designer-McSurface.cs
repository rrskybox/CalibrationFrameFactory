namespace CalFrameFactory
{
    partial class FormCalFrameFactory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OtherExposureBox = new System.Windows.Forms.NumericUpDown();
            this.CheckOther = new System.Windows.Forms.CheckBox();
            this.DarksCountBox = new System.Windows.Forms.NumericUpDown();
            this.Check600 = new System.Windows.Forms.CheckBox();
            this.BiasCountBox = new System.Windows.Forms.NumericUpDown();
            this.Check60 = new System.Windows.Forms.CheckBox();
            this.Check90 = new System.Windows.Forms.CheckBox();
            this.Check120 = new System.Windows.Forms.CheckBox();
            this.CCDTempBox = new System.Windows.Forms.NumericUpDown();
            this.Check180 = new System.Windows.Forms.CheckBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.BinningsBox = new System.Windows.Forms.GroupBox();
            this.binningButton4x4 = new System.Windows.Forms.RadioButton();
            this.binningButton3x3 = new System.Windows.Forms.RadioButton();
            this.binningButton2x2 = new System.Windows.Forms.RadioButton();
            this.binningButton1x1 = new System.Windows.Forms.RadioButton();
            this.StartButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.Check10 = new System.Windows.Forms.CheckBox();
            this.Check30 = new System.Windows.Forms.CheckBox();
            this.Check540 = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Check240 = new System.Windows.Forms.CheckBox();
            this.Check300 = new System.Windows.Forms.CheckBox();
            this.Check360 = new System.Windows.Forms.CheckBox();
            this.Check3 = new System.Windows.Forms.CheckBox();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.Check480 = new System.Windows.Forms.CheckBox();
            this.FlatManManualSetupCheckbox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FlatManExposureNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.PanelCheckBox = new System.Windows.Forms.CheckBox();
            this.DeviceIdLabel = new System.Windows.Forms.Label();
            this.ChooseButton = new System.Windows.Forms.Button();
            this.FlatManBrightnessNum = new System.Windows.Forms.NumericUpDown();
            this.FlatsTargetADU = new System.Windows.Forms.NumericUpDown();
            this.FlatsCountBox = new System.Windows.Forms.NumericUpDown();
            this.FlatsGroup = new System.Windows.Forms.GroupBox();
            this.FlatFilterListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.StayOnTopBox = new System.Windows.Forms.CheckBox();
            this.CreateLibraryButton = new System.Windows.Forms.Button();
            this.LibraryDateSelectionBox = new System.Windows.Forms.DateTimePicker();
            this.ImageFolderButton = new System.Windows.Forms.Button();
            this.ImagePathField = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OtherExposureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DarksCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BiasCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCDTempBox)).BeginInit();
            this.GroupBox4.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.BinningsBox.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManExposureNum)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManBrightnessNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsTargetADU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsCountBox)).BeginInit();
            this.FlatsGroup.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // OtherExposureBox
            // 
            this.OtherExposureBox.Location = new System.Drawing.Point(194, 323);
            this.OtherExposureBox.Margin = new System.Windows.Forms.Padding(6);
            this.OtherExposureBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.OtherExposureBox.Name = "OtherExposureBox";
            this.OtherExposureBox.Size = new System.Drawing.Size(108, 31);
            this.OtherExposureBox.TabIndex = 26;
            this.OtherExposureBox.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // CheckOther
            // 
            this.CheckOther.AutoSize = true;
            this.CheckOther.ForeColor = System.Drawing.Color.White;
            this.CheckOther.Location = new System.Drawing.Point(34, 329);
            this.CheckOther.Margin = new System.Windows.Forms.Padding(6);
            this.CheckOther.Name = "CheckOther";
            this.CheckOther.Size = new System.Drawing.Size(140, 29);
            this.CheckOther.TabIndex = 25;
            this.CheckOther.Text = "Other Sec";
            this.CheckOther.UseVisualStyleBackColor = true;
            this.CheckOther.CheckedChanged += new System.EventHandler(this.CheckOther_CheckedChanged);
            // 
            // DarksCountBox
            // 
            this.DarksCountBox.Location = new System.Drawing.Point(273, 446);
            this.DarksCountBox.Margin = new System.Windows.Forms.Padding(6);
            this.DarksCountBox.Name = "DarksCountBox";
            this.DarksCountBox.Size = new System.Drawing.Size(86, 31);
            this.DarksCountBox.TabIndex = 23;
            this.DarksCountBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.DarksCountBox.ValueChanged += new System.EventHandler(this.DarksCountBox_ValueChanged);
            // 
            // Check600
            // 
            this.Check600.AutoSize = true;
            this.Check600.ForeColor = System.Drawing.Color.White;
            this.Check600.Location = new System.Drawing.Point(170, 279);
            this.Check600.Margin = new System.Windows.Forms.Padding(6);
            this.Check600.Name = "Check600";
            this.Check600.Size = new System.Drawing.Size(123, 29);
            this.Check600.TabIndex = 19;
            this.Check600.Text = "600 Sec";
            this.Check600.UseVisualStyleBackColor = true;
            this.Check600.CheckedChanged += new System.EventHandler(this.Check600_CheckedChanged);
            // 
            // BiasCountBox
            // 
            this.BiasCountBox.Location = new System.Drawing.Point(263, 34);
            this.BiasCountBox.Margin = new System.Windows.Forms.Padding(6);
            this.BiasCountBox.Name = "BiasCountBox";
            this.BiasCountBox.Size = new System.Drawing.Size(86, 31);
            this.BiasCountBox.TabIndex = 23;
            this.BiasCountBox.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.BiasCountBox.ValueChanged += new System.EventHandler(this.BiasCountBox_ValueChanged);
            // 
            // Check60
            // 
            this.Check60.AutoSize = true;
            this.Check60.ForeColor = System.Drawing.Color.White;
            this.Check60.Location = new System.Drawing.Point(38, 198);
            this.Check60.Margin = new System.Windows.Forms.Padding(6);
            this.Check60.Name = "Check60";
            this.Check60.Size = new System.Drawing.Size(111, 29);
            this.Check60.TabIndex = 9;
            this.Check60.Text = "60 Sec";
            this.Check60.UseVisualStyleBackColor = true;
            this.Check60.CheckedChanged += new System.EventHandler(this.Check60_CheckedChanged);
            // 
            // Check90
            // 
            this.Check90.AutoSize = true;
            this.Check90.ForeColor = System.Drawing.Color.White;
            this.Check90.Location = new System.Drawing.Point(38, 238);
            this.Check90.Margin = new System.Windows.Forms.Padding(6);
            this.Check90.Name = "Check90";
            this.Check90.Size = new System.Drawing.Size(111, 29);
            this.Check90.TabIndex = 10;
            this.Check90.Text = "90 Sec";
            this.Check90.UseVisualStyleBackColor = true;
            this.Check90.CheckedChanged += new System.EventHandler(this.Check90_CheckedChanged);
            // 
            // Check120
            // 
            this.Check120.AutoSize = true;
            this.Check120.ForeColor = System.Drawing.Color.White;
            this.Check120.Location = new System.Drawing.Point(38, 279);
            this.Check120.Margin = new System.Windows.Forms.Padding(6);
            this.Check120.Name = "Check120";
            this.Check120.Size = new System.Drawing.Size(123, 29);
            this.Check120.TabIndex = 11;
            this.Check120.Text = "120 Sec";
            this.Check120.UseVisualStyleBackColor = true;
            this.Check120.CheckedChanged += new System.EventHandler(this.Check120_CheckedChanged);
            // 
            // CCDTempBox
            // 
            this.CCDTempBox.Location = new System.Drawing.Point(34, 37);
            this.CCDTempBox.Margin = new System.Windows.Forms.Padding(6);
            this.CCDTempBox.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.CCDTempBox.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.CCDTempBox.Name = "CCDTempBox";
            this.CCDTempBox.Size = new System.Drawing.Size(86, 31);
            this.CCDTempBox.TabIndex = 7;
            this.CCDTempBox.Value = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.CCDTempBox.ValueChanged += new System.EventHandler(this.CCDTempBox_ValueChanged);
            // 
            // Check180
            // 
            this.Check180.AutoSize = true;
            this.Check180.ForeColor = System.Drawing.Color.White;
            this.Check180.Location = new System.Drawing.Point(170, 40);
            this.Check180.Margin = new System.Windows.Forms.Padding(6);
            this.Check180.Name = "Check180";
            this.Check180.Size = new System.Drawing.Size(123, 29);
            this.Check180.TabIndex = 12;
            this.Check180.Text = "180 Sec";
            this.Check180.UseVisualStyleBackColor = true;
            this.Check180.CheckedChanged += new System.EventHandler(this.Check180_CheckedChanged);
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.Cyan;
            this.StatusBox.ForeColor = System.Drawing.Color.Black;
            this.StatusBox.Location = new System.Drawing.Point(34, 760);
            this.StatusBox.Margin = new System.Windows.Forms.Padding(6);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusBox.Size = new System.Drawing.Size(1586, 213);
            this.StatusBox.TabIndex = 42;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.label4);
            this.GroupBox4.Controls.Add(this.BiasCountBox);
            this.GroupBox4.ForeColor = System.Drawing.Color.White;
            this.GroupBox4.Location = new System.Drawing.Point(421, 72);
            this.GroupBox4.Margin = new System.Windows.Forms.Padding(6);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Padding = new System.Windows.Forms.Padding(6);
            this.GroupBox4.Size = new System.Drawing.Size(379, 92);
            this.GroupBox4.TabIndex = 39;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Bias Frames";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.label3);
            this.GroupBox2.Controls.Add(this.GroupBox3);
            this.GroupBox2.Controls.Add(this.DarksCountBox);
            this.GroupBox2.ForeColor = System.Drawing.Color.White;
            this.GroupBox2.Location = new System.Drawing.Point(421, 192);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.GroupBox2.Size = new System.Drawing.Size(379, 507);
            this.GroupBox2.TabIndex = 38;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Dark Frames";
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.CCDTempBox);
            this.GroupBox1.ForeColor = System.Drawing.Color.White;
            this.GroupBox1.Location = new System.Drawing.Point(246, 72);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.GroupBox1.Size = new System.Drawing.Size(149, 92);
            this.GroupBox1.TabIndex = 37;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "CCD Temp";
            // 
            // BinningsBox
            // 
            this.BinningsBox.Controls.Add(this.binningButton4x4);
            this.BinningsBox.Controls.Add(this.binningButton3x3);
            this.BinningsBox.Controls.Add(this.binningButton2x2);
            this.BinningsBox.Controls.Add(this.binningButton1x1);
            this.BinningsBox.ForeColor = System.Drawing.Color.White;
            this.BinningsBox.Location = new System.Drawing.Point(246, 181);
            this.BinningsBox.Margin = new System.Windows.Forms.Padding(6);
            this.BinningsBox.Name = "BinningsBox";
            this.BinningsBox.Padding = new System.Windows.Forms.Padding(6);
            this.BinningsBox.Size = new System.Drawing.Size(149, 240);
            this.BinningsBox.TabIndex = 36;
            this.BinningsBox.TabStop = false;
            this.BinningsBox.Text = "Binnings";
            // 
            // binningButton4x4
            // 
            this.binningButton4x4.AutoSize = true;
            this.binningButton4x4.Location = new System.Drawing.Point(37, 170);
            this.binningButton4x4.Margin = new System.Windows.Forms.Padding(6);
            this.binningButton4x4.Name = "binningButton4x4";
            this.binningButton4x4.Size = new System.Drawing.Size(78, 29);
            this.binningButton4x4.TabIndex = 56;
            this.binningButton4x4.TabStop = true;
            this.binningButton4x4.Text = "4x4";
            this.binningButton4x4.UseVisualStyleBackColor = true;
            this.binningButton4x4.CheckedChanged += new System.EventHandler(this.binningButton4x4_CheckedChanged);
            // 
            // binningButton3x3
            // 
            this.binningButton3x3.AutoSize = true;
            this.binningButton3x3.Location = new System.Drawing.Point(37, 130);
            this.binningButton3x3.Margin = new System.Windows.Forms.Padding(6);
            this.binningButton3x3.Name = "binningButton3x3";
            this.binningButton3x3.Size = new System.Drawing.Size(78, 29);
            this.binningButton3x3.TabIndex = 55;
            this.binningButton3x3.TabStop = true;
            this.binningButton3x3.Text = "3x3";
            this.binningButton3x3.UseVisualStyleBackColor = true;
            this.binningButton3x3.CheckedChanged += new System.EventHandler(this.binningButton3x3_CheckedChanged);
            // 
            // binningButton2x2
            // 
            this.binningButton2x2.AutoSize = true;
            this.binningButton2x2.Location = new System.Drawing.Point(37, 89);
            this.binningButton2x2.Margin = new System.Windows.Forms.Padding(6);
            this.binningButton2x2.Name = "binningButton2x2";
            this.binningButton2x2.Size = new System.Drawing.Size(78, 29);
            this.binningButton2x2.TabIndex = 54;
            this.binningButton2x2.TabStop = true;
            this.binningButton2x2.Text = "2x2";
            this.binningButton2x2.UseVisualStyleBackColor = true;
            this.binningButton2x2.CheckedChanged += new System.EventHandler(this.binningButton2x2_CheckedChanged);
            // 
            // binningButton1x1
            // 
            this.binningButton1x1.AutoSize = true;
            this.binningButton1x1.Checked = true;
            this.binningButton1x1.Location = new System.Drawing.Point(37, 49);
            this.binningButton1x1.Margin = new System.Windows.Forms.Padding(6);
            this.binningButton1x1.Name = "binningButton1x1";
            this.binningButton1x1.Size = new System.Drawing.Size(78, 29);
            this.binningButton1x1.TabIndex = 53;
            this.binningButton1x1.TabStop = true;
            this.binningButton1x1.Text = "1x1";
            this.binningButton1x1.UseVisualStyleBackColor = true;
            this.binningButton1x1.CheckedChanged += new System.EventHandler(this.binningButton1x1_CheckedChanged);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.SpringGreen;
            this.StartButton.ForeColor = System.Drawing.Color.Black;
            this.StartButton.Location = new System.Drawing.Point(23, 214);
            this.StartButton.Margin = new System.Windows.Forms.Padding(6);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(199, 52);
            this.StartButton.TabIndex = 35;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.BackColor = System.Drawing.Color.SpringGreen;
            this.AbortButton.ForeColor = System.Drawing.Color.Black;
            this.AbortButton.Location = new System.Drawing.Point(23, 278);
            this.AbortButton.Margin = new System.Windows.Forms.Padding(6);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(199, 52);
            this.AbortButton.TabIndex = 34;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = false;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.SpringGreen;
            this.CloseButton.ForeColor = System.Drawing.Color.Black;
            this.CloseButton.Location = new System.Drawing.Point(23, 340);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(6);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(199, 52);
            this.CloseButton.TabIndex = 33;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Check10
            // 
            this.Check10.AutoSize = true;
            this.Check10.ForeColor = System.Drawing.Color.White;
            this.Check10.Location = new System.Drawing.Point(38, 119);
            this.Check10.Margin = new System.Windows.Forms.Padding(6);
            this.Check10.Name = "Check10";
            this.Check10.Size = new System.Drawing.Size(111, 29);
            this.Check10.TabIndex = 22;
            this.Check10.Text = "10 Sec";
            this.Check10.UseVisualStyleBackColor = true;
            this.Check10.CheckedChanged += new System.EventHandler(this.Check10_CheckedChanged);
            // 
            // Check30
            // 
            this.Check30.AutoSize = true;
            this.Check30.ForeColor = System.Drawing.Color.White;
            this.Check30.Location = new System.Drawing.Point(38, 160);
            this.Check30.Margin = new System.Windows.Forms.Padding(6);
            this.Check30.Name = "Check30";
            this.Check30.Size = new System.Drawing.Size(111, 29);
            this.Check30.TabIndex = 21;
            this.Check30.Text = "30 Sec";
            this.Check30.UseVisualStyleBackColor = true;
            this.Check30.CheckedChanged += new System.EventHandler(this.Check30_CheckedChanged);
            // 
            // Check540
            // 
            this.Check540.AutoSize = true;
            this.Check540.ForeColor = System.Drawing.Color.White;
            this.Check540.Location = new System.Drawing.Point(170, 238);
            this.Check540.Margin = new System.Windows.Forms.Padding(6);
            this.Check540.Name = "Check540";
            this.Check540.Size = new System.Drawing.Size(123, 29);
            this.Check540.TabIndex = 20;
            this.Check540.Text = "540 Sec";
            this.Check540.UseVisualStyleBackColor = true;
            this.Check540.CheckedChanged += new System.EventHandler(this.Check540_CheckedChanged);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.Check600);
            this.GroupBox3.Controls.Add(this.Check60);
            this.GroupBox3.Controls.Add(this.Check90);
            this.GroupBox3.Controls.Add(this.OtherExposureBox);
            this.GroupBox3.Controls.Add(this.Check120);
            this.GroupBox3.Controls.Add(this.CheckOther);
            this.GroupBox3.Controls.Add(this.Check180);
            this.GroupBox3.Controls.Add(this.Check10);
            this.GroupBox3.Controls.Add(this.Check240);
            this.GroupBox3.Controls.Add(this.Check30);
            this.GroupBox3.Controls.Add(this.Check300);
            this.GroupBox3.Controls.Add(this.Check540);
            this.GroupBox3.Controls.Add(this.Check360);
            this.GroupBox3.Controls.Add(this.Check3);
            this.GroupBox3.Controls.Add(this.Check1);
            this.GroupBox3.Controls.Add(this.Check480);
            this.GroupBox3.ForeColor = System.Drawing.Color.White;
            this.GroupBox3.Location = new System.Drawing.Point(12, 38);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.GroupBox3.Size = new System.Drawing.Size(350, 383);
            this.GroupBox3.TabIndex = 40;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Exposures";
            // 
            // Check240
            // 
            this.Check240.AutoSize = true;
            this.Check240.ForeColor = System.Drawing.Color.White;
            this.Check240.Location = new System.Drawing.Point(170, 75);
            this.Check240.Margin = new System.Windows.Forms.Padding(6);
            this.Check240.Name = "Check240";
            this.Check240.Size = new System.Drawing.Size(123, 29);
            this.Check240.TabIndex = 13;
            this.Check240.Text = "240 Sec";
            this.Check240.UseVisualStyleBackColor = true;
            this.Check240.CheckedChanged += new System.EventHandler(this.Check240_CheckedChanged);
            // 
            // Check300
            // 
            this.Check300.AutoSize = true;
            this.Check300.ForeColor = System.Drawing.Color.White;
            this.Check300.Location = new System.Drawing.Point(170, 115);
            this.Check300.Margin = new System.Windows.Forms.Padding(6);
            this.Check300.Name = "Check300";
            this.Check300.Size = new System.Drawing.Size(123, 29);
            this.Check300.TabIndex = 14;
            this.Check300.Text = "300 Sec";
            this.Check300.UseVisualStyleBackColor = true;
            this.Check300.CheckedChanged += new System.EventHandler(this.Check300_CheckedChanged);
            // 
            // Check360
            // 
            this.Check360.AutoSize = true;
            this.Check360.ForeColor = System.Drawing.Color.White;
            this.Check360.Location = new System.Drawing.Point(170, 156);
            this.Check360.Margin = new System.Windows.Forms.Padding(6);
            this.Check360.Name = "Check360";
            this.Check360.Size = new System.Drawing.Size(123, 29);
            this.Check360.TabIndex = 15;
            this.Check360.Text = "360 Sec";
            this.Check360.UseVisualStyleBackColor = true;
            this.Check360.CheckedChanged += new System.EventHandler(this.Check360_CheckedChanged);
            // 
            // Check3
            // 
            this.Check3.AutoSize = true;
            this.Check3.ForeColor = System.Drawing.Color.White;
            this.Check3.Location = new System.Drawing.Point(38, 79);
            this.Check3.Margin = new System.Windows.Forms.Padding(6);
            this.Check3.Name = "Check3";
            this.Check3.Size = new System.Drawing.Size(99, 29);
            this.Check3.TabIndex = 16;
            this.Check3.Text = "3 Sec";
            this.Check3.UseVisualStyleBackColor = true;
            this.Check3.CheckedChanged += new System.EventHandler(this.Check3_CheckedChanged);
            // 
            // Check1
            // 
            this.Check1.AutoSize = true;
            this.Check1.ForeColor = System.Drawing.Color.White;
            this.Check1.Location = new System.Drawing.Point(38, 40);
            this.Check1.Margin = new System.Windows.Forms.Padding(6);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(99, 29);
            this.Check1.TabIndex = 18;
            this.Check1.Text = "1 Sec";
            this.Check1.UseVisualStyleBackColor = true;
            this.Check1.CheckedChanged += new System.EventHandler(this.Check1_CheckedChanged);
            // 
            // Check480
            // 
            this.Check480.AutoSize = true;
            this.Check480.ForeColor = System.Drawing.Color.White;
            this.Check480.Location = new System.Drawing.Point(170, 198);
            this.Check480.Margin = new System.Windows.Forms.Padding(6);
            this.Check480.Name = "Check480";
            this.Check480.Size = new System.Drawing.Size(123, 29);
            this.Check480.TabIndex = 17;
            this.Check480.Text = "480 Sec";
            this.Check480.UseVisualStyleBackColor = true;
            this.Check480.CheckedChanged += new System.EventHandler(this.Check480_CheckedChanged);
            // 
            // FlatManManualSetupCheckbox
            // 
            this.FlatManManualSetupCheckbox.AutoSize = true;
            this.FlatManManualSetupCheckbox.Location = new System.Drawing.Point(17, 73);
            this.FlatManManualSetupCheckbox.Margin = new System.Windows.Forms.Padding(6);
            this.FlatManManualSetupCheckbox.Name = "FlatManManualSetupCheckbox";
            this.FlatManManualSetupCheckbox.Size = new System.Drawing.Size(115, 29);
            this.FlatManManualSetupCheckbox.TabIndex = 46;
            this.FlatManManualSetupCheckbox.Text = "Manual";
            this.FlatManManualSetupCheckbox.UseVisualStyleBackColor = true;
            this.FlatManManualSetupCheckbox.CheckedChanged += new System.EventHandler(this.FlatManManualSetupCheckbox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(206, 40);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "Target ADU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(192, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 41;
            this.label1.Text = "Initial Exposure";
            // 
            // FlatManExposureNum
            // 
            this.FlatManExposureNum.DecimalPlaces = 1;
            this.FlatManExposureNum.Location = new System.Drawing.Point(354, 94);
            this.FlatManExposureNum.Margin = new System.Windows.Forms.Padding(6);
            this.FlatManExposureNum.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.FlatManExposureNum.Name = "FlatManExposureNum";
            this.FlatManExposureNum.Size = new System.Drawing.Size(100, 31);
            this.FlatManExposureNum.TabIndex = 40;
            this.FlatManExposureNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatManExposureNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.FlatManExposureNum.ValueChanged += new System.EventHandler(this.FlatManExposureNum_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(255, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 37;
            this.label2.Text = "Brightness";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.FlatManManualSetupCheckbox);
            this.groupBox6.Controls.Add(this.DeviceIdLabel);
            this.groupBox6.Controls.Add(this.ChooseButton);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.FlatManBrightnessNum);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(12, 192);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox6.Size = new System.Drawing.Size(538, 179);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Source";
            // 
            // PanelCheckBox
            // 
            this.PanelCheckBox.AutoSize = true;
            this.PanelCheckBox.Location = new System.Drawing.Point(45, 96);
            this.PanelCheckBox.Name = "PanelCheckBox";
            this.PanelCheckBox.Size = new System.Drawing.Size(99, 29);
            this.PanelCheckBox.TabIndex = 58;
            this.PanelCheckBox.Text = "Panel";
            this.PanelCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeviceIdLabel
            // 
            this.DeviceIdLabel.AutoSize = true;
            this.DeviceIdLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceIdLabel.Location = new System.Drawing.Point(194, 30);
            this.DeviceIdLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.DeviceIdLabel.Name = "DeviceIdLabel";
            this.DeviceIdLabel.Size = new System.Drawing.Size(191, 25);
            this.DeviceIdLabel.TabIndex = 2;
            this.DeviceIdLabel.Text = "ASCOM Device Id";
            // 
            // ChooseButton
            // 
            this.ChooseButton.BackColor = System.Drawing.Color.SpringGreen;
            this.ChooseButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseButton.ForeColor = System.Drawing.Color.Black;
            this.ChooseButton.Location = new System.Drawing.Point(412, 19);
            this.ChooseButton.Margin = new System.Windows.Forms.Padding(6);
            this.ChooseButton.Name = "ChooseButton";
            this.ChooseButton.Size = new System.Drawing.Size(114, 44);
            this.ChooseButton.TabIndex = 1;
            this.ChooseButton.Text = "Choose";
            this.ChooseButton.UseVisualStyleBackColor = false;
            this.ChooseButton.Click += new System.EventHandler(this.ChooseButton_Click);
            // 
            // FlatManBrightnessNum
            // 
            this.FlatManBrightnessNum.Location = new System.Drawing.Point(375, 115);
            this.FlatManBrightnessNum.Margin = new System.Windows.Forms.Padding(6);
            this.FlatManBrightnessNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FlatManBrightnessNum.Name = "FlatManBrightnessNum";
            this.FlatManBrightnessNum.Size = new System.Drawing.Size(100, 31);
            this.FlatManBrightnessNum.TabIndex = 36;
            this.FlatManBrightnessNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatManBrightnessNum.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.FlatManBrightnessNum.ValueChanged += new System.EventHandler(this.FlatManBrightnessNum_ValueChanged);
            // 
            // FlatsTargetADU
            // 
            this.FlatsTargetADU.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FlatsTargetADU.Location = new System.Drawing.Point(346, 37);
            this.FlatsTargetADU.Margin = new System.Windows.Forms.Padding(6);
            this.FlatsTargetADU.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FlatsTargetADU.Name = "FlatsTargetADU";
            this.FlatsTargetADU.Size = new System.Drawing.Size(108, 31);
            this.FlatsTargetADU.TabIndex = 39;
            this.FlatsTargetADU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FlatsTargetADU.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.FlatsTargetADU.ValueChanged += new System.EventHandler(this.FlatsTargetADU_ValueChanged);
            // 
            // FlatsCountBox
            // 
            this.FlatsCountBox.ForeColor = System.Drawing.Color.Black;
            this.FlatsCountBox.Location = new System.Drawing.Point(60, 37);
            this.FlatsCountBox.Margin = new System.Windows.Forms.Padding(6);
            this.FlatsCountBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.FlatsCountBox.Name = "FlatsCountBox";
            this.FlatsCountBox.Size = new System.Drawing.Size(84, 31);
            this.FlatsCountBox.TabIndex = 28;
            this.FlatsCountBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FlatsCountBox.ValueChanged += new System.EventHandler(this.FlatsCountBox_ValueChanged);
            // 
            // FlatsGroup
            // 
            this.FlatsGroup.Controls.Add(this.FlatFilterListBox);
            this.FlatsGroup.Controls.Add(this.groupBox6);
            this.FlatsGroup.ForeColor = System.Drawing.Color.White;
            this.FlatsGroup.Location = new System.Drawing.Point(873, 349);
            this.FlatsGroup.Margin = new System.Windows.Forms.Padding(6);
            this.FlatsGroup.Name = "FlatsGroup";
            this.FlatsGroup.Padding = new System.Windows.Forms.Padding(6);
            this.FlatsGroup.Size = new System.Drawing.Size(566, 383);
            this.FlatsGroup.TabIndex = 43;
            this.FlatsGroup.TabStop = false;
            this.FlatsGroup.Text = "Flat Frames";
            // 
            // FlatFilterListBox
            // 
            this.FlatFilterListBox.FormattingEnabled = true;
            this.FlatFilterListBox.Location = new System.Drawing.Point(20, 36);
            this.FlatFilterListBox.Margin = new System.Windows.Forms.Padding(6);
            this.FlatFilterListBox.Name = "FlatFilterListBox";
            this.FlatFilterListBox.ScrollAlwaysVisible = true;
            this.FlatFilterListBox.Size = new System.Drawing.Size(534, 144);
            this.FlatFilterListBox.TabIndex = 94;
            this.FlatFilterListBox.SelectedIndexChanged += new System.EventHandler(this.FlatFilterListBox_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.PanelCheckBox);
            this.groupBox8.Controls.Add(this.FlatsCountBox);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.FlatsTargetADU);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.FlatManExposureNum);
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(828, 85);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox8.Size = new System.Drawing.Size(566, 181);
            this.groupBox8.TabIndex = 39;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Flat Frames";
            // 
            // StayOnTopBox
            // 
            this.StayOnTopBox.AutoSize = true;
            this.StayOnTopBox.Location = new System.Drawing.Point(27, 85);
            this.StayOnTopBox.Margin = new System.Windows.Forms.Padding(6);
            this.StayOnTopBox.Name = "StayOnTopBox";
            this.StayOnTopBox.Size = new System.Drawing.Size(164, 29);
            this.StayOnTopBox.TabIndex = 53;
            this.StayOnTopBox.Text = "Stay On Top";
            this.StayOnTopBox.UseVisualStyleBackColor = true;
            this.StayOnTopBox.CheckedChanged += new System.EventHandler(this.StayOnTopBox_CheckedChanged);
            // 
            // CreateLibraryButton
            // 
            this.CreateLibraryButton.BackColor = System.Drawing.Color.SpringGreen;
            this.CreateLibraryButton.ForeColor = System.Drawing.Color.Black;
            this.CreateLibraryButton.Location = new System.Drawing.Point(23, 479);
            this.CreateLibraryButton.Margin = new System.Windows.Forms.Padding(6);
            this.CreateLibraryButton.Name = "CreateLibraryButton";
            this.CreateLibraryButton.Size = new System.Drawing.Size(199, 57);
            this.CreateLibraryButton.TabIndex = 54;
            this.CreateLibraryButton.Text = "Create Library";
            this.CreateLibraryButton.UseVisualStyleBackColor = false;
            this.CreateLibraryButton.Click += new System.EventHandler(this.CreateLibraryButton_Click);
            // 
            // LibraryDateSelectionBox
            // 
            this.LibraryDateSelectionBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.LibraryDateSelectionBox.Location = new System.Drawing.Point(34, 143);
            this.LibraryDateSelectionBox.Margin = new System.Windows.Forms.Padding(6);
            this.LibraryDateSelectionBox.Name = "LibraryDateSelectionBox";
            this.LibraryDateSelectionBox.Size = new System.Drawing.Size(188, 31);
            this.LibraryDateSelectionBox.TabIndex = 55;
            // 
            // ImageFolderButton
            // 
            this.ImageFolderButton.BackColor = System.Drawing.Color.SpringGreen;
            this.ImageFolderButton.ForeColor = System.Drawing.Color.Black;
            this.ImageFolderButton.Location = new System.Drawing.Point(24, 22);
            this.ImageFolderButton.Margin = new System.Windows.Forms.Padding(6);
            this.ImageFolderButton.Name = "ImageFolderButton";
            this.ImageFolderButton.Size = new System.Drawing.Size(198, 41);
            this.ImageFolderButton.TabIndex = 56;
            this.ImageFolderButton.Text = "Choose";
            this.ImageFolderButton.UseVisualStyleBackColor = false;
            this.ImageFolderButton.Click += new System.EventHandler(this.ImageFolderButton_Click);
            // 
            // ImagePathField
            // 
            this.ImagePathField.AutoSize = true;
            this.ImagePathField.BackColor = System.Drawing.Color.White;
            this.ImagePathField.ForeColor = System.Drawing.Color.Black;
            this.ImagePathField.Location = new System.Drawing.Point(250, 27);
            this.ImagePathField.MinimumSize = new System.Drawing.Size(1350, 31);
            this.ImagePathField.Name = "ImagePathField";
            this.ImagePathField.Size = new System.Drawing.Size(1350, 31);
            this.ImagePathField.TabIndex = 57;
            this.ImagePathField.Text = "Image Path ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 446);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 25);
            this.label3.TabIndex = 41;
            this.label3.Text = "Frames per Exposure";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(27, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 42;
            this.label4.Text = "Frames";
            // 
            // FormCalFrameFactory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(1626, 988);
            this.Controls.Add(this.ImagePathField);
            this.Controls.Add(this.ImageFolderButton);
            this.Controls.Add(this.LibraryDateSelectionBox);
            this.Controls.Add(this.CreateLibraryButton);
            this.Controls.Add(this.StayOnTopBox);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.FlatsGroup);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.BinningsBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.CloseButton);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormCalFrameFactory";
            this.Text = "Calibration Frame Factory";
            ((System.ComponentModel.ISupportInitialize)(this.OtherExposureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DarksCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BiasCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCDTempBox)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.BinningsBox.ResumeLayout(false);
            this.BinningsBox.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManExposureNum)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlatManBrightnessNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsTargetADU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlatsCountBox)).EndInit();
            this.FlatsGroup.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.NumericUpDown OtherExposureBox;
        internal System.Windows.Forms.CheckBox CheckOther;
        internal System.Windows.Forms.NumericUpDown DarksCountBox;
        internal System.Windows.Forms.CheckBox Check600;
        internal System.Windows.Forms.NumericUpDown BiasCountBox;
        internal System.Windows.Forms.CheckBox Check60;
        internal System.Windows.Forms.CheckBox Check90;
        internal System.Windows.Forms.CheckBox Check120;
        internal System.Windows.Forms.NumericUpDown CCDTempBox;
        internal System.Windows.Forms.CheckBox Check180;
        internal System.Windows.Forms.TextBox StatusBox;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox BinningsBox;
        internal System.Windows.Forms.Button StartButton;
        internal System.Windows.Forms.Button AbortButton;
        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.CheckBox Check10;
        internal System.Windows.Forms.CheckBox Check30;
        internal System.Windows.Forms.CheckBox Check540;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.CheckBox Check240;
        internal System.Windows.Forms.CheckBox Check300;
        internal System.Windows.Forms.CheckBox Check360;
        internal System.Windows.Forms.CheckBox Check3;
        internal System.Windows.Forms.CheckBox Check1;
        internal System.Windows.Forms.CheckBox Check480;
        private System.Windows.Forms.CheckBox FlatManManualSetupCheckbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown FlatManExposureNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown FlatManBrightnessNum;
        private System.Windows.Forms.NumericUpDown FlatsTargetADU;
        internal System.Windows.Forms.NumericUpDown FlatsCountBox;
        internal System.Windows.Forms.GroupBox FlatsGroup;
        internal System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton binningButton4x4;
        private System.Windows.Forms.RadioButton binningButton3x3;
        private System.Windows.Forms.RadioButton binningButton2x2;
        private System.Windows.Forms.RadioButton binningButton1x1;
        private System.Windows.Forms.CheckBox StayOnTopBox;
        internal System.Windows.Forms.Button CreateLibraryButton;
        private System.Windows.Forms.DateTimePicker LibraryDateSelectionBox;
        private System.Windows.Forms.Button ChooseButton;
        private System.Windows.Forms.CheckedListBox FlatFilterListBox;
        private System.Windows.Forms.Label DeviceIdLabel;
        internal System.Windows.Forms.Button ImageFolderButton;
        private System.Windows.Forms.Label ImagePathField;
        private System.Windows.Forms.CheckBox PanelCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

