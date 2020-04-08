using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200408_Hexapod
{
    public partial class Main_UI : Form
    {
        #region 생성자
        public Main_UI()
        {
            InitializeComponent();
            SetupDataGridView_Vector();
        }
        #endregion

        #region delegate 설정
        public delegate void SetHexapod_Hardware(int nNumOfJoint, double dbRadius_Base, double dbAngleOfOffset_Base, double dbRadius_Upper, double dbAngleOfOffset_Upper);
        public SetHexapod_Hardware Delegate_SetHardware;
        public delegate void SetHexapod_HeightVector(double dbHeight);
        public SetHexapod_HeightVector Delegate_SetHeightVector;
        #endregion

        #region 메소드
        private void btnMakeHardware_Click(object sender, EventArgs e)
        {
            int nNumberOfJoint          = Convert.ToInt16(txtNumOfJoint.Text.Trim());
            int nDegreeOfOffset_Base    = Convert.ToInt32(txtAngleOfOffset_Base.Text.Trim());
            double dbRadius_Base        = Convert.ToDouble(txtRadius_Base.Text.Trim());        
            double dbAngleOfOffset_Base = nDegreeOfOffset_Base * Math.PI / 180;

            int nDegreeOfOffset_Upper    = Convert.ToInt16(txtAngleOfOffset_Upper.Text.Trim());
            double dbRadius_Upper        = Convert.ToDouble(txtRadius_Upper.Text.Trim());          
            double dbAngleOfOffset_Upper = nDegreeOfOffset_Upper * Math.PI / 180;

            Delegate_SetHardware(nNumberOfJoint, dbRadius_Base, dbAngleOfOffset_Base, dbRadius_Upper, dbAngleOfOffset_Upper);

            double dbHeight = Convert.ToDouble(txtHeight.Text.Trim());
            Delegate_SetHeightVector(dbHeight);
        }

        private void SetupDataGridView_Vector()
        {
            DataGridView_Vector.ColumnCount = 5;

            DataGridView_Vector.Columns[0].Name = "Vector";
            DataGridView_Vector.Columns[1].Name = "X";
            DataGridView_Vector.Columns[2].Name = "Y";
            DataGridView_Vector.Columns[3].Name = "Z";
            DataGridView_Vector.Columns[4].Name = "Length";
        }

        private void btnCalculateVector_Click(object sender, EventArgs e)
        {
          


        }


        #endregion



    }
}
