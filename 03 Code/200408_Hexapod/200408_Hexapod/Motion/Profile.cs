using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Profile
    {
        private bool m_bIsArrive     = false;
        private double m_dbMaxAccel = 0;
        private double m_nInterval  = 1000;
        private Motor m_Motor;
        private Dictionary<int, double> m_dicOfPosition = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicOfVelocity = new Dictionary<int, double>();

        public bool bIsArrive { get { return m_bIsArrive; } set { m_bIsArrive = value; } }
        public double MaxAccel { get { return m_dbMaxAccel; } set { m_dbMaxAccel = value; } }
        public double Interval { get { return m_nInterval; } set { m_nInterval = value; } }
        public Motor Motor { get { return m_Motor; } }
        public Dictionary<int, double>  DicOfVelocity { get { return m_dicOfVelocity; } }
        public Dictionary<int, double>  DicOfPosition { get { return m_dicOfPosition; } }

        public Profile(Motor motor)
        {
            m_Motor = motor;
        }

        public void CalculateRequiredVelocity()
        {
            double dbInterval = m_nInterval * 0.001;
            double dbAccelPercent = m_Motor.AccelPercent * 0.5;
            double dbDecelPercent = m_Motor.DecelPercent * 0.5;
            double dbVelocity = 2 * m_Motor.TargetPosition / (1 + (1 - dbAccelPercent - dbDecelPercent)) * dbInterval;
            m_Motor.MaxVel = dbVelocity;
        }
        /// <summary>
        /// 2020.04.14 by chjung [ADD] 사다리꼴 위치 프로파일을 계산한다.
        /// </summary>
        public void CalculatePositionProfile(int nTickTime, double dbStartPosition)
        {
            if(false == m_bIsArrive)
            {
                // 1. ms로 운영
                double dbTickTime  = nTickTime * 0.001;
                double dbEndTime = m_nInterval * 0.001;
                double dbAccelTime = m_Motor.AccelPercent * dbEndTime / 2;
                double dbDecelTime = (2 - m_Motor.DecelPercent) * dbEndTime / 2;
                double dbNextPosition = 0;

                // 2. 위치 계산
                if (dbTickTime < dbAccelTime)
                {
                    dbNextPosition = 0.5 * (m_Motor.MaxVel / dbAccelTime) * dbTickTime * dbTickTime + dbStartPosition;
                }
                else if (dbTickTime < dbDecelTime)
                {
                    dbNextPosition = 0.5 * (m_Motor.MaxVel / dbAccelTime) * dbAccelTime * dbAccelTime + dbStartPosition
                                     + m_Motor.MaxVel * (dbTickTime - dbAccelTime);
                }
                else
                {
                    dbNextPosition = 0.5 * (m_Motor.MaxVel / dbAccelTime) * dbAccelTime * dbAccelTime + dbStartPosition
                                     + m_Motor.MaxVel * (dbDecelTime - dbAccelTime)
                                     + m_Motor.MaxVel * (dbTickTime - dbDecelTime)
                                     - 0.5 * (m_Motor.MaxVel / (dbEndTime - dbDecelTime)) * (dbTickTime - dbDecelTime) * (dbTickTime - dbDecelTime);
                }
                if(m_dicOfPosition.ContainsKey(nTickTime))
                {
                    m_dicOfPosition.Clear();
                    m_dicOfVelocity.Clear();
                }
                m_dicOfPosition.Add(nTickTime, dbNextPosition);
          
                if(nTickTime >= m_nInterval)
                {
                    CalculateVelocityProfile();
                    m_bIsArrive = true;
                }
            }                  
        }

        /// <summary>
        /// 2020.04.14 by chjung [ADD] 위치값을 기반으로 속도 프로파일을 계산한다.
        /// </summary>
        public void CalculateVelocityProfile()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            for(int i = 0; i < m_dicOfPosition.Count - 1; i++)
            {
                double dbVelocity = (m_dicOfPosition[i + 1] - m_dicOfPosition[i]) / dbInterval; // m/ms
                dbVelocity = dbVelocity * 1000; // m/s
                m_dicOfVelocity.Add(i , dbVelocity);
            }      
        }
        
    }
}
// 참고 :https://gammabeta.tistory.com/593