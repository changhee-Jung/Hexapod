using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seq
{
    class TickCount
    {
        public TickCount()
        {

        }

        private long m_lElapsedTickCount = 0;
        private DateTime StartTime = new DateTime();
        private DateTime StopTime = new DateTime();

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            StopTime = DateTime.Now;
            m_lElapsedTickCount = StopTime.Ticks - StartTime.Ticks;
        }

        public double GetTickCount()
        {
            // 시간의 정확도를 높이기 위해 반복 
            return Math.Truncate((double)m_lElapsedTickCount * 0.0001);
        }
    }
}
