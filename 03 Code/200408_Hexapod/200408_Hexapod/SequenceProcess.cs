using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

        int nTicktime = 0;
        int nCycle     = 1000; // 주기
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
                switch(MainSequenceState)
                {
                    case SequenceState.Init:
                        // 하드웨어 설정 및 좌표계 생성
                        if (Control_Coordinate.IsSetAllHardWare)
                        {
                            MainSequenceState = SequenceState.Ready;
                            nTicktime = 0;
                        }                                              
                        break;
                    case SequenceState.Ready:
                        // 모션 생성
                        Control_Coordinate.CalculateNextStepMotion(nTicktime, nCycle);
                        if (Control_Coordinate.IsMakeAllProfile)
                        {
                            MainSequenceState = SequenceState.Processing;
                        }
                        InitializeTickTime();
                        break;
                    case SequenceState.Processing:
                        Console.WriteLine("test");
                        break;
                    case SequenceState.Done:
                        break;

                    Thread.Sleep(1);

                } 

                // 1. input 
                // 2. process 진행
                // 3. output
            }
        }

        private void InitializeTickTime()
        {
            if (nTicktime > nCycle)
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
