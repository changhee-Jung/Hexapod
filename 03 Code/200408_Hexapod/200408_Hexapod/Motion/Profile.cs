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
        private double m_nInterval   = 1000;
        private Motor m_Motor;
        private Dictionary<int, double> m_dicOfPosition = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicOfVelocity = new Dictionary<int, double>();

        public double MaxAccel { get { return m_dbMaxAccel; } set { m_dbMaxAccel = value; } }
        public bool bIsArrive { get { return m_bIsArrive; } }
        public Motor Motor { get { return m_Motor; } }

        public Dictionary<int, double>  DicOfVelocity { get { return m_dicOfVelocity; } }
        public Dictionary<int, double>  DicOfPosition { get { return m_dicOfPosition; } }
        public Profile(Motor motor)
        {
            m_Motor = motor;
        }

        public void CalculatePositionProfile(int nTickTime, int nEndTime, double dbStartPosition) //EndTime 때 위치값이 동일해야 한다.
        {
            if(false == m_bIsArrive)
            {
                // 1. ms로 운영
                double dbTickTime  = nTickTime * 0.001;
                double dbEndTime   = nEndTime * 0.001;
                double dbAccelTime = m_Motor.AccelPercent * dbEndTime / 2;
                double dbDecelTime = (2 - m_Motor.DecelPercent) * dbEndTime / 2;
                double dbNextPosition = 0;

                // 2. 사다리꼴 프로파일 생성
                // 3. 위치 변환
                if (dbTickTime < dbAccelTime)
                {
                    dbNextPosition = (m_Motor.MaxAccel * dbTickTime * dbTickTime) * 0.5 + dbStartPosition;
                }
                else if (dbTickTime < dbDecelTime)
                {
                    dbNextPosition = (m_Motor.MaxAccel * dbAccelTime * dbAccelTime) * 0.5 + dbStartPosition
                                    + m_Motor.MaxAccel * dbAccelTime * (dbTickTime - dbAccelTime);
                }
                else
                {
                    dbNextPosition = (m_Motor.MaxAccel * dbAccelTime * dbAccelTime) * 0.5 + dbStartPosition
                                    + m_Motor.MaxAccel * dbAccelTime * (dbDecelTime - dbAccelTime) 
                                    + m_Motor.MaxAccel * dbAccelTime * (dbTickTime - dbDecelTime)
                                    - 0.5 * (m_Motor.MaxAccel * dbAccelTime / (dbEndTime - dbDecelTime)) * (dbTickTime - dbDecelTime) * (dbTickTime - dbDecelTime);
                }

                m_dicOfPosition.Add(nTickTime, dbNextPosition);
                
                

                if (Math.Abs(dbNextPosition) >= Math.Abs(Motor.TargetPosition))
                {
                    CalculateVelocityProfile();
                    m_bIsArrive = true;
                }
            }
                   
        }

        public void CalculateVelocityProfile()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            for(int i = 1; i < m_dicOfPosition.Count; i++)
            {
                double dbVelocity = (m_dicOfPosition[i] - m_dicOfPosition[i - 1]) / dbInterval; 
                m_dicOfVelocity.Add(i - 1, dbVelocity);
            }      
        }
        
    }
}
// 참고 :https://gammabeta.tistory.com/593