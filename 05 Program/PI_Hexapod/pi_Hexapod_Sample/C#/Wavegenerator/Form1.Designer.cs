namespace Wavegenerator
{
    partial class MainWindow
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

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.IDNDisplay = new System.Windows.Forms.Label();
            this.lblCST_q = new System.Windows.Forms.Label();
            this.lblSVO_q = new System.Windows.Forms.Label();
            this.btnFRF = new System.Windows.Forms.Button();
            this.btnMOV = new System.Windows.Forms.Button();
            this.txtWGC = new System.Windows.Forms.TextBox();
            this.btnWAV = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStartWG = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(138, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect Controller";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // IDNDisplay
            // 
            this.IDNDisplay.AutoSize = true;
            this.IDNDisplay.Location = new System.Drawing.Point(8, 61);
            this.IDNDisplay.Name = "IDNDisplay";
            this.IDNDisplay.Size = new System.Drawing.Size(136, 13);
            this.IDNDisplay.TabIndex = 7;
            this.IDNDisplay.Text = "no controller connected yet";
            // 
            // lblCST_q
            // 
            this.lblCST_q.Location = new System.Drawing.Point(0, 0);
            this.lblCST_q.Name = "lblCST_q";
            this.lblCST_q.Size = new System.Drawing.Size(100, 23);
            this.lblCST_q.TabIndex = 33;
            // 
            // lblSVO_q
            // 
            this.lblSVO_q.Location = new System.Drawing.Point(0, 0);
            this.lblSVO_q.Name = "lblSVO_q";
            this.lblSVO_q.Size = new System.Drawing.Size(100, 23);
            this.lblSVO_q.TabIndex = 32;
            // 
            // btnFRF
            // 
            this.btnFRF.Enabled = false;
            this.btnFRF.Location = new System.Drawing.Point(4, 17);
            this.btnFRF.Name = "btnFRF";
            this.btnFRF.Size = new System.Drawing.Size(138, 23);
            this.btnFRF.TabIndex = 19;
            this.btnFRF.Text = "Reference";
            this.btnFRF.UseVisualStyleBackColor = true;
            this.btnFRF.Click += new System.EventHandler(this.btnFRF_Click);
            // 
            // btnMOV
            // 
            this.btnMOV.Enabled = false;
            this.btnMOV.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnMOV.Location = new System.Drawing.Point(15, 279);
            this.btnMOV.Name = "btnMOV";
            this.btnMOV.Size = new System.Drawing.Size(407, 23);
            this.btnMOV.TabIndex = 22;
            this.btnMOV.Text = "Move to start position";
            this.btnMOV.UseVisualStyleBackColor = true;
            this.btnMOV.Click += new System.EventHandler(this.btnMOV_Click);
            // 
            // txtWGC
            // 
            this.txtWGC.Location = new System.Drawing.Point(234, 233);
            this.txtWGC.Name = "txtWGC";
            this.txtWGC.Size = new System.Drawing.Size(188, 20);
            this.txtWGC.TabIndex = 23;
            this.txtWGC.Text = "5";
            // 
            // btnWAV
            // 
            this.btnWAV.Enabled = false;
            this.btnWAV.Location = new System.Drawing.Point(7, 15);
            this.btnWAV.Name = "btnWAV";
            this.btnWAV.Size = new System.Drawing.Size(404, 23);
            this.btnWAV.TabIndex = 28;
            this.btnWAV.Text = "Define sine waveform (circle)";
            this.btnWAV.UseVisualStyleBackColor = true;
            this.btnWAV.Click += new System.EventHandler(this.btnWAV_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Number of repetitions:";
            // 
            // btnStartWG
            // 
            this.btnStartWG.Enabled = false;
            this.btnStartWG.Location = new System.Drawing.Point(15, 332);
            this.btnStartWG.Name = "btnStartWG";
            this.btnStartWG.Size = new System.Drawing.Size(407, 23);
            this.btnStartWG.TabIndex = 29;
            this.btnStartWG.Text = "Start wavegenerator";
            this.btnStartWG.UseVisualStyleBackColor = true;
            this.btnStartWG.Click += new System.EventHandler(this.btnStartWG_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(331, 382);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(88, 23);
            this.btnQuit.TabIndex = 31;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "not referenced";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Status:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IDNDisplay);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(11, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 90);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWAV);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 200);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnFRF);
            this.groupBox3.Location = new System.Drawing.Point(13, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 50);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 418);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnStartWG);
            this.Controls.Add(this.txtWGC);
            this.Controls.Add(this.btnMOV);
            this.Controls.Add(this.lblSVO_q);
            this.Controls.Add(this.lblCST_q);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainWindow";
            this.Text = "Wavegenerator sample";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label IDNDisplay;
        private System.Windows.Forms.Label lblCST_q;
        private System.Windows.Forms.Label lblSVO_q;
        private System.Windows.Forms.Button btnFRF;
        private System.Windows.Forms.Button btnMOV;
        private System.Windows.Forms.TextBox txtWGC;
        private System.Windows.Forms.Button btnWAV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStartWG;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
    }
}

