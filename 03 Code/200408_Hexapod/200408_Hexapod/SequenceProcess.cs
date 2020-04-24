using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace _200408_Hexapod
{
    enum SequenceState
    {
        Init,
        Ready,
        Processing,
        Done
    }
    public class SequenceProcess
    {
        Thread SequenceThread;
        SequenceState MainSequenceState = SequenceState.Init;

        Main_UI Main_ui = null;
        Coordinate_Control Control_Coordinate = null;
        Stopwatch sw = new Stopwatch();
        int nTicktime  =  0;
        int nCycleTime = 1000; 

        public SequenceProcess(Main_UI _Main_ui)
        {
            Control_Coordinate = new Coordinate_Control(_Main_ui ,new Hardware_Model(), new Coordinate_Model(), new Motion_Model());
            Main_ui = _Main_ui;
            Main_ui.UpdateData += Control_Coordinate.EventCallbackMethod;

            SequenceThread = new Thread(new ThreadStart(Thread_Run));
            SequenceThread.IsBackground = true;
            SequenceThread.Start();
        }

        void Thread_Run()
        {
            while(true)
            {
                // 상태 값 체크       
                switch(MainSequenceState)
                {
                    case SequenceState.Init:
                        // 하드웨어 설정 및 좌표계 생성, 모션 초기화
                        if (Control_Coordinate.IsSetAllHardWare)
                        {
                            MainSequenceState = SequenceState.Ready;
                            nTicktime = 0;
                            sw.Restart();
                        }                      
                        break;
                    case SequenceState.Ready:
                        // 모션 생성
                        Control_Coordinate.CalculateNextStepMotion(nTicktime, nCycleTime);
                        InitializeTickTime();

                        if (Control_Coordinate.IsMakeAllProfile)
                        {
                            MainSequenceState = SequenceState.Processing;
                            nTicktime = 0;
                            sw.Stop();
                            Console.WriteLine("Time: " + sw.ElapsedMilliseconds.ToString() + "msec");
                        }
                        break;
                    case SequenceState.Processing:
                        if(false == Control_Coordinate.IsSetAllHardWare)
                        {
                            MainSequenceState = SequenceState.Init;
                        }
                        break;
                    case SequenceState.Done:
                        break;
                  

                } 

                // 1. input 
                // 2. process 진행
                // 3. output

             Thread.Sleep(1);
            }
        }

        private void InitializeTickTime()
        {
            if (nTicktime > nCycleTime)
            {
                nTicktime = 0;
            }             
            else
            {
                nTicktime++;
            }
        }
    }
}
