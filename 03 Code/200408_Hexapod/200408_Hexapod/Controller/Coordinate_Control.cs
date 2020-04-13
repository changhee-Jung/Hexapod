using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    #region 열거체
    public enum CallBackMethod
    {
        SetHardWareData,
        SetTargetCoordinate,
        CalculateMovingVector,
        CallMotionProfileData
    }
    public struct DesignData
    {
        public int nNumberOfJoint;
        public double dbHeight;
        public double dbRadius_Base;
        public double dbAngleOfOffset_Base;
        public double dbRadius_Upper;
        public double dbAngleOfOffset_Upper;
    }
    public struct TargetData
    {
        public double[] dbTargetPosition;
        public double[] dbTargetRotation;
    }
    #endregion

    #region EventArgs 설정
    public class DataEventArgs : EventArgs
    {
        public DesignData Design;
        public TargetData Target;

        public CallBackMethod Callback;

        public string strName;
        public int nIndex;
    }
    #endregion

    class Coordinate_Control
    {

        #region 생성자
        public Coordinate_Control(Main_UI _Main_ui, Hardware_Model Hardware, Coordinate_Model Coordinate, Motion_Model Motion)
        {
            Model_Hw = Hardware;
            Model_Coordinate = Coordinate;
            Model_Motion = Motion;
            Main_ui = _Main_ui;            
        }
        #endregion

        #region 멤버
        Hardware_Model Model_Hw = null;
        Coordinate_Model Model_Coordinate = null;
        Motion_Model Model_Motion = null;
        Main_UI Main_ui = null;
        bool m_bIsSetAllHardWare = false;
        bool m_bIsMakeAllProfile = false;
        #endregion    

        #region 속성
        public bool IsSetAllHardWare { get { return m_bIsSetAllHardWare; } }
        public bool IsMakeAllProfile { get { return m_bIsMakeAllProfile; } }
        #endregion

        #region 메소드
        public void InitializeState()
        {
            m_bIsSetAllHardWare = false;
            m_bIsMakeAllProfile = false;
        }
        public void EventCallbackMethod(object sender, DataEventArgs e)
        {          
            switch (e.Callback)
            {
                case CallBackMethod.SetHardWareData:
                    SetHardware(e);
                    SetHeightVector(e);
                    break;
                case CallBackMethod.SetTargetCoordinate:
                    SetTargetCoordinate(e);
                    break;
                case CallBackMethod.CalculateMovingVector:
                    CalculateMovingVector();
                    break;
                case CallBackMethod.CallMotionProfileData:
                    CallMotionProfileData(e);
                    break;
            }
        }

        private void SetHardware(DataEventArgs e)
        {
            InitializeState(); // 이전 설정 초기화

            int nNumOfJoint = e.Design.nNumberOfJoint;
            double dbRadius_Base = e.Design.dbRadius_Base;
            double dbAngleOfOffset_Base = e.Design.dbAngleOfOffset_Base;
            double dbRadius_Upper = e.Design.dbRadius_Upper;
            double dbAngleOfOffset_Upper = e.Design.dbAngleOfOffset_Upper;

            Model_Hw.MakeHexapodPlate(nNumOfJoint, dbRadius_Base, dbAngleOfOffset_Base, dbRadius_Upper, dbAngleOfOffset_Upper);

            Model_Motion.NumberOfAxis = nNumOfJoint;
        }

        private void SetHeightVector(DataEventArgs e)
        {
            double dbHeight             = e.Design.dbHeight;
            Model_Hw.Plate_Upper.Height = dbHeight;
        }

        private void SetTargetCoordinate(DataEventArgs e)
        {
            // 여기서 ToolOffset 보상
            Model_Hw.Plate_Upper.Position = e.Target.dbTargetPosition;
            Model_Hw.Plate_Upper.Rotation = e.Target.dbTargetRotation;
        }

        private void CalculateMovingVector()
        {
            // 1. base to base point vector 계산
            Model_Coordinate.SetBasetoHeightVector(Model_Hw.Plate_Upper.Height);
            Model_Coordinate.CalculateBaseToUpperPlateVector(Model_Hw.Plate_Base.dicOfJointVector);
            // 2. baseJoint to UpperPlate Vector 계산

            // 2. base에서 바라본 upper point vector 계산(회전 행렬) -> Upper값이 등록되었는지 확인
            Model_Coordinate.CalculateBaseToUpperJoint(Model_Hw.Plate_Upper.Rotation, Model_Hw.Plate_Upper.dicOfJointVector);
            Model_Coordinate.CalculateBaseJointToUpperJointVector();
            Model_Coordinate.CalculateTargetPostionTranslation(Model_Hw.Plate_Upper.Position);
        
            Model_Coordinate.CalculateActuatorVector();
            SetMotionData(Model_Coordinate.TargetLenghsOfActuator);

            m_bIsSetAllHardWare = true;

            // 3. 최종 Actuator Vector 계산
            DisplayActuatorVector();
        }

        private void DisplayActuatorVector()
        {
            Main_ui.DisplayDateGridView(Model_Coordinate.BaseToTargetVector, Model_Coordinate.TargetLenghsOfActuator);
        }

        private void SetMotionData(Dictionary<int,double> TargetLengths)
        {
            // 1. 현 위치를 기반으로 이동해야할 위치 계산.
            Model_Motion.SetMotorAxis(TargetLengths.Count);
            Model_Motion.SetTargetPosition(TargetLengths);

        }
        public void CalculateNextStepMotion(int nTicktime, int nEndtime)
        {
            if (true == Model_Motion.CheckCompleteMotionProfiles())
            {
                for (int nIndex = 0; nIndex < Model_Motion.NumberOfAxis; nIndex++ )
                {
                    Profile profile = Model_Motion.GetAxisProfile(nIndex);
                    Main_ui.Invoke(new Action(
                                       delegate()
                                       {
                                           Main_ui.SetcomboSelectItem("Position", nIndex);
                                           Main_ui.SetcomboSelectItem("Velocity", nIndex);
                                       }));
                }
               
                m_bIsMakeAllProfile = true;
            }
            else
            {
                Model_Motion.MakeMotionProfile(nTicktime, nEndtime);
                
            }          
        }

        public void CallMotionProfileData(DataEventArgs e)
        {
            string strName = e.strName;
            int nIndex = e.nIndex;
            Profile SelectedProfile = Model_Motion.GetAxisProfile(nIndex);

            Dictionary<int, double> dicOfGraphData = null;
             string strSendName = null;

            if (strName == "Position")
            {
                strSendName = strName + ": " + nIndex.ToString();
                dicOfGraphData = SelectedProfile.DicOfPosition;
                
            }
            else if(strName == "Velocity")
            {
                strSendName = strName + ": " + nIndex.ToString();
                dicOfGraphData = SelectedProfile.DicOfVelocity;

            }
            Main_ui.DisplayMotionProfileData(strSendName, dicOfGraphData);
        }
        
        #endregion

    }
}
