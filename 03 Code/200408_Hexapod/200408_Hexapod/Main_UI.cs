using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace _200408_Hexapod
{

    public partial class Main_UI : Form
    {
        #region 생성자
        public Main_UI()
        {
            InitializeComponent();
            SetupDataGridView_Vector();

            Process = new SequenceProcess(this);
        }
        #endregion

        #region 멤버
        public event EventHandler<DataEventArgs> UpdateData;
        public SequenceProcess Process;
        public int m_nSelectedAxis = 0;
        private Dictionary<int, Series> m_dicOfGraphsPosition = new Dictionary<int, Series>();
        private Dictionary<int, Series> m_dicOfGraphsVelocity = new Dictionary<int, Series>();
        #endregion

        #region 메소드
        private void btnMakeHardware_Click(object sender, EventArgs e)
        {
            int nNumberOfJoint = Convert.ToInt16(txtNumOfJoint.Text.Trim());
            int nDegreeOfOffset_Base = Convert.ToInt32(txtAngleOfOffset_Base.Text.Trim());
            double dbRadius_Base = Convert.ToDouble(txtRadius_Base.Text.Trim());
            double dbAngleOfOffset_Base = nDegreeOfOffset_Base * Math.PI / 180;

            int nDegreeOfOffset_Upper = Convert.ToInt16(txtAngleOfOffset_Upper.Text.Trim());
            double dbRadius_Upper = Convert.ToDouble(txtRadius_Upper.Text.Trim());
            double dbAngleOfOffset_Upper = nDegreeOfOffset_Upper * Math.PI / 180;

            double dbToolOffset_X = Convert.ToDouble(txtToolOffset_X.Text.Trim());
            double dbToolOffset_Y = Convert.ToDouble(txtToolOffset_Y.Text.Trim());
            double dbToolOffset_Z = Convert.ToDouble(txtToolOffset_Z.Text.Trim());
            double[] ar_dbToolOffset = { dbToolOffset_X, dbToolOffset_Y, dbToolOffset_Z };

            DataEventArgs DesignEventArgs = new DataEventArgs();

            DesignEventArgs.Design.nNumberOfJoint        = nNumberOfJoint;
            DesignEventArgs.Design.dbRadius_Base         = dbRadius_Base;
            DesignEventArgs.Design.dbRadius_Upper        = dbRadius_Upper;
            DesignEventArgs.Design.dbAngleOfOffset_Base  = dbAngleOfOffset_Base;
            DesignEventArgs.Design.dbAngleOfOffset_Upper = dbAngleOfOffset_Upper;
            DesignEventArgs.Design.ar_dbToolOffset       = ar_dbToolOffset;

            double dbHeight = Convert.ToDouble(txtHeight.Text.Trim());
            DesignEventArgs.Design.dbHeight = dbHeight;

            DesignEventArgs.Callback = CallBackMethod.SetHardWareData;

            if (UpdateData != null)
                UpdateData.Invoke(this, DesignEventArgs);
        }

        private void SetupDataGridView_Vector()
        {
            DataGridView_Vector.ColumnCount = 5;

            DataGridView_Vector.Columns[0].Name = "Vector";
            DataGridView_Vector.Columns[1].Name = "X";
            DataGridView_Vector.Columns[2].Name = "Y";
            DataGridView_Vector.Columns[3].Name = "Z";
            DataGridView_Vector.Columns[4].Name = "Length(Absolute)";
        }

        private void btnCalculateVector_Click(object sender, EventArgs e)
        {
            DataEventArgs MovingEventArgs = new DataEventArgs();
            MovingEventArgs.Callback = CallBackMethod.CalculateMovingVector;

            if (UpdateData != null)
                UpdateData.Invoke(this, MovingEventArgs);
        }

        private void btnSetTarget_Click(object sender, EventArgs e)
        {
            double dbTarget_X = Convert.ToDouble(txtTarget_X.Text.Trim());
            double dbTarget_Y = Convert.ToDouble(txtTarget_Y.Text.Trim());
            double dbTarget_Z = Convert.ToDouble(txtTarget_Z.Text.Trim());

            double[] dbTargetPosition = { dbTarget_X, dbTarget_Y, dbTarget_Z };

            int nDegreeOfTarget_Roll = Convert.ToInt16(txtTarget_Roll.Text.Trim());
            int nDegreeOfTarget_Pitch = Convert.ToInt16(txtTarget_Pitch.Text.Trim());
            int nDegreeOfTarget_Yaw = Convert.ToInt16(txtTarget_Yaw.Text.Trim());
            double dbTarget_Roll = nDegreeOfTarget_Roll * Math.PI / 180;
            double dbTarget_Pitch = nDegreeOfTarget_Pitch * Math.PI / 180;
            double dbTarget_Yaw = nDegreeOfTarget_Yaw * Math.PI / 180;

            double[] dbTargetRotation = { dbTarget_Roll, dbTarget_Pitch, dbTarget_Yaw };

            DataEventArgs TargetDataEventArgs = new DataEventArgs();

            TargetDataEventArgs.Target.dbTargetPosition = dbTargetPosition;
            TargetDataEventArgs.Target.dbTargetRotation = dbTargetRotation;
            TargetDataEventArgs.Callback = CallBackMethod.SetTargetCoordinate;

            if (UpdateData != null)
                UpdateData.Invoke(this, TargetDataEventArgs);

        }

        public void DisplayDateGridView(Dictionary<int, double[]> dicOfVector, Dictionary<int, double> dbTargetLengths)
        {
            // 현 DataGirdView 초기화
            DataGridView_Vector.Columns.Clear();
            DataGridView_Vector.Rows.Clear();

            // Column 등록
            SetupDataGridView_Vector();

            // Rows 데이터 등록

            foreach (KeyValuePair<int, double[]> dbVector in dicOfVector)
            {
                DataGridView_Vector.Rows.Add("S" + dbVector.Key.ToString(), dbVector.Value[0], dbVector.Value[1], dbVector.Value[2], dbTargetLengths[dbVector.Key]);
            }
        }

   

        public void InitializeChartControl()
        {
            chartLinearGraph.Series.Clear();
            chartLinearGraph.ChartAreas.Clear();

            chartLinearGraph.ChartAreas.Add("Position");
            chartLinearGraph.ChartAreas.Add("Velocity");
            chartLinearGraph.ChartAreas.Add("Acceleration");

        }
        public void DisplayMotionProfileData(int nIndex, string strName, Dictionary<int, double> MotionData)
        {
            string strAxisNumber = "L" + nIndex.ToString() + ": " + strName;

            Series series = new Series(strAxisNumber);
            series.ChartType = SeriesChartType.Line;
         //   series.Legend    = "L" + nIndex.ToString();
            series.ChartArea = strName;
            chartLinearGraph.Series.Add(series);

            chartLinearGraph.ChartAreas[strName].AxisX.Minimum = 0;
            chartLinearGraph.ChartAreas[strName].AxisX.Maximum = MotionData.Count;
            chartLinearGraph.ChartAreas[strName].AxisX.Interval = 100;

          //  chartLinearGraph.Series[strAxisNumber].IsVisibleInLegend = true;
                        
            foreach (KeyValuePair<int, double> Data in MotionData)
            {
                series.Points.AddXY(Data.Key, Data.Value);
            }

        }


        private void chartLinearGraph_MouseMove(object sender, MouseEventArgs e)
        {
            string strAxis = m_nSelectedAxis.ToString();
            var source = sender as Chart;
            HitTestResult result = source.HitTest(e.X, e.Y);
  
            foreach (Series series in source.Series)
            {
                if(true == series.Name.Contains(strAxis))
                {
                    if (result.ChartElementType == ChartElementType.DataPoint && result.PointIndex != -1)
                    {

                        if (result.PointIndex >= series.Points.Count) { return; }
                        var xValue = series.Points[result.PointIndex].XValue;
                        var yValue = series.Points[result.PointIndex].YValues[0];
                        string[] strSplit = series.Name.Split(':');
                        string strValue = strSplit[1].Trim();

                        lblGraphData_X.Text = xValue.ToString();
                        
                        switch(strValue)
                        {
                            case "Position":
                                lblGraphData_Pos.Text = yValue.ToString();
                                break;
                            case "Velocity":
                                lblGraphData_Vel.Text = yValue.ToString();
                                break;
                            case "Acceleration":
                                lblGraphData_Accel.Text = yValue.ToString();
                                break;
                        }
                        
                    }
          
                }
            }
        }


        public void SetcomboSelectItem(List<string> listOfStringData)
        {
            cboAxis.Items.Clear();

            for (int i = 0; i < listOfStringData.Count; i++)
            {
                string strName = listOfStringData[i];
                cboAxis.Items.Add(strName);
            }

        }

        private void cboAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = cboAxis.SelectedItem.ToString();
            string[] strSplit = strName.Split(':');
            m_nSelectedAxis = Convert.ToInt16(strSplit[1]);
        }

      

        #endregion

    






    }
}
   