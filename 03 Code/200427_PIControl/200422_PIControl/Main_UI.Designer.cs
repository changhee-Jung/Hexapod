namespace PI_Hexapod
{
    partial class Main_UI
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDialog = new System.Windows.Forms.TextBox();
            this.txtErrorLog = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHomming = new System.Windows.Forms.Button();
            this.btnZeroPosition = new System.Windows.Forms.Button();
            this.txtCmdPosition = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAvailableAxis = new System.Windows.Forms.ComboBox();
            this.btnMoveAxis = new System.Windows.Forms.Button();
            this.btnPIInterface = new System.Windows.Forms.Button();
            this.btnRotation = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPosition_X = new System.Windows.Forms.Label();
            this.lblPosition_Y = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPosition_Z = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPosition_U = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPosition_V = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPosition_W = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDialog
            // 
            this.txtDialog.Location = new System.Drawing.Point(12, 94);
            this.txtDialog.Multiline = true;
            this.txtDialog.Name = "txtDialog";
            this.txtDialog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDialog.Size = new System.Drawing.Size(421, 290);
            this.txtDialog.TabIndex = 0;
            // 
            // txtErrorLog
            // 
            this.txtErrorLog.Location = new System.Drawing.Point(12, 29);
            this.txtErrorLog.Multiline = true;
            this.txtErrorLog.Name = "txtErrorLog";
            this.txtErrorLog.Size = new System.Drawing.Size(421, 47);
            this.txtErrorLog.TabIndex = 1;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(12, 391);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(251, 21);
            this.txtSend.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(358, 389);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Error Log";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Dialog";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHomming);
            this.groupBox1.Controls.Add(this.btnZeroPosition);
            this.groupBox1.Controls.Add(this.txtCmdPosition);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboAvailableAxis);
            this.groupBox1.Controls.Add(this.btnMoveAxis);
            this.groupBox1.Location = new System.Drawing.Point(455, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 108);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Axis";
            // 
            // btnHomming
            // 
            this.btnHomming.Location = new System.Drawing.Point(10, 48);
            this.btnHomming.Name = "btnHomming";
            this.btnHomming.Size = new System.Drawing.Size(79, 23);
            this.btnHomming.TabIndex = 11;
            this.btnHomming.Text = "Homming";
            this.btnHomming.UseVisualStyleBackColor = true;
            this.btnHomming.Click += new System.EventHandler(this.btnHomming_Click);
            // 
            // btnZeroPosition
            // 
            this.btnZeroPosition.Location = new System.Drawing.Point(92, 47);
            this.btnZeroPosition.Name = "btnZeroPosition";
            this.btnZeroPosition.Size = new System.Drawing.Size(79, 23);
            this.btnZeroPosition.TabIndex = 12;
            this.btnZeroPosition.Text = "Origin";
            this.btnZeroPosition.UseVisualStyleBackColor = true;
            this.btnZeroPosition.Click += new System.EventHandler(this.btnZeroPosition_Click);
            // 
            // txtCmdPosition
            // 
            this.txtCmdPosition.Location = new System.Drawing.Point(15, 77);
            this.txtCmdPosition.Name = "txtCmdPosition";
            this.txtCmdPosition.Size = new System.Drawing.Size(71, 21);
            this.txtCmdPosition.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Axis :";
            // 
            // cboAvailableAxis
            // 
            this.cboAvailableAxis.FormattingEnabled = true;
            this.cboAvailableAxis.Location = new System.Drawing.Point(57, 21);
            this.cboAvailableAxis.Name = "cboAvailableAxis";
            this.cboAvailableAxis.Size = new System.Drawing.Size(114, 20);
            this.cboAvailableAxis.TabIndex = 10;
            // 
            // btnMoveAxis
            // 
            this.btnMoveAxis.Location = new System.Drawing.Point(92, 76);
            this.btnMoveAxis.Name = "btnMoveAxis";
            this.btnMoveAxis.Size = new System.Drawing.Size(79, 23);
            this.btnMoveAxis.TabIndex = 9;
            this.btnMoveAxis.Text = "Move";
            this.btnMoveAxis.UseVisualStyleBackColor = true;
            this.btnMoveAxis.Click += new System.EventHandler(this.btnMoveAxis_Click);
            // 
            // btnPIInterface
            // 
            this.btnPIInterface.Location = new System.Drawing.Point(469, 29);
            this.btnPIInterface.Name = "btnPIInterface";
            this.btnPIInterface.Size = new System.Drawing.Size(157, 47);
            this.btnPIInterface.TabIndex = 11;
            this.btnPIInterface.Text = "PI Connect";
            this.btnPIInterface.UseVisualStyleBackColor = true;
            this.btnPIInterface.Click += new System.EventHandler(this.btnPIInterface_Click);
            // 
            // btnRotation
            // 
            this.btnRotation.Location = new System.Drawing.Point(470, 207);
            this.btnRotation.Name = "btnRotation";
            this.btnRotation.Size = new System.Drawing.Size(157, 47);
            this.btnRotation.TabIndex = 15;
            this.btnRotation.Text = "Rotate Start";
            this.btnRotation.UseVisualStyleBackColor = true;
            this.btnRotation.Click += new System.EventHandler(this.btnRotation_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "X :";
            // 
            // lblPosition_X
            // 
            this.lblPosition_X.AutoSize = true;
            this.lblPosition_X.Location = new System.Drawing.Point(495, 273);
            this.lblPosition_X.Name = "lblPosition_X";
            this.lblPosition_X.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_X.TabIndex = 17;
            this.lblPosition_X.Text = "0";
            // 
            // lblPosition_Y
            // 
            this.lblPosition_Y.AutoSize = true;
            this.lblPosition_Y.Location = new System.Drawing.Point(495, 296);
            this.lblPosition_Y.Name = "lblPosition_Y";
            this.lblPosition_Y.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_Y.TabIndex = 19;
            this.lblPosition_Y.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(468, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "Y :";
            // 
            // lblPosition_Z
            // 
            this.lblPosition_Z.AutoSize = true;
            this.lblPosition_Z.Location = new System.Drawing.Point(495, 320);
            this.lblPosition_Z.Name = "lblPosition_Z";
            this.lblPosition_Z.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_Z.TabIndex = 21;
            this.lblPosition_Z.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(468, 320);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "Z :";
            // 
            // lblPosition_U
            // 
            this.lblPosition_U.AutoSize = true;
            this.lblPosition_U.Location = new System.Drawing.Point(495, 344);
            this.lblPosition_U.Name = "lblPosition_U";
            this.lblPosition_U.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_U.TabIndex = 23;
            this.lblPosition_U.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(468, 344);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "U :";
            // 
            // lblPosition_V
            // 
            this.lblPosition_V.AutoSize = true;
            this.lblPosition_V.Location = new System.Drawing.Point(495, 369);
            this.lblPosition_V.Name = "lblPosition_V";
            this.lblPosition_V.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_V.TabIndex = 25;
            this.lblPosition_V.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(468, 369);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 12);
            this.label12.TabIndex = 24;
            this.label12.Text = "V :";
            // 
            // lblPosition_W
            // 
            this.lblPosition_W.AutoSize = true;
            this.lblPosition_W.Location = new System.Drawing.Point(495, 393);
            this.lblPosition_W.Name = "lblPosition_W";
            this.lblPosition_W.Size = new System.Drawing.Size(11, 12);
            this.lblPosition_W.TabIndex = 27;
            this.lblPosition_W.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(468, 393);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "W :";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(282, 389);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 28;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(530, 430);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(11, 12);
            this.lblBufferSize.TabIndex = 30;
            this.lblBufferSize.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(468, 430);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "Buffer :";
            // 
            // Main_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 457);
            this.Controls.Add(this.lblBufferSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblPosition_W);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblPosition_V);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblPosition_U);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblPosition_Z);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPosition_Y);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblPosition_X);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRotation);
            this.Controls.Add(this.btnPIInterface);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtErrorLog);
            this.Controls.Add(this.txtDialog);
            this.Name = "Main_UI";
            this.Text = "Hexapod";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtErrorLog;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveAxis;
        private System.Windows.Forms.ComboBox cboAvailableAxis;
        private System.Windows.Forms.Button btnPIInterface;
        private System.Windows.Forms.Button btnHomming;
        private System.Windows.Forms.Button btnZeroPosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCmdPosition;
        private System.Windows.Forms.Button btnRotation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPosition_X;
        private System.Windows.Forms.Label lblPosition_Y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPosition_Z;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPosition_U;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPosition_V;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPosition_W;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txtDialog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label lblBufferSize;
    }
}

