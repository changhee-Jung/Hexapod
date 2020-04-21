using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seq;
namespace Hexapod
{
    class Hardware
    {
        public enum HardwareState
        {
            None,
            Setcompleted_Base,
            SetCompleted_Upper,
            SetCompleted_Height,
            SetTargetPose,
            Actionable,
        }
        #region 생성자
        public Hardware()
        {

        }
        #endregion

        #region 멤버
        Plate m_Plate_Base;
        Plate m_Plate_Upper;
        HardwareState m_HardwareState = HardwareState.None;
        DataEventArgs m_Args = null;
        double[] m_dbToolOffset;
        #endregion

        #region 속성
        public Plate Plate_Base { get { return m_Plate_Base; } }
        public Plate Plate_Upper { get { return m_Plate_Upper; } }
        public double[] ToolOffset { get { return m_dbToolOffset; } set { m_dbToolOffset = value; } }
        public HardwareState State { get { return m_HardwareState; } set { m_HardwareState = value; } }
        #endregion

        #region 메소드

        public void SetEventData(SetData setData, DataEventArgs args)
        {
            if(args != null)
            {
                m_Args = args;
                if (setData == SetData.HardWareData)
                {
                    m_HardwareState = HardwareState.None;
                }
                else if(setData == SetData.TargetCoordinate)
                {
                    m_HardwareState = HardwareState.SetTargetPose;
                }
            }

        }
        public void Update()
        {
            if (m_Args == null) { return; }
            try
            {
                switch (m_HardwareState)
                {
                    case HardwareState.None:
                        if (m_Args.Design.nNumberOfJoint <= 0 || m_Args.Design.dbRadius_Base <= 0 || m_Args.Design.dbAngleOfOffset_Base <= 0) { break; }

                        m_Plate_Base = new Plate(Plate.PlateSettings.Base, m_Args.Design.nNumberOfJoint, m_Args.Design.dbRadius_Base, m_Args.Design.dbAngleOfOffset_Base);
                        m_Plate_Base.MakeJointVector();
                        m_HardwareState = HardwareState.Setcompleted_Base;
                        break;
                    case HardwareState.Setcompleted_Base:
                        m_Plate_Upper = new Plate(Plate.PlateSettings.Upper, m_Args.Design.nNumberOfJoint, m_Args.Design.dbRadius_Upper, m_Args.Design.dbAngleOfOffset_Upper);
                        m_Plate_Upper.MakeJointVector();
                        m_HardwareState = HardwareState.SetCompleted_Upper;
                        break;
                    case HardwareState.SetCompleted_Upper:
                        m_Plate_Upper.ToolOffset = m_Args.Design.ar_dbToolOffset;
                        m_HardwareState = HardwareState.SetCompleted_Height;
                        break;
                    case HardwareState.SetCompleted_Height:
                        m_Plate_Upper.Height = m_Args.Design.dbHeight;
                        m_HardwareState = HardwareState.SetTargetPose;
                        break;
                    case HardwareState.SetTargetPose:
                        if (m_Args.Target.dbTargetPosition == null || m_Args.Target.dbTargetRotation == null) { break; }
                        
                        m_Plate_Upper.CalculateTranslationVector(m_Args.Target.dbTargetPosition);
                        m_Plate_Upper.Rotation = m_Args.Target.dbTargetRotation;
                        m_HardwareState = HardwareState.Actionable;
                        break;
                    case HardwareState.Actionable:
                        m_Args = null;
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

      
        #endregion
    }

}
