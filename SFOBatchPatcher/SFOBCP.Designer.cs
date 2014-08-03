namespace SFOBatchCategoryPatcher
{
    partial class SFOBCP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFOBCP));
            this.comboSFO = new System.Windows.Forms.ComboBox();
            this.buttonBC = new System.Windows.Forms.Button();
            this.progressBarSFOBCP = new System.Windows.Forms.ProgressBar();
            this.labelVer = new System.Windows.Forms.Label();
            this.labelPF2 = new System.Windows.Forms.Label();
            this.folderKeys = new System.Windows.Forms.FolderBrowserDialog();
            this.labelPF1 = new System.Windows.Forms.Label();
            this.checkVerbose = new System.Windows.Forms.CheckBox();
            this.consoleControl = new ConsoleControl.ConsoleControl();
            this.SuspendLayout();
            // 
            // comboSFO
            // 
            this.comboSFO.AllowDrop = true;
            this.comboSFO.FormattingEnabled = true;
            this.comboSFO.Items.AddRange(new object[] {
            "HG - HDD Game",
            "DG - Disc Game",
            "X0 - PCE (PC Engine)",
            "X4 - Neo Geo",
            "AP - App Photo",
            "AM - App Music",
            "AV - App Video",
            "BV - Broadcast Video",
            "AT - App TV",
            "WT - Web TV",
            "CB - Cell BE",
            "HM - Home",
            "SF - Store Frontend",
            "2G - PS2 Game",
            "2P - PS2 PSN",
            "1P - PS1 PSN",
            "MN - PSP Minis",
            "PE - PSP Emulator",
            "PP - PSP",
            "2D - PS2 Data"});
            this.comboSFO.Location = new System.Drawing.Point(15, 31);
            this.comboSFO.Name = "comboSFO";
            this.comboSFO.Size = new System.Drawing.Size(151, 21);
            this.comboSFO.TabIndex = 0;
            this.comboSFO.Text = "Target";
            this.comboSFO.SelectedIndexChanged += new System.EventHandler(this.comboSFO_SelectedIndexChanged);
            // 
            // buttonBC
            // 
            this.buttonBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBC.Location = new System.Drawing.Point(172, 17);
            this.buttonBC.Name = "buttonBC";
            this.buttonBC.Size = new System.Drawing.Size(317, 48);
            this.buttonBC.TabIndex = 1;
            this.buttonBC.Text = "Batch Convert";
            this.buttonBC.UseVisualStyleBackColor = true;
            this.buttonBC.Click += new System.EventHandler(this.buttonBC_Click);
            // 
            // progressBarSFOBCP
            // 
            this.progressBarSFOBCP.Location = new System.Drawing.Point(12, 83);
            this.progressBarSFOBCP.Name = "progressBarSFOBCP";
            this.progressBarSFOBCP.Size = new System.Drawing.Size(477, 28);
            this.progressBarSFOBCP.TabIndex = 2;
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.ForeColor = System.Drawing.Color.Blue;
            this.labelVer.Location = new System.Drawing.Point(12, 9);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(31, 13);
            this.labelVer.TabIndex = 3;
            this.labelVer.Text = "v.1.0";
            // 
            // labelPF2
            // 
            this.labelPF2.AutoSize = true;
            this.labelPF2.ForeColor = System.Drawing.Color.Blue;
            this.labelPF2.Location = new System.Drawing.Point(12, 68);
            this.labelPF2.Name = "labelPF2";
            this.labelPF2.Size = new System.Drawing.Size(0, 13);
            this.labelPF2.TabIndex = 4;
            // 
            // labelPF1
            // 
            this.labelPF1.AutoSize = true;
            this.labelPF1.ForeColor = System.Drawing.Color.Blue;
            this.labelPF1.Location = new System.Drawing.Point(12, 53);
            this.labelPF1.Name = "labelPF1";
            this.labelPF1.Size = new System.Drawing.Size(0, 13);
            this.labelPF1.TabIndex = 5;
            // 
            // checkVerbose
            // 
            this.checkVerbose.AutoSize = true;
            this.checkVerbose.Location = new System.Drawing.Point(108, 8);
            this.checkVerbose.Name = "checkVerbose";
            this.checkVerbose.Size = new System.Drawing.Size(65, 17);
            this.checkVerbose.TabIndex = 6;
            this.checkVerbose.Text = "Verbose";
            this.checkVerbose.UseVisualStyleBackColor = true;
            this.checkVerbose.CheckedChanged += new System.EventHandler(this.checkVerbose_CheckedChanged);
            // 
            // consoleControl
            // 
            this.consoleControl.IsInputEnabled = true;
            this.consoleControl.IsLogEnabled = false;
            this.consoleControl.Location = new System.Drawing.Point(12, 117);
            this.consoleControl.Name = "consoleControl";
            this.consoleControl.SendKeyboardCommandsToProcess = false;
            this.consoleControl.ShowDiagnostics = false;
            this.consoleControl.Size = new System.Drawing.Size(477, 339);
            this.consoleControl.TabIndex = 7;
            // 
            // SFOBCP
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 468);
            this.Controls.Add(this.consoleControl);
            this.Controls.Add(this.checkVerbose);
            this.Controls.Add(this.labelPF1);
            this.Controls.Add(this.labelPF2);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.progressBarSFOBCP);
            this.Controls.Add(this.buttonBC);
            this.Controls.Add(this.comboSFO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SFOBCP";
            this.Text = "SFO BCP";
            this.Load += new System.EventHandler(this.SFOBCP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboSFO;
        private System.Windows.Forms.Button buttonBC;
        private System.Windows.Forms.ProgressBar progressBarSFOBCP;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.Label labelPF2;
        private System.Windows.Forms.FolderBrowserDialog folderKeys;
        private System.Windows.Forms.Label labelPF1;
        private System.Windows.Forms.CheckBox checkVerbose;
        private ConsoleControl.ConsoleControl consoleControl;
    }
}

