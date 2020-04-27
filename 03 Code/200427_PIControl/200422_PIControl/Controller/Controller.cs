using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PI_Hexapod;

namespace Seq
{     

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
            main_UI    = _main_UI;
            model_Hexa = new Model();
        }

        Main_UI main_UI = null;
        Model model_Hexa = null;
        ActionState m_ActionState = ActionState.STOP;
        TickCount tickCount = new TickCount();
        string m_strError;
        int nTick = 0;
        double m_dbElapsedTickCounter = 0;
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
                if(nIsConnected == 1)
                {
                    CompleteConnectionProcess(model_Hexa.ID);
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

           if(model_Hexa.IsConnected)
            {
                CheckError();
                GetCurrentPos();

                switch (m_ActionState)
                {
                    case ActionState.START:
                        model_Hexa.SetZeroPosition();
                        if (false == model_Hexa.IsMoving())
                        {
                            nTick = 0;
                            m_ActionState = ActionState.MOVE;

                        }
                        break;

                    case ActionState.MOVE:
                     //   if (false == model_Hexa.IsMoving()) // 위치 확인
                        {
                            double dbPosition_U = 5 * Math.Sin(Math.PI * 0.1 * nTick);
                            double dbPosition_V = 5 * Math.Cos(Math.PI * 0.1 * nTick);
                            RotateAction(dbPosition_U, dbPosition_V);
                            UpdateTickCount();
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
        public void MovePosition(string strAxis,double dbPos)
        {
            model_Hexa.Move(strAxis, dbPos);   
        }

        public void RotateAction(double dbPosition_U, double dbPosition_V)
        {
            MovePosition("U", dbPosition_U);
            MovePosition("V", dbPosition_V);
            //switch (m_ActionState)
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

        public void UpdateTickCount()
        {
            if(nTick >= 20)
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


    }
}
