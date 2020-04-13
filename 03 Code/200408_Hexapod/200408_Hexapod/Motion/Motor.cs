using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Motor
    {
        #region 멤버
        private double m_dbPosition       = 0;
        private double m_dbTargetPosition = 0;
        private double m_dbMaxAccel       = 1.223;
        private double m_dbMaxVel         = 1;
        private double m_dbAccelPercent   = 0.5;
        private double m_dbDecelPercent   = 0.5;

        #endregion

        #region 속성
        public double Position { get { return m_dbPosition; } set { m_dbPosition = value; } }
        public double TargetPosition { get { return m_dbTargetPosition; } set { m_dbTargetPosition = value; } }
        public double MaxAccel { get { return m_dbMaxAccel; } set { m_dbMaxAccel = value; } }
        public double MaxVel { get { return m_dbMaxVel; } set { m_dbMaxVel = value; } }
        public double AccelPercent { get { return m_dbAccelPercent; } set { m_dbAccelPercent = value; } }
        public double DecelPercent { get { return m_dbDecelPercent; } set { m_dbDecelPercent = value; } }
        #endregion

        #region 생성자
        public Motor()
        {

        }
        #endregion

    }
}
