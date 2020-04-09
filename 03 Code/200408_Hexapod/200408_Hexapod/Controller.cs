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

        #region 생성자
        public Controller()
        {
            Main_ui = new Main_UI();
            Model_hw = new Hardware_Model();
            Model_Coordinate = new Coordinate_Model();

            RegisterDelegate_MainUI();

            Main_ui.Show();
        }
        #endregion

        #region Delegate 설정
        /// <summary>
        /// 2020.04.08 by chjung [ADD] Form에서 생성한 Delegate를 등록한다
        /// </summary>
        private void RegisterDelegate_MainUI()
        {
            Main_ui.Delegate_SetHardware           += new Main_UI.SetHexapod_Hardware(SetHardware);
            Main_ui.Delegate_SetHeightVector       += new Main_UI.SetHexapod_HeightVector(SetHeightVector);
            Main_ui.Delegate_SetTarget             += new Main_UI.SetHexapod_Target(SetTargetCoordinate);
            Main_ui.Delegate_CalculateMovingVector += new Main_UI.CalculateMovingVector(CalculateMovingVector);
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

        private void SetTargetCoordinate(double[] dbPositon, double[] dbRotation)
        {
            Model_hw.Plate_Upper.Position = dbPositon;
            Model_hw.Plate_Upper.Rotation = dbRotation;
        }

        private void CalculateMovingVector()
        {
            // 1. base to base point vector 계산
            Model_Coordinate.CalculateBaseToBaseJoint(Model_hw.Plate_Base.dicOfJointVector);        
            // 2. base에서 바라본 upper point vector 계산(회전 행렬) -> Upper값이 등록되었는지 확인
            Model_Coordinate.CalculateBaseToUpperJoint(Model_hw.Plate_Upper.Rotation, Model_hw.Plate_Upper.dicOfJointVector);
            Model_Coordinate.CalculateBaseJointToUpperJointVector();
            // 3. 최종 Actuator Vector 계산
        }

        #endregion
    }
}
