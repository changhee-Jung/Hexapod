namespace _200406_hexapod
{
    partial class Hexapod_View
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInput_X = new System.Windows.Forms.TextBox();
            this.txtInput_Y = new System.Windows.Forms.TextBox();
            this.txtInput_Z = new System.Windows.Forms.TextBox();
            this.txtInput_Roll = new System.Windows.Forms.TextBox();
            this.txtInput_Yaw = new System.Windows.Forms.TextBox();
            this.txtInput_Pitch = new System.Windows.Forms.TextBox();
            this.btnCalculate_InverseKinematics = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOutput_L6 = new System.Windows.Forms.TextBox();
            this.txtOutput_L5 = new System.Windows.Forms.TextBox();
            this.txtOutput_L4 = new System.Windows.Forms.TextBox();
            this.txtOutput_L3 = new System.Windows.Forms.TextBox();
            this.txtOutput_L2 = new System.Windows.Forms.TextBox();
            this.txtOutput_L1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInput_X
            // 
            this.txtInput_X.Location = new System.Drawing.Point(67, 29);
            this.txtInput_X.Name = "txtInput_X";
            this.txtInput_X.Size = new System.Drawing.Size(113, 21);
            this.txtInput_X.TabIndex = 0;
            // 
            // txtInput_Y
            // 
            this.txtInput_Y.Location = new System.Drawing.Point(67, 78);
            this.txtInput_Y.Name = "txtInput_Y";
            this.txtInput_Y.Size = new System.Drawing.Size(113, 21);
            this.txtInput_Y.TabIndex = 1;
            // 
            // txtInput_Z
            // 
            this.txtInput_Z.Location = new System.Drawing.Point(67, 127);
            this.txtInput_Z.Name = "txtInput_Z";
            this.txtInput_Z.Size = new System.Drawing.Size(113, 21);
            this.txtInput_Z.TabIndex = 2;
            // 
            // txtInput_Roll
            // 
            this.txtInput_Roll.Location = new System.Drawing.Point(67, 176);
            this.txtInput_Roll.Name = "txtInput_Roll";
            this.txtInput_Roll.Size = new System.Drawing.Size(113, 21);
            this.txtInput_Roll.TabIndex = 3;
            // 
            // txtInput_Yaw
            // 
            this.txtInput_Yaw.Location = new System.Drawing.Point(67, 273);
            this.txtInput_Yaw.Name = "txtInput_Yaw";
            this.txtInput_Yaw.Size = new System.Drawing.Size(113, 21);
            this.txtInput_Yaw.TabIndex = 5;
            // 
            // txtInput_Pitch
            // 
            this.txtInput_Pitch.Location = new System.Drawing.Point(67, 224);
            this.txtInput_Pitch.Name = "txtInput_Pitch";
            this.txtInput_Pitch.Size = new System.Drawing.Size(113, 21);
            this.txtInput_Pitch.TabIndex = 4;
            // 
            // btnCalculate_InverseKinematics
            // 
            this.btnCalculate_InverseKinematics.Location = new System.Drawing.Point(148, 311);
            this.btnCalculate_InverseKinematics.Name = "btnCalculate_InverseKinematics";
            this.btnCalculate_InverseKinematics.Size = new System.Drawing.Size(113, 51);
            this.btnCalculate_InverseKinematics.TabIndex = 6;
            this.btnCalculate_InverseKinematics.Text = "Inverse Kinematics";
            this.btnCalculate_InverseKinematics.UseVisualStyleBackColor = true;
            this.btnCalculate_InverseKinematics.Click += new System.EventHandler(this.btnCalculate_InverseKinematics_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Z :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "Roll :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "Pitch :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "Yaw :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "L6 :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "L5 :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(220, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "L4 :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "L3 :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(220, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "L2 :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(220, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "L1 :";
            // 
            // txtOutput_L6
            // 
            this.txtOutput_L6.Location = new System.Drawing.Point(273, 273);
            this.txtOutput_L6.Name = "txtOutput_L6";
            this.txtOutput_L6.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L6.TabIndex = 24;
            // 
            // txtOutput_L5
            // 
            this.txtOutput_L5.Location = new System.Drawing.Point(273, 224);
            this.txtOutput_L5.Name = "txtOutput_L5";
            this.txtOutput_L5.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L5.TabIndex = 23;
            // 
            // txtOutput_L4
            // 
            this.txtOutput_L4.Location = new System.Drawing.Point(273, 176);
            this.txtOutput_L4.Name = "txtOutput_L4";
            this.txtOutput_L4.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L4.TabIndex = 22;
            // 
            // txtOutput_L3
            // 
            this.txtOutput_L3.Location = new System.Drawing.Point(273, 127);
            this.txtOutput_L3.Name = "txtOutput_L3";
            this.txtOutput_L3.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L3.TabIndex = 21;
            // 
            // txtOutput_L2
            // 
            this.txtOutput_L2.Location = new System.Drawing.Point(273, 78);
            this.txtOutput_L2.Name = "txtOutput_L2";
            this.txtOutput_L2.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L2.TabIndex = 20;
            // 
            // txtOutput_L1
            // 
            this.txtOutput_L1.Location = new System.Drawing.Point(273, 29);
            this.txtOutput_L1.Name = "txtOutput_L1";
            this.txtOutput_L1.Size = new System.Drawing.Size(113, 21);
            this.txtOutput_L1.TabIndex = 19;
            // 
            // Hexapod_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 396);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtOutput_L6);
            this.Controls.Add(this.txtOutput_L5);
            this.Controls.Add(this.txtOutput_L4);
            this.Controls.Add(this.txtOutput_L3);
            this.Controls.Add(this.txtOutput_L2);
            this.Controls.Add(this.txtOutput_L1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCalculate_InverseKinematics);
            this.Controls.Add(this.txtInput_Yaw);
            this.Controls.Add(this.txtInput_Pitch);
            this.Controls.Add(this.txtInput_Roll);
            this.Controls.Add(this.txtInput_Z);
            this.Controls.Add(this.txtInput_Y);
            this.Controls.Add(this.txtInput_X);
            this.Name = "Hexapod_View";
            this.Text = "Hexapod";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput_X;
        private System.Windows.Forms.TextBox txtInput_Y;
        private System.Windows.Forms.TextBox txtInput_Z;
        private System.Windows.Forms.TextBox txtInput_Roll;
        private System.Windows.Forms.TextBox txtInput_Yaw;
        private System.Windows.Forms.TextBox txtInput_Pitch;
        private System.Windows.Forms.Button btnCalculate_InverseKinematics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOutput_L6;
        private System.Windows.Forms.TextBox txtOutput_L5;
        private System.Windows.Forms.TextBox txtOutput_L4;
        private System.Windows.Forms.TextBox txtOutput_L3;
        private System.Windows.Forms.TextBox txtOutput_L2;
        private System.Windows.Forms.TextBox txtOutput_L1;
    }
}

