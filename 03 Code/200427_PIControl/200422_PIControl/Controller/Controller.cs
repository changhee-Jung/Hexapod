using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using PI_Hexapod;
using NLog;

namespace Seq
{



    enum ActionState
    {
        Wait,
        START,
        MOVE,
        Test,
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
        ActionState m_ActionState;
        TickCount tickCount = new TickCount();

        string m_strError;
        long m_lBufferSize            = 0;
        int nTick                     = 0;
        double m_dbElapsedTickCounter = 0;
        bool bTest = false;
        int nNext = 0;
        int nWaveName_U = 30;
        int nWaveName_V = 40;

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
                    SetImmediatelyMoveMode();
                    m_ActionState = ActionState.Wait;
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
                    case ActionState.Wait:

                        break;

                    case ActionState.START:
                        if (false == model_Hexa.IsMoving())
                        {
                            nTick = 0;
                            SetCyclicMode(100);
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

                    case ActionState.Test:
                        MakeWaveGenerator();
                        //if (false == IsRunningWaveGenerator())
                        //{
                        //    MakeWaveGenerator();
                        //}
                        break;

                    case ActionState.STOP:
                        model_Hexa.StopAllAxis();
                        SetImmediatelyMoveMode();
                        m_ActionState = ActionState.Wait;
                        break;

                }
            }
            tickCount.Stop();
            m_dbElapsedTickCounter = tickCount.GetTickCount();
           // Console.WriteLine("TickTime: " + m_dbElapsedTickCounter.ToString());
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
                double dbPosition_U = 5 * Math.Sin(Math.PI * 0.002 * nTick);
                double dbPosition_V = 5 * Math.Cos(Math.PI * 0.002 * nTick);
                double[] UVTargetPosition = { dbPosition_U, dbPosition_V };
                MovePosition("U V", UVTargetPosition);
                UpdateTickCount();
            }
        }

        public void UpdateTickCount()
        {
            if(nTick >= 1000)
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
            else if (strName.Contains("Tool"))
            {
                model_Hexa.SetPivotPosition();
            
            }
            else if(strName.Contains("qWGS"))
            {
                string strItem = "STATUS";
                StringBuilder strAnswer = new StringBuilder(256);
                PI.GCS2.qWGS(model_Hexa.ID, 4, strItem, strAnswer, 256);

                string[] strBuffer = strAnswer.ToString().Split('=');
                string strResult = strBuffer[1].Trim();

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
        public void SetCyclicMode(int nBufferSize)
        {
            model_Hexa.SetCyclicMode(nBufferSize);

        }

        public void SetImmediatelyMoveMode()
        {
            
            model_Hexa.SetImmediatelyMoveMode();
        }


        public bool IsRunningWaveGenerator()
        {
            return  model_Hexa.IsRunningWaveGenerator(4) & model_Hexa.IsRunningWaveGenerator(5);
        }
        public void MakeWaveGenerator()
        {

            double[] dbPoint_U = new double[1000];
            double[] dbPoint_V = new double[1000];

            for (int i = 0; i < 1000 - m_lBufferSize; i++)
            {
                dbPoint_U[i] = 5 * Math.Sin(Math.PI * 0.002 * nTick);
                dbPoint_V[i] = 5 * Math.Cos(Math.PI * 0.002 * nTick);
                UpdateTickCount();
            }

            if (bTest == false)
            {
                model_Hexa.SetWaveGenerator(nWaveName_U, 0, dbPoint_U);
                model_Hexa.SetWaveGenerator(nWaveName_V, 0, dbPoint_V);
                bTest = true;
                model_Hexa.SetWaveGeneratorToAxis(nWaveName_U, "U");
                model_Hexa.SetWaveGeneratorToAxis(nWaveName_V, "V");
                string[] ar_strAxis = { "U", "V" };
                model_Hexa.StartWavegenerator(ar_strAxis);
            }
            else
            {
                model_Hexa.SetWaveGenerator(nWaveName_U, nNext * 1000, dbPoint_U);
                model_Hexa.SetWaveGenerator(nWaveName_V, nNext * 1000, dbPoint_V);

                if(nNext > 10)
                {
                    nNext = 0;
                }
                else
                {
                    nNext++;
                }
            }

        }

        public void WaveGeneratorAction()
        {
            // VLS와 관련이 없음.
            // 내부 Start, Stop 모드 설정이 있음
            // 자체 문제 발생할 경우 멈춤
            // Servo Cycle Time 0.0001s
            // 반복도가 0일 경우 WGO 또는 #24, STP, HLT 이외에는 계속 움직임

            int nWaveName_U = 30;
            int nWaveName_V = 40;
            double[] dbPointData_U = new double[1000];
            double[] dbPointData_V = new double[1000];

            for (int i = 0; i < 1000; i++)
            {
                dbPointData_U[i] = 5 * Math.Sin(Math.PI * 0.002 * i);
                dbPointData_V[i] = 5 * Math.Cos(Math.PI * 0.002 * i);
            }

            model_Hexa.SetWaveGenerator(nWaveName_U, 0, dbPointData_U);
            model_Hexa.SetWaveGenerator(nWaveName_V, 0, dbPointData_V);

            model_Hexa.SetWaveGeneratorToAxis(nWaveName_U, "U");
            model_Hexa.SetWaveGeneratorToAxis(nWaveName_V, "V");

            string[] ar_strAxis = { "U", "V" };
            model_Hexa.StartWavegenerator(ar_strAxis);
        }

        public void Stop()
        {
            model_Hexa.Stop();
        }
    }
}
