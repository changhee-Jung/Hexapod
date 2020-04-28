using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PI_Hexapod;
using System.Diagnostics;
using NLog;

namespace Seq
{
    class SeqApplication
    {
        public SeqApplication(Main_UI main_UI, Controller controller)
        {
            this.main_UI = main_UI;
            this.controller = controller;
            ErrorLog = LogManager.GetLogger("ErrorLog");
            ErrorLog.Info("ErrorLog Start!");
        }

        Thread MainThread;
        Controller controller;
        Main_UI main_UI;
        Logger ErrorLog = null;
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
                try
                {
                    controller.Update();

                    //int SleepTime = 100 - (int)controller.ElapsedTickCounter;
                    Thread.Sleep(1);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    ErrorLog.Error(ex.ToString());
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
