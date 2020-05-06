using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hexapod;
using System.Diagnostics;
namespace Seq
{
    public enum SetData
    {
        HardWareData,
        TargetCoordinate,
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

    public class DataEventArgs : EventArgs
    {
        public DesignData Design;
        public TargetData Target;

        public SetData RegisterData;

        public string strName;
        public int nIndex;
    }

    class ConMotion : Controller
    {
        enum ControlState
        {
            none,
            SetHardware,
            SetVector,
            Wait,
            SetMotionProfile,
            Actionable
        }

        #region 생성자
        public ConMotion(Main_UI _Main_ui, Hardware _Hardware, Vector _Vector, MotionProfile Motion)
        {
            hardware = _Hardware;
            vector = _Vector;
            motionProfile = Motion;
            Main_ui = _Main_ui;            
        }
        #endregion

        #region 멤버
        Hardware hardware = null;
        Vector vector = null;
        MotionProfile motionProfile = null;
        Main_UI Main_ui = null;
        ControlState m_State = ControlState.none;
        DataEventArgs DataArgs = null;
        #endregion    

        #region 메소드

        public void DataSet_Updated(object obj, DataEventArgs arg)
        {
            DataArgs = arg;
        }

        public void SetDataProcess()
        {
            DataEventArgs TempArgs = null;
            if (DataArgs != null)
            {
                TempArgs = DataArgs;
                DataArgs = null;
            }
            else { return; }

            if (TempArgs == null) { return; }
            switch (TempArgs.RegisterData)
            {
                case SetData.HardWareData:
                    hardware.SetEventData(SetData.HardWareData, TempArgs);
                    vector.InitializeState();
                    m_State = ControlState.SetHardware;
                    break;
                case SetData.TargetCoordinate:
                    hardware.SetEventData(SetData.TargetCoordinate, TempArgs);
                    vector.InitializeState();
                    m_State = ControlState.SetVector;
                    break;
                case SetData.CalculateMovingVector:
                    m_State = ControlState.SetMotionProfile;           
                    motionProfile.InitializeState();
                    break;
            }

        }
        public override void Update()
        {
            switch (m_State)
            {
                case ControlState.none:
                
                    break;

                case ControlState.SetHardware:
                    hardware.Update();
                    if (hardware.State == Hardware.HardwareState.Actionable)
                    {
                        m_State = ControlState.SetVector;
                    }
                    break;
                case ControlState.SetVector:
                    hardware.Update();
                    vector.Update(hardware);
                    if (vector.Procedure == Vector.SettingProcedure.SetCompletedActuatorLengths)
                    {
                        m_State = ControlState.Wait;
                        DisplayActuatorVector();
                    }
                    break;

                case ControlState.Wait:    
                    break;

                case ControlState.SetMotionProfile:
                    motionProfile.Update(vector.TargetLenghsOfActuator);
                    
                    if (motionProfile.State == MotionProfile.MotionState.CompletedMotionProfile)
                    {
                       DisplayMotionProfileGraph();
                       m_State = ControlState.Actionable;           
                    }
                    break;
                case ControlState.Actionable:
                    break;

            }
        }

        private void DisplayActuatorVector()
        {
            Main_ui.Invoke(new Action(
                                   delegate()
                                   {
                                       Main_ui.DisplayDateGridView(vector.BaseToTargetVector, vector.TargetLenghsOfActuator);
                                   }));
        }

        private void DisplayMotionProfileGraph()
        {
            List<string> listOfProfileItemsName = new List<string>();
            Main_ui.Invoke(new Action(
                             delegate()
                             {
                                 Main_ui.InitializeChartControl();
                             }));
            for (int nIndex = 0; nIndex < motionProfile.NumberOfAxis; nIndex++)
            {
                listOfProfileItemsName.Add("Axis: " + nIndex.ToString());
                Profile profile = motionProfile.GetAxisProfile(nIndex);

                Main_ui.Invoke(new Action(
                               delegate()
                               {
                                   Main_ui.DisplayMotionProfileData(nIndex, "Position", profile.DicOfPosition_MovingAverage);
                                   Main_ui.DisplayMotionProfileData(nIndex, "Velocity", profile.DicOfVelocity_MovingAverage);
                                   Main_ui.DisplayMotionProfileData(nIndex, "Acceleration", profile.DicOfAcceleration_MovingAverage);
                                   Main_ui.SetcomboSelectItem(listOfProfileItemsName);
                               }));
            }
        }

    
        #endregion

    }
}
