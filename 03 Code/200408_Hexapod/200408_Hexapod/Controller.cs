using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Controller
    {

        #region Model 객체 선언
        Main_UI Main_ui = null;
        Hardware_Model Model_hw = null;
        Coordinate_Model Model_Coordinate = null;
        #endregion 

        public Controller()
        {
            Main_ui = new Main_UI();
            Model_hw = new Hardware_Model();
            Model_Coordinate = new Coordinate_Model();


            RegisterDelegate_MainUI();

            Main_ui.Show();
        }

        #region Delegate 설정

        private void RegisterDelegate_MainUI()
        {
            Main_ui.Delegate_SetHardware += new Main_UI.SetHexapod_Hardware(SetHardware);
            Main_ui.Delegate_SetHeightVector += new Main_UI.SetHexapod_HeightVector(SetHeightVector);
        }
        #endregion

        #region 메소드

        private void SetHardware(int nNumOfJoint, double dbRadius_Base, double dbAngleOfOffset_Base, double dbRadius_Upper, double dbAngleOfOffset_Upper)
        {
            Model_hw.MakeHexapodPlate(nNumOfJoint, dbRadius_Base, dbAngleOfOffset_Base, dbRadius_Upper, dbAngleOfOffset_Upper);
        }

        private void SetHeightVector(double dbHeight)
        {
            Model_Coordinate.SetBasetoHeightVector(dbHeight);
        }

        private void CalculateVector()
        {

        }

        #endregion
    }
}
