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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtToolOffset_Z = new System.Windows.Forms.TextBox();
            this.txtToolOffset_Y = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtToolOffset_X = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSetTarget = new System.Windows.Forms.Button();
            this.txtTarget_Yaw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTarget_Pitch = new System.Windows.Forms.TextBox();
            this.txtTarget_Roll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTarget_Z = new System.Windows.Forms.TextBox();
            this.txtTarget_Y = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTarget_X = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chartLinearGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cboSelectGraph = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Vector)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLinearGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 203);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 12);
            this.label13.TabIndex = 46;
            this.label13.Text = "Number Of Joints :";
            // 
            // txtNumOfJoint
            // 
            this.txtNumOfJoint.Location = new System.Drawing.Point(156, 199);
            this.txtNumOfJoint.Name = "txtNumOfJoint";
            this.txtNumOfJoint.Size = new System.Drawing.Size(50, 21);
            this.txtNumOfJoint.TabIndex = 45;
            this.txtNumOfJoint.Text = "6";
            // 
            // btnMakeHardware
            // 
            this.btnMakeHardware.Location = new System.Drawing.Point(157, 271);
            this.btnMakeHardware.Name = "btnMakeHardware";
            this.btnMakeHardware.Size = new System.Drawing.Size(62, 91);
            this.btnMakeHardware.TabIndex = 44;
            this.btnMakeHardware.Text = "Set";
            this.btnMakeHardware.UseVisualStyleBackColor = true;
            this.btnMakeHardware.Click += new System.EventHandler(this.btnMakeHardware_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAngleOfOffset_Upper);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtRadius_Upper);
            this.groupBox2.Location = new System.Drawing.Point(10, 107);
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
            this.groupBox1.Location = new System.Drawing.Point(10, 17);
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
            this.DataGridView_Vector.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView_Vector.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView_Vector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Vector.Location = new System.Drawing.Point(393, 27);
            this.DataGridView_Vector.Name = "DataGridView_Vector";
            this.DataGridView_Vector.RowTemplate.Height = 23;
            this.DataGridView_Vector.Size = new System.Drawing.Size(648, 120);
            this.DataGridView_Vector.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "Result(Vector)";
            // 
            // btnCalculateVector
            // 
            this.btnCalculateVector.Location = new System.Drawing.Point(239, 276);
            this.btnCalculateVector.Name = "btnCalculateVector";
            this.btnCalculateVector.Size = new System.Drawing.Size(140, 102);
            this.btnCalculateVector.TabIndex = 49;
            this.btnCalculateVector.Text = "Calculate Vector";
            this.btnCalculateVector.UseVisualStyleBackColor = true;
            this.btnCalculateVector.Click += new System.EventHandler(this.btnCalculateVector_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "Height : ";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(156, 233);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(50, 21);
            this.txtHeight.TabIndex = 50;
            this.txtHeight.Text = "0.16";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtHeight);
            this.groupBox3.Controls.Add(this.btnMakeHardware);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtNumOfJoint);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(2, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 373);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtToolOffset_Z);
            this.groupBox5.Controls.Add(this.txtToolOffset_Y);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtToolOffset_X);
            this.groupBox5.Location = new System.Drawing.Point(10, 263);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(141, 99);
            this.groupBox5.TabIndex = 59;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ToolOffset";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 12);
            this.label9.TabIndex = 58;
            this.label9.Text = "Z :";
            // 
            // txtToolOffset_Z
            // 
            this.txtToolOffset_Z.Location = new System.Drawing.Point(83, 74);
            this.txtToolOffset_Z.Name = "txtToolOffset_Z";
            this.txtToolOffset_Z.Size = new System.Drawing.Size(50, 21);
            this.txtToolOffset_Z.TabIndex = 57;
            this.txtToolOffset_Z.Text = "0";
            // 
            // txtToolOffset_Y
            // 
            this.txtToolOffset_Y.Location = new System.Drawing.Point(83, 44);
            this.txtToolOffset_Y.Name = "txtToolOffset_Y";
            this.txtToolOffset_Y.Size = new System.Drawing.Size(50, 21);
            this.txtToolOffset_Y.TabIndex = 56;
            this.txtToolOffset_Y.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 12);
            this.label10.TabIndex = 55;
            this.label10.Text = "Y :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 12);
            this.label11.TabIndex = 54;
            this.label11.Text = "X :";
            // 
            // txtToolOffset_X
            // 
            this.txtToolOffset_X.Location = new System.Drawing.Point(83, 15);
            this.txtToolOffset_X.Name = "txtToolOffset_X";
            this.txtToolOffset_X.Size = new System.Drawing.Size(50, 21);
            this.txtToolOffset_X.TabIndex = 53;
            this.txtToolOffset_X.Text = "0";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSetTarget);
            this.groupBox4.Controls.Add(this.txtTarget_Yaw);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtTarget_Pitch);
            this.groupBox4.Controls.Add(this.txtTarget_Roll);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtTarget_Z);
            this.groupBox4.Controls.Add(this.txtTarget_Y);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtTarget_X);
            this.groupBox4.Location = new System.Drawing.Point(239, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(140, 253);
            this.groupBox4.TabIndex = 53;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Target(Upper)";
            // 
            // btnSetTarget
            // 
            this.btnSetTarget.Location = new System.Drawing.Point(7, 213);
            this.btnSetTarget.Name = "btnSetTarget";
            this.btnSetTarget.Size = new System.Drawing.Size(124, 35);
            this.btnSetTarget.TabIndex = 52;
            this.btnSetTarget.Text = "Set Target";
            this.btnSetTarget.UseVisualStyleBackColor = true;
            this.btnSetTarget.Click += new System.EventHandler(this.btnSetTarget_Click);
            // 
            // txtTarget_Yaw
            // 
            this.txtTarget_Yaw.Location = new System.Drawing.Point(81, 186);
            this.txtTarget_Yaw.Name = "txtTarget_Yaw";
            this.txtTarget_Yaw.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_Yaw.TabIndex = 44;
            this.txtTarget_Yaw.Text = "30";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 12);
            this.label7.TabIndex = 43;
            this.label7.Text = "Yaw(Deg) :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 12);
            this.label8.TabIndex = 42;
            this.label8.Text = "Pitch(Deg) :";
            // 
            // txtTarget_Pitch
            // 
            this.txtTarget_Pitch.Location = new System.Drawing.Point(81, 153);
            this.txtTarget_Pitch.Name = "txtTarget_Pitch";
            this.txtTarget_Pitch.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_Pitch.TabIndex = 41;
            this.txtTarget_Pitch.Text = "20";
            // 
            // txtTarget_Roll
            // 
            this.txtTarget_Roll.Location = new System.Drawing.Point(81, 120);
            this.txtTarget_Roll.Name = "txtTarget_Roll";
            this.txtTarget_Roll.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_Roll.TabIndex = 40;
            this.txtTarget_Roll.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "Roll(Deg) :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 38;
            this.label6.Text = "Z :";
            // 
            // txtTarget_Z
            // 
            this.txtTarget_Z.Location = new System.Drawing.Point(81, 87);
            this.txtTarget_Z.Name = "txtTarget_Z";
            this.txtTarget_Z.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_Z.TabIndex = 37;
            this.txtTarget_Z.Text = "0";
            // 
            // txtTarget_Y
            // 
            this.txtTarget_Y.Location = new System.Drawing.Point(81, 54);
            this.txtTarget_Y.Name = "txtTarget_Y";
            this.txtTarget_Y.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_Y.TabIndex = 36;
            this.txtTarget_Y.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "Y :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 34;
            this.label4.Text = "X :";
            // 
            // txtTarget_X
            // 
            this.txtTarget_X.Location = new System.Drawing.Point(81, 21);
            this.txtTarget_X.Name = "txtTarget_X";
            this.txtTarget_X.Size = new System.Drawing.Size(50, 21);
            this.txtTarget_X.TabIndex = 33;
            this.txtTarget_X.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(391, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 12);
            this.label12.TabIndex = 54;
            this.label12.Text = "Graph";
            // 
            // chartLinearGraph
            // 
            chartArea2.Name = "ChartArea1";
            this.chartLinearGraph.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartLinearGraph.Legends.Add(legend2);
            this.chartLinearGraph.Location = new System.Drawing.Point(393, 189);
            this.chartLinearGraph.Name = "chartLinearGraph";
            this.chartLinearGraph.Size = new System.Drawing.Size(648, 206);
            this.chartLinearGraph.TabIndex = 55;
            this.chartLinearGraph.Text = "Chart";
            // 
            // cboSelectGraph
            // 
            this.cboSelectGraph.FormattingEnabled = true;
            this.cboSelectGraph.Location = new System.Drawing.Point(920, 157);
            this.cboSelectGraph.Name = "cboSelectGraph";
            this.cboSelectGraph.Size = new System.Drawing.Size(121, 20);
            this.cboSelectGraph.TabIndex = 56;
            this.cboSelectGraph.SelectedIndexChanged += new System.EventHandler(this.cboSelectGraph_SelectedIndexChanged);
            // 
            // Main_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 407);
            this.Controls.Add(this.cboSelectGraph);
            this.Controls.Add(this.chartLinearGraph);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCalculateVector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGridView_Vector);
            this.Name = "Main_UI";
            this.Text = "Hexapod";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Vector)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLinearGraph)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSetTarget;
        private System.Windows.Forms.TextBox txtTarget_Yaw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTarget_Pitch;
        private System.Windows.Forms.TextBox txtTarget_Roll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTarget_Z;
        private System.Windows.Forms.TextBox txtTarget_Y;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTarget_X;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtToolOffset_Z;
        private System.Windows.Forms.TextBox txtToolOffset_Y;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtToolOffset_X;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLinearGraph;
        private System.Windows.Forms.ComboBox cboSelectGraph;
    }
}

