using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace _200408_Hexapod
{
    class Profile
    {
        private bool m_bIsArrive     = false;
        private double m_dbMaxAccel = 0;
        private int m_nInterval     = 1000;
        private int m_nCycletime    = 1000;
        private Motor m_Motor;
        private Dictionary<int, double> m_dicOfPosition               = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicOfVelocity               = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicOfAcceleration           = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicOfVelocity_MovingAverage = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicPosition_MovingAverage   = new Dictionary<int, double>();
        private Dictionary<int, double> m_dicAcceleration_MovingAverage = new Dictionary<int, double>();

        public bool bIsArrive { get { return m_bIsArrive; } set { m_bIsArrive = value; } }
        public int Interval { get { return m_nInterval; } set { m_nInterval = value; } }
        public int CycleTime { get { return m_nCycletime; } set { m_nCycletime = value; } }
        public double MaxAccel { get { return m_dbMaxAccel; } set { m_dbMaxAccel = value; } }
  
        public Motor Motor { get { return m_Motor; } }
        public Dictionary<int, double> DicOfPosition { get { return m_dicOfPosition; } }
        public Dictionary<int, double> DicOfVelocity { get { return m_dicOfVelocity; } }
        public Dictionary<int, double> DicOfAcceleration { get { return m_dicOfAcceleration; } }
        public Dictionary<int, double> DicOfVelocity_MovingAverage { get { return m_dicOfVelocity_MovingAverage; } }
        public Dictionary<int, double> DicOfPosition_MovingAverage { get { return m_dicPosition_MovingAverage; } }
        public Dictionary<int, double> DicAcceleration_MovingAverage { get { return m_dicAcceleration_MovingAverage; } }


        private double[] m_ardbVelocity = new double[300];
        Stopwatch sw = new Stopwatch();

        public Profile(Motor motor)
        {
            m_Motor = motor;
        }

        public void CalculateRequiredVelocity()
        {
            double dbEndtime = m_nCycletime * 0.001;
            double dbAccelPercent = m_Motor.AccelPercent * 0.5;
            double dbDecelPercent = m_Motor.DecelPercent * 0.5;
            double dbVelocity = 2 * m_Motor.TargetPosition / ((1 + (1 - dbAccelPercent - dbDecelPercent)) * dbEndtime); //mm/s
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
                double dbEndTime = m_nCycletime * 0.001;
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
                if (m_dicOfPosition.ContainsKey(nTickTime))
                {
                    m_dicOfPosition.Clear();
                    m_dicOfVelocity.Clear();
                }
                m_dicOfPosition.Add(nTickTime, dbNextPosition);

                if (dbTickTime >= dbEndTime)
                {
                    // 사다리꼴 프로파일 계산
                    CalculateVelocityProfile();
                    CalculateAccelerationProfile();
                    // 이동 평균 사다리꼴 프로파일 계산
                    CalculateVelocityProfile_MovingAverage();
                    CalculatePositionProfile_MovingAverage();
                    CalculateAccelerationProfile_MovingAverage();
                    // 소숫점 자리 계산
                    CalculateDigitData(m_dicPosition_MovingAverage);
                    CalculateDigitData(m_dicOfVelocity_MovingAverage);
                    CalculateDigitData(m_dicAcceleration_MovingAverage);
                    m_bIsArrive = true;
                }
            }                  
        }

        /// <summary>
        /// 2020.04.14 by chjung [ADD] 위치값을 기반으로 속도 프로파일을 생성한다.
        /// </summary>
        public void CalculateVelocityProfile()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            for(int i = 0; i < m_dicOfPosition.Count - 1; i++)
            {
                double dbVelocity = (m_dicOfPosition[i + 1] - m_dicOfPosition[i]) / dbInterval; // mm/ms
                dbVelocity = dbVelocity * 1000; // mm/s
                m_dicOfVelocity.Add(i , dbVelocity);
            }      
        }
        /// <summary>
        /// 2020.04.16 by chjung [ADD] 위치 값을 기반으로 가속도 프로파일을 생성한다.
        /// </summary>
        public void CalculateAccelerationProfile()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            for (int i = 0; i < m_dicOfVelocity.Count - 1; i++)
            {
                double dbAcceleration = (m_dicOfVelocity[i + 1] - m_dicOfVelocity[i]) / dbInterval;
                dbAcceleration = dbAcceleration * 1000; // mm/s^2
                m_dicOfAcceleration.Add(i, dbAcceleration);
            }      
            
        }
        /// <summary>
        /// 2020.04.17 by chjung [ADD] 이동 평균 속도 프로파일을 생성한다.
        /// </summary>
        public void CalculateVelocityProfile_MovingAverage()
        {
            if (m_dicOfVelocity.Count <= 0 || m_ardbVelocity.Length <= 0) { return; }
            if (m_dicOfVelocity.Count <= m_ardbVelocity.Length) { return; }
            bool bIsCompletedCalculation = false;
            int nIndex = 0;

            while(false == bIsCompletedCalculation)
            {
                if(nIndex < m_ardbVelocity.Length)
                {
                    m_ardbVelocity[nIndex] = m_dicOfVelocity[nIndex];
                }
                else
                {
                    for (int nIndexOfArray = 0; nIndexOfArray < m_ardbVelocity.Length - 1; nIndexOfArray++)
                    {
                        m_ardbVelocity[nIndexOfArray] = m_ardbVelocity[nIndexOfArray + 1];
                    }
                    if(true == m_dicOfVelocity.ContainsKey(nIndex))
                    {
                        m_ardbVelocity[m_ardbVelocity.Length - 1] = m_dicOfVelocity[nIndex];
                    }
                    else
                    {
                        m_ardbVelocity[m_ardbVelocity.Length - 1] = 0;
                    }
                }
                double dbNextVelocity = 0;
                foreach (double dbData in m_ardbVelocity)
                {
                    dbNextVelocity += dbData / m_ardbVelocity.Length;
                }
                m_dicOfVelocity_MovingAverage.Add(nIndex, dbNextVelocity);

                if(dbNextVelocity <= 0) { break;}
                nIndex++;
            }
        }
        /// <summary>
        /// 2020.04.20 by chjung [ADD] 이동 평균 속도 프로파일을 통해 위치 프로파일을 계산한다.
        /// </summary>
        public void CalculatePositionProfile_MovingAverage()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            double dbPosition_MovingAverage = 0;
            for (int i = 0; i < m_dicOfVelocity_MovingAverage.Count - 1; i++)
            {
                dbPosition_MovingAverage += 0.001 * (m_dicOfVelocity_MovingAverage[i] + m_dicOfVelocity_MovingAverage[i + 1]) * 0.5 * dbInterval;
                m_dicPosition_MovingAverage.Add(i, dbPosition_MovingAverage);
            }      
        }
        /// <summary>
        /// 2020.04.20 by chjung [ADD] 이동 평균 속도 프로파일을 통해 가속도 프로파일을 계산한다.
        /// </summary>
        public void CalculateAccelerationProfile_MovingAverage()
        {
            double dbInterval = (double)0.001 * m_nInterval;
            if (dbInterval == 0) { return; }
            m_dicAcceleration_MovingAverage[0] = 0;
            for (int i = 1; i < m_dicOfVelocity_MovingAverage.Count; i++)
            {
                double dbAcceleration = (m_dicOfVelocity_MovingAverage[i] - m_dicOfVelocity_MovingAverage[i - 1]) / dbInterval;
                dbAcceleration = dbAcceleration * 1000; // mm/s^2
                m_dicAcceleration_MovingAverage.Add(i, dbAcceleration);
            }      
        }

        public void CalculateDigitData(Dictionary<int,double> DicOfData)
        {
            if (DicOfData.Count <= 0) { return; }

            for(int nIndex = 0; nIndex < DicOfData.Count - 1; nIndex++)
            {
                DicOfData[nIndex] = Math.Truncate(DicOfData[nIndex] * 1000) * 0.001;
            }
        }
      
        
    }
}
// 참고 :https://gammabeta.tistory.com/593