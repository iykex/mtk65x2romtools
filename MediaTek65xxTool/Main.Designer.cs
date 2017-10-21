namespace MediaTek65xxTool
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.baseRomFile = new System.Windows.Forms.OpenFileDialog();
            this.portRomFile = new System.Windows.Forms.OpenFileDialog();
            this.portZipButton = new System.Windows.Forms.Button();
            this.stockZipButton = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.mtButton1 = new System.Windows.Forms.RadioButton();
            this.mtButton2 = new System.Windows.Forms.RadioButton();
            this.mtButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.mtButton4 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseRomFile
            // 
            this.baseRomFile.FileName = "base rom.zip";
            this.baseRomFile.Filter = "Flashable ROM|*.zip";
            // 
            // portRomFile
            // 
            this.portRomFile.FileName = "zip to port.zip";
            this.portRomFile.Filter = "Flashable ROM|*.zip";
            // 
            // portZipButton
            // 
            this.portZipButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.portZipButton.Location = new System.Drawing.Point(158, 12);
            this.portZipButton.Name = "portZipButton";
            this.portZipButton.Size = new System.Drawing.Size(241, 25);
            this.portZipButton.TabIndex = 0;
            this.portZipButton.Text = "New ROM Zip";
            this.portZipButton.UseVisualStyleBackColor = true;
            this.portZipButton.Click += new System.EventHandler(this.portZipButton_Click);
            // 
            // stockZipButton
            // 
            this.stockZipButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stockZipButton.Location = new System.Drawing.Point(158, 35);
            this.stockZipButton.Name = "stockZipButton";
            this.stockZipButton.Size = new System.Drawing.Size(241, 26);
            this.stockZipButton.TabIndex = 1;
            this.stockZipButton.Text = "Stock/Ported ROM Zip";
            this.stockZipButton.UseVisualStyleBackColor = true;
            this.stockZipButton.Click += new System.EventHandler(this.stockZipButton_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(12, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Android 4.4.x";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 35);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(86, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Android 5.x.x";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 58);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Android 6.x-7.x";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(12, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(387, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Do it!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mtButton1
            // 
            this.mtButton1.AutoSize = true;
            this.mtButton1.Location = new System.Drawing.Point(7, 26);
            this.mtButton1.Name = "mtButton1";
            this.mtButton1.Size = new System.Drawing.Size(138, 17);
            this.mtButton1.TabIndex = 6;
            this.mtButton1.Text = "MediaTek 6572 Chipset";
            this.mtButton1.UseVisualStyleBackColor = true;
            this.mtButton1.CheckedChanged += new System.EventHandler(this.mtButton1_CheckedChanged);
            // 
            // mtButton2
            // 
            this.mtButton2.AutoSize = true;
            this.mtButton2.Location = new System.Drawing.Point(7, 49);
            this.mtButton2.Name = "mtButton2";
            this.mtButton2.Size = new System.Drawing.Size(138, 17);
            this.mtButton2.TabIndex = 7;
            this.mtButton2.Text = "MediaTek 6582 Chipset";
            this.mtButton2.UseVisualStyleBackColor = true;
            this.mtButton2.CheckedChanged += new System.EventHandler(this.mtButton2_CheckedChanged);
            // 
            // mtButton3
            // 
            this.mtButton3.AutoSize = true;
            this.mtButton3.Location = new System.Drawing.Point(7, 72);
            this.mtButton3.Name = "mtButton3";
            this.mtButton3.Size = new System.Drawing.Size(138, 17);
            this.mtButton3.TabIndex = 8;
            this.mtButton3.Text = "MediaTek 6592 Chipset";
            this.mtButton3.UseVisualStyleBackColor = true;
            this.mtButton3.CheckedChanged += new System.EventHandler(this.mtButton3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.mtButton4);
            this.groupBox1.Controls.Add(this.mtButton2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mtButton3);
            this.groupBox1.Controls.Add(this.mtButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 147);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chipset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Made by Swoopae \r\n      (Octav Adrian)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 284);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(387, 35);
            this.progressBar1.TabIndex = 11;
            // 
            // mtButton4
            // 
            this.mtButton4.AutoSize = true;
            this.mtButton4.Location = new System.Drawing.Point(7, 95);
            this.mtButton4.Name = "mtButton4";
            this.mtButton4.Size = new System.Drawing.Size(143, 17);
            this.mtButton4.TabIndex = 11;
            this.mtButton4.Text = "Custom (name in textbox)";
            this.mtButton4.UseVisualStyleBackColor = true;
            this.mtButton4.CheckedChanged += new System.EventHandler(this.mtButton4_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 116);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(374, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Status: Idle";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 331);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.stockZipButton);
            this.Controls.Add(this.portZipButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "MediaTek 65xx ROM Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog baseRomFile;
        private System.Windows.Forms.OpenFileDialog portRomFile;
        private System.Windows.Forms.Button portZipButton;
        private System.Windows.Forms.Button stockZipButton;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton mtButton1;
        private System.Windows.Forms.RadioButton mtButton2;
        private System.Windows.Forms.RadioButton mtButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton mtButton4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}

