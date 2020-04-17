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
    }
    public struct DesignData
    {
        public int nNumberOfJoint;
        public double dbHeight;
        public double dbRadius_Base;
        public double dbAngleOfOffset_Base;
        public double dbRadius_Upper;
        public double dbAngleOfOffset_Upper;
        public double[] ar_dbToolOffset;
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
            }
        }

        private void SetHardware(DataEventArgs e)
        {
            InitializeState(); // 상태 설정 초기화

            int nNumOfJoint              = e.Design.nNumberOfJoint;
            double dbRadius_Base         = e.Design.dbRadius_Base;
            double dbAngleOfOffset_Base  = e.Design.dbAngleOfOffset_Base;
            double dbRadius_Upper        = e.Design.dbRadius_Upper;
            double dbAngleOfOffset_Upper = e.Design.dbAngleOfOffset_Upper;
            double[] ar_dbToolOffset     = e.Design.ar_dbToolOffset;

            Model_Hw.MakeHexapodPlate(nNumOfJoint, dbRadius_Base, dbAngleOfOffset_Base, dbRadius_Upper, dbAngleOfOffset_Upper);
            Model_Hw.Plate_Upper.ToolOffset = ar_dbToolOffset;

            Model_Motion.NumberOfAxis = nNumOfJoint;
        }

        private void SetHeightVector(DataEventArgs e)
        {
            double dbHeight                 = e.Design.dbHeight;
            Model_Hw.Plate_Upper.Height = dbHeight;
            Model_Hw.State = Hardware_Model.HardwareState.Actionable;
        }

        private void SetTargetCoordinate(DataEventArgs e)
        {
            InitializeState();
            if (Model_Hw.State != Hardware_Model.HardwareState.Actionable) { return; }
            Model_Hw.Plate_Upper.CalculateTranslationVector(e.Target.dbTargetPosition);
            Model_Hw.Plate_Upper.Rotation = e.Target.dbTargetRotation;
        }

        private void CalculateMovingVector()
        {
            InitializeState();
            if (Model_Hw.State != Hardware_Model.HardwareState.Actionable) { return; }
            // 1. base to Upper point height vector 계산
            Model_Coordinate.SetBasetoHeightVector(Model_Hw.Plate_Upper.Height);

            // 2. baseJoint to UpperPlate Vector 위치 벡터 계산
            Model_Coordinate.CalculateBaseToUpperPlateVector(Model_Hw.Plate_Base.dicOfJointVector);

            // 3. base에서 바라본 Upperjoint vector 계산(회전 행렬)
            Model_Coordinate.CalculateBaseToUpperJoint_Rotation(Model_Hw.Plate_Upper.Rotation, Model_Hw.Plate_Upper.dicOfJointVector);

            // 4. base에서 바라본 BaseJoint와 UpperJoint 사이의 벡터 계산
            Model_Coordinate.CalculateBaseJointToUpperJointVector();

            // 5.1 보상할 병진 행렬 계산(목표 위치)
            Model_Coordinate.CalculateTargetPostionTranslation(Model_Hw.Plate_Upper.Position);
            // 5.2 보상할 병진 행렬 계산(ToolOffset) 위치
            Model_Coordinate.CompensateToolOffsetVector(Model_Hw.Plate_Upper.Rotation, Model_Hw.Plate_Upper.ToolOffset);

            // 6. 목표 엑추에이터 값 계산
            Model_Coordinate.CalculateActuatorLengths();

            // 7. 해당 엑추에이터에 위치 등록
            SetMotionData(Model_Coordinate.TargetLenghsOfActuator);

            m_bIsSetAllHardWare = true;

            // 8. 계산 값 표시
            DisplayActuatorVector();
        }

        private void DisplayActuatorVector()
        {
            Main_ui.DisplayDateGridView(Model_Coordinate.BaseToTargetVector, Model_Coordinate.TargetLenghsOfActuator);
        }

        private void SetMotionData(Dictionary<int,double> TargetLengths)
        {
            // 프로파일 초기화
            Model_Motion.InitializeState();   

            // 1. 모션 축 생성 및 초기화
            Model_Motion.SetMotorAxis(TargetLengths.Count);

            // 2. 목표 위치 설정
            Model_Motion.SetTargetPosition(TargetLengths);

        }

        public void CalculateNextStepMotion(int nTicktime, int nCycleTime)
        {
            if (true == Model_Motion.CheckCompleteMotionProfiles())
            {
                List<string> listOfProfileItemsName = new List<string>();
                Main_ui.Invoke(new Action(
                                 delegate()
                                 {
                                     Main_ui.InitializeChartControl();
                                 }));
                for (int nIndex = 0; nIndex < Model_Motion.NumberOfAxis; nIndex++)
                {
                    listOfProfileItemsName.Add("Axis: " + nIndex.ToString());
                    Profile profile = Model_Motion.GetAxisProfile(nIndex);

                    Main_ui.Invoke(new Action(
                                   delegate()
                                   {
                                       Main_ui.DisplayMotionProfileData(nIndex,"Position", profile.DicOfPosition);
                                       Main_ui.DisplayMotionProfileData(nIndex, "Velocity", profile.DicOfVelocity_MovingAverage); //200417 확인 완료
                                       Main_ui.DisplayMotionProfileData(nIndex, "Acceleration", profile.DicOfAcceleration);
                                       Main_ui.SetcomboSelectItem(listOfProfileItemsName);
                                   }));
                }              
                m_bIsMakeAllProfile = true;
            }
            else
            {
                Model_Motion.SetCycleTime(nCycleTime);
                Model_Motion.CalculateRequiredVelocity();
                Model_Motion.MakeMotionProfile(nTicktime);              
            }          
        }
        
        #endregion

    }
}
