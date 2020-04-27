using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PI_Hexapod;
using System.Diagnostics;
namespace Seq
{
    class SeqApplication
    {
        public SeqApplication(Main_UI main_UI, Controller controller)
        {
            this.main_UI = main_UI;
            this.controller = controller;
        }

        Thread MainThread;
        Controller controller;
        Main_UI main_UI;
        Stopwatch sw = new Stopwatch();
        int ntick     = 0;
        int nInterval = 1000;

        public void ThreadStart()
        {
            MainThread = new Thread(new ThreadStart(Run));
            MainThread.Start();
        }

        public void ThreadStop()
        {
            MainThread.Abort();
        }

        void Run()
        {
            while (true)
            {
                sw.Restart();
                try
                {
                    controller.Update();

                    int SleepTime = 100 - (int)controller.ElapsedTickCounter;
                    Thread.Sleep(SleepTime);
                    sw.Stop();
                    Console.WriteLine("Time :" + sw.ElapsedMilliseconds.ToString() + "msec");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                UpdateTickCount();

            }
        }
        void UpdateTickCount()
        {
            if(ntick > nInterval)
            {
                ntick = 0;
            }
            else
            {
                ntick++;
            }
        }
    }
}
