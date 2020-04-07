using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _200406_hexapod
{
    public partial class Hexapod_View : Form
    {

        #region 멤버

        Hexapod_Model Model = new Hexapod_Model();

        #endregion

        public Hexapod_View()
        {
            InitializeComponent();
        }

        private void btnCalculate_InverseKinematics_Click(object sender, EventArgs e)
        {
            double[] dbTranslation = { 0, 0, 0 };
            dbTranslation[0] = Convert.ToDouble(txtInput_X.ToString());
            dbTranslation[1] = Convert.ToDouble(txtInput_Y.ToString());
            dbTranslation[2] = Convert.ToDouble(txtInput_Z.ToString());
            
            double[] dbRotation = { 0, 0, 0 };
            dbRotation[0] = Convert.ToDouble(txtInput_Roll.ToString());
            dbRotation[1] = Convert.ToDouble(txtInput_Pitch.ToString());
            dbRotation[2] = Convert.ToDouble(txtInput_Yaw.ToString());

            Model.CalculateRotateEulerAngle(dbRotation[0], dbRotation[1], dbRotation[2], dbTranslation);

        }

    }
}
