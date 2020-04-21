using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapod
{

    class MotionProfile
    {
        public enum MotionState
        {
            none,
            SetCompletedInitializeState,
            SetCompletedRequiredVelocity,
            SetCompletedPositionProfile,
            SetCompletedVelocityProfile,
            SetCompletedAccelerationProfile,
            SetCompletedMovingAverage_Profile,
            SetCompletedDecimalPointRounding,
            SetCompletedMotionProfile
        }

        #region 멤버
        MotionState m_State = MotionState.none;
        double[] dbTargetLength;
        int m_nNumberOfAxis = 0;
        int m_nCycleTime    = 0;
        int m_nTickTime     = 0;
        bool m_bIsSuccess          = false;      
        Dictionary<int, Motor> m_dicOfMotor;
        Dictionary<int, Profile> m_dicOfProfile;
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
            switch(m_State)
            {
                case MotionState.none:
                    SetMotorAxis(TargetLengths.Count);
                    SetTargetPosition(TargetLengths);
                    m_State = MotionState.SetCompletedInitializeState;
                    break;

                case MotionState.SetCompletedInitializeState:
                    SetCycleTime(m_nCycleTime);
                    CalculateRequiredVelocity();
                    m_nTickTime = 0;
                    m_State = MotionState.SetCompletedRequiredVelocity;
                    break;

                case MotionState.SetCompletedRequiredVelocity:
                    CalcuatePositionProfile(m_nTickTime);
                    if (true == CheckCompleteMotionProfiles())
                    {
                        m_nTickTime = 0;
                        m_State = MotionState.SetCompletedMotionProfile;                
                    }
                    m_nTickTime++;
                    break;

                case MotionState.SetCompletedPositionProfile:
                    CalcuateVelocityProfile();

                    break;

                case MotionState.SetCompletedVelocityProfile:
                    CalcuateAccelerationProfile();
                    break;

                case MotionState.SetCompletedAccelerationProfile:
                    break;

                case MotionState.SetCompletedMovingAverage_Profile:
                    break;

                case MotionState.SetCompletedDecimalPointRounding:
                    break;

                case MotionState.SetCompletedMotionProfile:
                    break;

            }

        }


        public void InitializeState()
        {
            if (m_dicOfProfile.Count <= 0) { return; }
 
            for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++ )
            {
                Profile SelectedProfile = m_dicOfProfile[nIndex];
                SelectedProfile.bIsArrive = false;
            }
            m_State = MotionState.none;
        }
        
        public void SetCycleTime(int nCycleTime)
        {
            if (m_nCycleTime != nCycleTime)
            {
                m_nCycleTime = nCycleTime;

                for (int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
                {
                    Profile profile = m_dicOfProfile[nIndex];
                    profile.CycleTime = nCycleTime;
                }
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
            dbTargetLength = new double[nNumberOfAxis];
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

        public bool CheckCompleteMotionProfiles()
        {
            bool bIsComplete = true;

            for(int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
            {
                bIsComplete &= m_dicOfProfile[nIndex].bIsArrive;
            }
            m_bIsSuccess = bIsComplete;

            return m_bIsSuccess;
        }

        #endregion
    }
}
