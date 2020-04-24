using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Hexapod
{

    class MotionProfile
    {
        public enum MotionState
        {
            none,
            SetHardwareCondition,
            SetRequiredVelocity,
            SetPositionProfile,
            SetTrapezoidalProfile,
            SetMovingAverage_Profile,
            SetDecimalPointRounding,
            CompletedMotionProfile
        }

        #region 멤버
        MotionState m_State = MotionState.none;
        int m_nNumberOfAxis = 0;
        int m_nCycleTime    = 1000;
        int m_nTickTime     = 0;
        bool m_bIsSuccess          = false;      
        Dictionary<int, Motor> m_dicOfMotor;
        Dictionary<int, Profile> m_dicOfProfile;
        Stopwatch sw = new Stopwatch();
        #endregion
        
        #region 속성
        public int NumberOfAxis { get { return m_nNumberOfAxis; } set { m_nNumberOfAxis = value; } }
        public MotionState State { get { return m_State; } }
        #endregion

        #region 생성자
        public MotionProfile()
        {
            m_dicOfMotor = new Dictionary<int, Motor>();
            m_dicOfProfile = new Dictionary<int, Profile>();
        }
        #endregion

        #region 메소드

        public void Update(Dictionary<int, double> TargetLengths) // 여기서 Position, Velocity, 소숫점 계산
        {
            switch (m_State)
            {
                case MotionState.none:
                    break;

                case MotionState.SetHardwareCondition:
                    sw.Restart();
                    SetMotorAxis(TargetLengths.Count);
                    SetTargetPosition(TargetLengths);
                    SetCycleTime(m_nCycleTime);
                    m_State = MotionState.SetRequiredVelocity;
                    break;

                case MotionState.SetRequiredVelocity:
                    CalculateRequiredVelocity();
                    m_nTickTime = 0;
                    m_State = MotionState.SetPositionProfile;
                    break;

                case MotionState.SetPositionProfile:
                    CalcuatePositionProfile(m_nTickTime);
                    bool bIsEndCalculatePosition = CheckCompleteMotionProfiles();
                    if (true == bIsEndCalculatePosition)
                    {
                        m_nTickTime = 0;
                        m_State = MotionState.SetTrapezoidalProfile;
                        sw.Stop();
                        Console.WriteLine("Time: " + sw.ElapsedMilliseconds.ToString() + "msec");
                    }
                    else
                    {
                        m_nTickTime++;
                    }
                    break;

                case MotionState.SetTrapezoidalProfile:
                    CalcuateVelocityProfile();
                    CalcuateAccelerationProfile();      
                    m_State = MotionState.SetMovingAverage_Profile;
                    break;

                case MotionState.SetMovingAverage_Profile:
                    CalculateMovingAverageProfile();
                    m_State = MotionState.SetDecimalPointRounding;
                    break;

                case MotionState.SetDecimalPointRounding:
                    CalculateDecimalPointRounding();
                    m_State = MotionState.CompletedMotionProfile;

                    break;

                case MotionState.CompletedMotionProfile:
                  
                    break;
            }

        }


        public void MakeMotionProfile(int nTicktime)
        {
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculatePositionProfile(nTicktime, profile.Motor.Position);

            }
        }




        public void InitializeState()
        {
            m_State = MotionState.SetHardwareCondition;
        }
        
        public void SetCycleTime(int nCycleTime)
        {
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CycleTime = nCycleTime;
            }            
        }

        public Profile GetAxisProfile(int nIndex)
        {
            Profile returnProfile = null;

            if(m_dicOfProfile.ContainsKey(nIndex))
            {
                returnProfile = m_dicOfProfile[nIndex];
            }
            return returnProfile;
        }

        public void SetMotorAxis(int nNumberOfAxis)
        {
            m_nNumberOfAxis = nNumberOfAxis;
            m_dicOfMotor.Clear();
            m_dicOfProfile.Clear();
            for (int nIndex = 0; nIndex < nNumberOfAxis; nIndex++)
            {          
                Motor motor = new Motor();
                m_dicOfMotor.Add(nIndex, motor);
                m_dicOfProfile.Add(nIndex, new Profile(motor));
            }
        }

        public void SetTargetPosition(Dictionary<int, double> TargetLengths)
        {
            for (int nIndex = 0; nIndex < TargetLengths.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                Motor motor = profile.Motor;
                // 현재값 기반으로 이동해야할 위치 등록(절대값)
                motor.TargetPosition = Math.Abs(TargetLengths[nIndex] - motor.Position);
                
            }
        }

        public void CalculateRequiredVelocity()
        {         
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculateRequiredVelocity();
            }
        }

        public void CalcuatePositionProfile(int nTicktime)
        {
           for(int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
           {
               Profile profile = m_dicOfProfile[nIndex];
               profile.CalculatePositionProfile(nTicktime, profile.Motor.Position);
             
           }
        }
        public void CalcuateVelocityProfile()
        {
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
               // m_dicOfProfile[nIndex].CalculateVelocityProfile();
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculateVelocityProfile();

            }
        }
        public void CalcuateAccelerationProfile()
        {
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculateAccelerationProfile();

            }
        }
        public void CalculateMovingAverageProfile()
        {
            for(int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculateVelocityProfile_MovingAverage();
                profile.CalculatePositionProfile_MovingAverage();
                profile.CalculateAccelerationProfile_MovingAverage();
            }
        }

        public void CalculateDecimalPointRounding()
        {
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                Profile profile = m_dicOfProfile[nIndex];
                profile.CalculateDigitData(profile.DicOfPosition_MovingAverage);
                profile.CalculateDigitData(profile.DicOfVelocity_MovingAverage);
                profile.CalculateDigitData(profile.DicOfAcceleration_MovingAverage);
            }

        }
        public bool CheckCompleteMotionProfiles()
        {
            bool bIsComplete = true;

            for(int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                if (false == m_dicOfProfile[nIndex].bIsArrive)
                    bIsComplete = false;
            }
            m_bIsSuccess = bIsComplete;
            return bIsComplete;
        }

        #endregion
    }
}
