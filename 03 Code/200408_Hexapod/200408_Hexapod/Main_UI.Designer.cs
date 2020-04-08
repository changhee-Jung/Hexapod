namespace _200408_Hexapod
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.txtNumOfJoint = new System.Windows.Forms.TextBox();
            this.btnMakeHardware = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAngleOfOffset_Upper = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtRadius_Upper = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAngleOfOffset_Base = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRadius_Base = new System.Windows.Forms.TextBox();
            this.DataGridView_Vector = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCalculateVector = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Vector)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 12);
            this.label13.TabIndex = 46;
            this.label13.Text = "Number Of Joints :";
            // 
            // txtNumOfJoint
            // 
            this.txtNumOfJoint.Location = new System.Drawing.Point(158, 206);
            this.txtNumOfJoint.Name = "txtNumOfJoint";
            this.txtNumOfJoint.Size = new System.Drawing.Size(50, 21);
            this.txtNumOfJoint.TabIndex = 45;
            this.txtNumOfJoint.Text = "6";
            // 
            // btnMakeHardware
            // 
            this.btnMakeHardware.Location = new System.Drawing.Point(12, 265);
            this.btnMakeHardware.Name = "btnMakeHardware";
            this.btnMakeHardware.Size = new System.Drawing.Size(212, 35);
            this.btnMakeHardware.TabIndex = 44;
            this.btnMakeHardware.Text = "Make HardWare";
            this.btnMakeHardware.UseVisualStyleBackColor = true;
            this.btnMakeHardware.Click += new System.EventHandler(this.btnMakeHardware_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAngleOfOffset_Upper);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtRadius_Upper);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 85);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Upper";
            // 
            // txtAngleOfOffset_Upper
            // 
            this.txtAngleOfOffset_Upper.Location = new System.Drawing.Point(146, 54);
            this.txtAngleOfOffset_Upper.Name = "txtAngleOfOffset_Upper";
            this.txtAngleOfOffset_Upper.Size = new System.Drawing.Size(50, 21);
            this.txtAngleOfOffset_Upper.TabIndex = 36;
            this.txtAngleOfOffset_Upper.Text = "10";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(129, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "Angle Of Offset(Deg) :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 12);
            this.label17.TabIndex = 34;
            this.label17.Text = "Radius :";
            // 
            // txtRadius_Upper
            // 
            this.txtRadius_Upper.Location = new System.Drawing.Point(146, 21);
            this.txtRadius_Upper.Name = "txtRadius_Upper";
            this.txtRadius_Upper.Size = new System.Drawing.Size(50, 21);
            this.txtRadius_Upper.TabIndex = 33;
            this.txtRadius_Upper.Text = "0.08";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAngleOfOffset_Base);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtRadius_Base);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 84);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BASE";
            // 
            // txtAngleOfOffset_Base
            // 
            this.txtAngleOfOffset_Base.Location = new System.Drawing.Point(146, 53);
            this.txtAngleOfOffset_Base.Name = "txtAngleOfOffset_Base";
            this.txtAngleOfOffset_Base.Size = new System.Drawing.Size(50, 21);
            this.txtAngleOfOffset_Base.TabIndex = 36;
            this.txtAngleOfOffset_Base.Text = "15";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(129, 12);
            this.label15.TabIndex = 35;
            this.label15.Text = "Angle Of Offset(Deg) :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 12);
            this.label14.TabIndex = 34;
            this.label14.Text = "Radius :";
            // 
            // txtRadius_Base
            // 
            this.txtRadius_Base.Location = new System.Drawing.Point(146, 20);
            this.txtRadius_Base.Name = "txtRadius_Base";
            this.txtRadius_Base.Size = new System.Drawing.Size(50, 21);
            this.txtRadius_Base.TabIndex = 33;
            this.txtRadius_Base.Text = "0.16";
            // 
            // DataGridView_Vector
            // 
            this.DataGridView_Vector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Vector.Location = new System.Drawing.Point(240, 31);
            this.DataGridView_Vector.Name = "DataGridView_Vector";
            this.DataGridView_Vector.RowTemplate.Height = 23;
            this.DataGridView_Vector.Size = new System.Drawing.Size(562, 278);
            this.DataGridView_Vector.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "Result(Vector)";
            // 
            // btnCalculateVector
            // 
            this.btnCalculateVector.Location = new System.Drawing.Point(12, 306);
            this.btnCalculateVector.Name = "btnCalculateVector";
            this.btnCalculateVector.Size = new System.Drawing.Size(212, 35);
            this.btnCalculateVector.TabIndex = 49;
            this.btnCalculateVector.Text = "Calculate Vector";
            this.btnCalculateVector.UseVisualStyleBackColor = true;
            this.btnCalculateVector.Click += new System.EventHandler(this.btnCalculateVector_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "Height : ";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(158, 240);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(50, 21);
            this.txtHeight.TabIndex = 50;
            this.txtHeight.Text = "0.16";
            // 
            // Main_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 352);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.btnCalculateVector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGridView_Vector);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtNumOfJoint);
            this.Controls.Add(this.btnMakeHardware);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main_UI";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Vector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNumOfJoint;
        private System.Windows.Forms.Button btnMakeHardware;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAngleOfOffset_Upper;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtRadius_Upper;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAngleOfOffset_Base;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRadius_Base;
        private System.Windows.Forms.DataGridView DataGridView_Vector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalculateVector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHeight;
    }
}

