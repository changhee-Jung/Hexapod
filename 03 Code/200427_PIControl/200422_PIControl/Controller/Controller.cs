using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PI_Hexapod;
using NLog;

namespace Seq
{

    public class 

    enum ActionState
    {
        START,
        MOVE,
        STOP
    }

    class Controller
    {
 
        public Controller(Main_UI _main_UI)
        {
            Log = LogManager.GetLogger("InfoLog");
            main_UI = _main_UI;
            model_Hexa = new Model();
            Log.Info("Controller Start");
        }

        Logger Log = null;
        Main_UI main_UI = null;
        Model model_Hexa = null;
        ActionState m_ActionState = ActionState.STOP;
        TickCount tickCount = new TickCount();

        string m_strError;
        long m_lBufferSize            = 0;
        int nTick                     = 0;
        double m_dbElapsedTickCounter = 0;

        List<double> m_listOfPosition_U = new List<double>();
        List<double> m_listOfPosition_V = new List<double>();
        Stopwatch sw = new Stopwatch();

        public ActionState ActionState { get { return m_ActionState; } set { m_ActionState = value; } }
        public double ElapsedTickCounter { get { return m_dbElapsedTickCounter; } }


        public bool Connect()
        {
            bool bResult = false;
            model_Hexa.ID = PI.GCS2.InterfaceSetupDlg("");
            if (model_Hexa.ID > -1)
            {
                int nIsConnected = PI.GCS2.IsConnected(model_Hexa.ID); // Connect Success(1) 
                model_Hexa.IsConnected = Convert.ToBoolean(nIsConnected);
                if (nIsConnected == 1)
                {
                    CompleteConnectionProcess(model_Hexa.ID);
                    SetCyclicMode(model_Hexa.ID);
                    bResult = true;
                }
                else
                {
                    main_UI.Invoke(new Action(delegate () { main_UI.DisplayError("Fail to Connect!\r\n"); }));
                    bResult = false;
                }

            }
            return bResult;
        }

        /// <summary>
        /// 2020.04.23 by chjung [ADD] PI_Hexapod와 연결 후 상태 및 데이터 갱신을 실행한다.
        /// </summary>
        public void CompleteConnectionProcess(int ID)
        {
            StringBuilder IdnBuffer = new StringBuilder(255);
            PI.GCS2.qIDN(ID, IdnBuffer, 255);
            PI.GCS2.qCST(ID, "W", IdnBuffer, IdnBuffer.Length);

            // 현재 사용가능한 Axis를 가져온다.
            PI.GCS2.qSAI_ALL(ID, IdnBuffer, IdnBuffer.Capacity);
            string[] words = (IdnBuffer.ToString()).Split('\n');
            foreach (string strAxis in words)
            {
                if (strAxis != "")
                {
                    if (strAxis.Contains('A') || strAxis.Contains('B')) { continue; }
                    model_Hexa.SetAvailableAxis(strAxis.Trim());
                    main_UI.Invoke(new Action(delegate () { main_UI.UpdateAxis(strAxis.Trim()); }));
                }
            }
        }

        public void Update()
        {
            tickCount.Start();

            if (model_Hexa.IsConnected)
            {
                CheckError();
                GetCurrentPos();
                GetTargetPositionBuffer();
                switch (m_ActionState)
                {
                    case ActionState.START:

                        if (false == model_Hexa.IsMoving())
                        {
                            nTick = 0;
                            m_ActionState = ActionState.MOVE;

                        }
                        break;

                    case ActionState.MOVE:
                        if (m_lBufferSize <= 100)
                        {
                            sw.Restart();
                            RotateAction();
                            sw.Stop();
                            Log.Info("SendTime: " + sw.ElapsedMilliseconds.ToString() + "msec");                         
                        }
                        break;

                    case ActionState.STOP:
                        break;

                }
            }
            tickCount.Stop();
            m_dbElapsedTickCounter = tickCount.GetTickCount();
            Console.WriteLine("TickTime: " + m_dbElapsedTickCounter.ToString());
        }
    
        /// <summary>
        /// 2020.04.23 by chjung [ADD] 에러 체크를 실행한다.
        /// </summary>
        public void CheckError()
        {     
            string strError = model_Hexa.CheckError();

            if (strError != m_strError)
            {
                m_strError = strError;
                main_UI.Invoke(new Action(delegate () { main_UI.DisplayError(m_strError); }));
                Log.Error(m_strError);
            }
        }
         
        /// <summary>
        /// 2020.04.23 by chjung [ADD] Hexapod의 각 축의 길이 값을 가져온다.
        /// </summary>
        public void GetCurrentPos()
        {
            model_Hexa.UpdateAxisPosition();
            main_UI.Invoke(new Action(delegate () { main_UI.UpdatePositionData(model_Hexa.DicOfAxis); }));         
        }

        /// <summary>
        /// 2020.04.23 by chjung [ADD] hexapod의 초기 위치로 이동한다.
        /// </summary>
        public void SetHomming()
        {
            model_Hexa.SetHome();
        }

        /// <summary>
        /// 2020.04.23 by chjung [ADD] 선택 Axis의 위치를 이동시킨다.
        /// </summary>
        public void MovePosition(string strAxis,double[] dbPos)
        {
            model_Hexa.Move(strAxis, dbPos);   
        }

        public void RotateAction()
        {
            for (int i = 0; i < 100 - m_lBufferSize; i++)
            {
                double dbPosition_U = 5 * Math.Sin(Math.PI * 0.02 * nTick);
                double dbPosition_V = 5 * Math.Cos(Math.PI * 0.02 * nTick);
                double[] UVTargetPosition = { dbPosition_U, dbPosition_V };
                MovePosition("X Y", UVTargetPosition);
                UpdateTickCount();
            }
        }

        public void UpdateTickCount()
        {
            if(nTick >= 100)
            {
                nTick = 0;
            }
            else
            {
                nTick++;
            }
        }

        public void SearchCommand(string strName)
        {
            if (strName == "IsMoving")
            {
                bool bResult = model_Hexa.IsMoving();
                main_UI.txtDialog.Text = bResult.ToString();
            }
            else if (strName == "Velocity")
            {
                double nVelocity = 13;
                PI.GCS2.qVLS(model_Hexa.ID, ref nVelocity);
                main_UI.txtDialog.Text = nVelocity.ToString();
            }
            else if(strName.Contains("SetVel"))
            {
                string[] ar_strName = strName.Split(':');
                double dbVelocity = Convert.ToDouble(ar_strName[1]);
                PI.GCS2.VLS(model_Hexa.ID, dbVelocity);
            }
 
        }
        /// <summary>
        /// 2020.04.28 by chjung [ADD] 현재 저장된 PositionBuffer 크기를 확인한다.
        /// </summary>
        public void GetTargetPositionBuffer() 
        {
            uint[] nBufferLength = { 0x19001904 };
            long[] lData = { 1 };
            PI.GCS2.qSPA_int64(model_Hexa.ID, "X", nBufferLength, lData);
            m_lBufferSize = lData[0];
            Log.Info("BufferSize :" + lData[0].ToString());
            main_UI.Invoke(new Action(delegate () { main_UI.lblBufferSize.Text = lData[0].ToString(); }));
         
        }
        /// <summary>
        /// 2020.04.28 by chjung [ADD] Cyclic Mode를 설정한다.
        /// </summary>
        public void SetCyclicMode(int nID)
        {
            uint[] nTrajectorySource = { 0x19001900, 0x19001901 };
            long[] lSetData = { 1, 1 };
            PI.GCS2.SPA_int64(nID, "X", nTrajectorySource, lSetData);

        }



    }
}
