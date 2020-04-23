using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
//using PI;
using PI_Hexapod;
namespace Seq
{
    enum SeqState
    {
        none,
        initialize,
        wait,
        start,
        Action,
        stop,

    }
    
    enum ActionState
    {
        first,
        second,
        third,
        fourth
    }
    class Controller
    {
        public Controller(Main_UI _main_UI)
        {
            main_UI = _main_UI;
        }

        Main_UI main_UI = null;
        Thread MainThread;
        SeqState m_SeqState         = SeqState.none;
        ActionState m_ActionState = ActionState.first;
        List<string> m_listOfAvailableAxis = new List<string>();
        Dictionary<string, string> m_dicOfAxisPosition = new Dictionary<string, string>();
        string m_strError;
        int m_ID;
        int ntick = 0;
        Stopwatch sw = new Stopwatch();
        double m_dbMaxDeg_U = 0;
        double m_dbMaxDeg_V = 0;

        public Dictionary<string,string> DicOfAxisPosition {  get { return m_dicOfAxisPosition; } }
        public SeqState SeqState { get { return m_SeqState; } set { m_SeqState = value; } }
        public List<String> ListOfAxis { get { return m_listOfAvailableAxis; } }
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
            while(true)
            {
                CheckError();
                GetCurrentPos();
                try
                {
                    sw.Restart();
                    switch (m_SeqState)
                    {
                        case SeqState.none:
                            break;

                        case SeqState.initialize:
                            break;

                        case SeqState.wait:
                            break;
                            
                        case SeqState.start:
                            foreach(string strAxis in ListOfAxis)
                            { 
                                MovePosition(strAxis, 0);
                            }
                            if(false == IsMoving())
                            {
                                m_SeqState = SeqState.Action;
                                ntick = 0;
                            }
                            break;

                        case SeqState.Action:
                            //if (false == IsMoving())
                            //{
                            //    double dbPosition_U = 2 * Math.Sin(Math.PI * 0.2 * ntick);
                            //    double dbPosition_V = 2 * Math.Cos(Math.PI * 0.2 * ntick);
                            //    RotateAction(dbPosition_U, dbPosition_V);
                            //}
                            double dbPosition_U = 5 * Math.Sin(Math.PI * 0.1 * ntick);
                            double dbPosition_V = 5 * Math.Cos(Math.PI * 0.1 * ntick);
                            RotateAction(dbPosition_U, dbPosition_V);
                            break;

                        case SeqState.stop:
                            ntick = 0;
                            m_ActionState = ActionState.first;
                            m_SeqState = SeqState.wait;
                            break;

       
                    }
                    Thread.Sleep(50);
                    sw.Stop();
                    Console.WriteLine("Time :" + sw.ElapsedMilliseconds.ToString() + "msec");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
              
            }
        }
        public void Connect()
        {
            m_ID = PI.GCS2.InterfaceSetupDlg("");
            if (m_ID > -1)
            {
                CompleteConnectionProcess();
                m_SeqState = SeqState.wait;
            }
        }
        /// <summary>
        /// 2020.04.23 by chjung [ADD] 에러 체크를 실행한다.
        /// </summary>
        private void CheckError()
        {
            if(m_SeqState != SeqState.none)
            {
                int nError = 0;
                PI.GCS2.qERR(m_ID, ref nError);
                string strError;
                if (true == Enum.IsDefined(typeof(ErrorCode), nError))
                {
                    ErrorCode error = (ErrorCode)nError;
                    strError = error.ToString();
                }
                else
                {
                    strError = nError.ToString();
                }
                
                if(strError != m_strError)
                {
                    m_strError = strError;
                    main_UI.Invoke(new Action(delegate () { main_UI.DisplayError(m_strError); }));

                }
            }
          
        }
        /// <summary>
        /// 2020.04.23 by chjung [ADD] PI_Hexapod와 연결 후 상태 및 데이터 갱신을 실행한다.
        /// </summary>
        public void CompleteConnectionProcess()
        {
            StringBuilder IdnBuffer = new StringBuilder(255);
            PI.GCS2.qIDN(m_ID, IdnBuffer, 255);
            PI.GCS2.qCST(m_ID, "W", IdnBuffer, IdnBuffer.Length);

            // 현재 사용가능한 Axis를 가져온다.
            PI.GCS2.qSAI_ALL(m_ID, IdnBuffer, IdnBuffer.Capacity);
            string[] words = (IdnBuffer.ToString()).Split('\n');
            m_listOfAvailableAxis.Clear();
            foreach(string strAxis in words)
            {
                if(strAxis !="")
                {
                    if (strAxis.Contains('A') || strAxis.Contains('B')) { continue; }
                    m_listOfAvailableAxis.Add(strAxis.Trim());
                    m_dicOfAxisPosition.Add(strAxis.Trim(), "0");
                }
            }
            main_UI.Invoke(new Action(delegate () { main_UI.UpdateAxis(); }));

        }
        /// <summary>
        /// 2020.04.23 by chjung [ADD] Hexapod의 각 축의 길이 값을 가져온다.
        /// </summary>
        public void GetCurrentPos()
        {
            if(m_SeqState != SeqState.none)
            {
                double[] dbPos = new double[1];
                foreach (string strAxis in m_listOfAvailableAxis)
                {
                    PI.GCS2.qPOS(m_ID, strAxis, dbPos);
                    m_dicOfAxisPosition[strAxis] = dbPos[0].ToString();
                    main_UI.Invoke(new Action(delegate () { main_UI.UpdatePositionData(); }));
                }
            }
           
            
        }
        /// <summary>
        /// 2020.04.23 by chjung [ADD] hexapod의 초기 위치로 이동한다.
        /// </summary>
        public void SetHomming()
        {
            if(m_SeqState != SeqState.none)
            {
                if (m_strError == ErrorCode.PI_CNTR_NO_ERROR.ToString())
                {
                    /// reference 초기 위치로 이동, 모든 축에 대해 실행(Fast Reference Move To Referece Switch)
                    PI.GCS2.FRF(m_ID, "");
                }

            }
           

        }
        /// <summary>
        /// 2020.04.23 by chjung [ADD] 선택 Axis의 위치를 이동시킨다.
        /// </summary>
        public void MovePosition(string strAxis,double dbPos)
        {
            if(m_strError == "PI_CNTR_NO_ERROR")
            {
                int[] iChnl = new int[2];
                iChnl[0] = 0;
                int[] iPar = new int[2];
                iPar[0] = 1;
                PI.GCS2.DRT(m_ID, iChnl, iPar, "0", 1);
                double[] dVals = new double[1];
                dVals[0] = dbPos;
                PI.GCS2.MOV(m_ID, strAxis, dVals);
            }
           
        }

        public void RotateAction(double dbPosition_U, double dbPosition_V)
        {
            MovePosition("U", dbPosition_U);
            MovePosition("V", dbPosition_V);

            if (ntick >= 20)
            {
                ntick = 0;
            }
            else
            {
                ntick++;
            }
            //switch(m_ActionState)
            //{
            //    case ActionState.first:
            //        MovePosition("U", 5);
            //        MovePosition("V", 5);
            //        m_ActionState = ActionState.second;
            //        break;

            //    case ActionState.second:
            //        MovePosition("U", -5);
            //        MovePosition("V", 5);
            //        m_ActionState = ActionState.third;
            //        break;

            //    case ActionState.third:
            //        MovePosition("U", -5);
            //        MovePosition("V", -5);
            //        m_ActionState = ActionState.fourth;
            //        break;

            //    case ActionState.fourth:
            //        MovePosition("U", 5);
            //        MovePosition("V", -5);
            //        m_ActionState = ActionState.first;
            //        break;
            //}        
        }

        public bool IsMoving()
        {
            bool bResult = false;
            int[] nMovingCheck = { 0 };
            foreach (string strAxis in ListOfAxis)
            {
                int[] nCurrentAxisMoving = { 0 };
                PI.GCS2.IsMoving(m_ID, strAxis, nCurrentAxisMoving);
                nMovingCheck[0] += nCurrentAxisMoving[0];
                if (nMovingCheck[0] > 0)
                {
                    bResult = true;
                    break;
                }
            }

           
            return bResult;
        }

        public void SearchCommand(string strName)
        {
            if (strName == "IsMoving")
            {
                bool bResult = IsMoving();
                main_UI.txtDialog.Text = bResult.ToString();
            }
            else if (strName == "Velocity")
            {
                double nVelocity = 13;
                PI.GCS2.qVLS(m_ID, ref nVelocity);
                main_UI.txtDialog.Text = nVelocity.ToString();
            }
            else if(strName.Contains("SetVel"))
            {
                string[] ar_strName = strName.Split(':');
                double dbVelocity = Convert.ToDouble(ar_strName[1]);
                PI.GCS2.VLS(m_ID, dbVelocity);
            }
                
        }


    }
}
