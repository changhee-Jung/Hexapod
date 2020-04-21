using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Seq;
namespace Hexapod
{

    public class SeqProcess
    {
        Thread SequenceThread;
        Main_UI Main_ui      = null;
        ConMotion conMotion = null;

        public SeqProcess(Main_UI _Main_ui)
        {
            conMotion = new ConMotion(_Main_ui, new Hardware(), new Vector(), new MotionProfile());
   
            Main_ui = _Main_ui;
            Main_ui.UpdateData += conMotion.DataSet_Updated;

            SequenceThread = new Thread(new ThreadStart(Thread_Run));
            SequenceThread.IsBackground = true;
            SequenceThread.Start();
        }

        void Thread_Run()
        {
            while(true)
            {
                conMotion.SetDataProcess();
                conMotion.Update();
  
                Thread.Sleep(1);
                // 1. input 
                // 2. process 진행
                // 3. output
            }
        }

      
    }
}