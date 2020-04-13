using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{

    class Motion_Model
    {
        #region 멤버

        double[] dbTargetLength;
        int m_nNumberOfAxis = 0;
        bool m_bIsSuccess = false;
        Dictionary<int, Motor> m_dicOfMotor;
        Dictionary<int, Profile> m_dicOfProfile;
        #endregion
        
        #region 속성

        public bool IsSuccess { get { return m_bIsSuccess; } }
        public int NumberOfAxis { get { return m_nNumberOfAxis; } set { m_nNumberOfAxis = value; } }
        #endregion

        #region 메소드

        public Motion_Model()
        {
            m_dicOfMotor   = new Dictionary<int, Motor>();
            m_dicOfProfile = new Dictionary<int, Profile>();
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
                motor.TargetPosition = TargetLengths[nIndex];
                
            }
        }

        public void MakeMotionProfile(int nTicktime,int nEndtime)
        {
           for(int nIndex = 0; nIndex < m_dicOfProfile.Count; nIndex++)
           {
               Profile profile = m_dicOfProfile[nIndex];
               profile.CalculatePositionProfile(nTicktime, nEndtime, profile.Motor.Position);
             
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
