using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace PI_Hexapod
{
    class Model
    {
        public Model()
        {

        }

        int m_ID;
        bool m_bIsConnected = false;

        Dictionary<string, double> m_dicOfAxis= new Dictionary<string, double>();
        int nErrorCode = 0;

        public int ID { get { return m_ID; } set { m_ID = value; } }
        public bool IsConnected { get { return m_bIsConnected; } set { m_bIsConnected = value; } }
        public Dictionary<string, double> DicOfAxis { get { return m_dicOfAxis; } }

        public bool IsAvailableAxis(string strAxis)
        {
            bool bResult = false;
            if(m_dicOfAxis.ContainsKey(strAxis))
            {
                bResult = true;
            }
            return bResult;
        }
        public void SetAvailableAxis(string strAxis)
        {
            if(m_dicOfAxis.ContainsKey(strAxis))
            {
                m_dicOfAxis.Remove(strAxis);
            }

            m_dicOfAxis.Add(strAxis,0);
        }

        public void UpdateAxisPosition()
        {
            if(m_dicOfAxis.Count <= 0) { return; }


            double[] dbData = { 0, 0, 0, 0, 0, 0 };
            PI.GCS2.qPOS(ID,"X Y Z U V W", dbData);

            DicOfAxis["X"] = dbData[0];
            DicOfAxis["Y"] = dbData[1];
            DicOfAxis["Z"] = dbData[2];
            DicOfAxis["U"] = dbData[3];
            DicOfAxis["V"] = dbData[4];
            DicOfAxis["W"] = dbData[5];

           // List<string> listOfAxis = new List<string>(m_dicOfAxis.Keys);
            //for (int nIndex = 0; nIndex < listOfAxis.Count; nIndex++)
            //{
            //    double[] dbPos = { 0 };

            //    PI.GCS2.qPOS(ID, listOfAxis[nIndex], dbPos);
            //    DicOfAxis[listOfAxis[nIndex]] = dbPos[0];
            //}         
        }

        public double GetAxisPosition(string strAxis)
        {
            double dbResult = 0;
            if(m_dicOfAxis.ContainsKey(strAxis))
            {
                dbResult = m_dicOfAxis[strAxis];
            }
            return dbResult;
        }

        public void SetHome()
        {
            if (nErrorCode == 0)
            {
                /// reference 초기 위치로 이동, 모든 축에 대해 실행(Fast Reference Move To Referece Switch)
                PI.GCS2.FRF(m_ID, "");
                // 진행중인지 파악할때는 qFRF
                //int[] nQref = new int[1];
                //while (0 == nQref[0])
                //{
                //    PI.GCS2.qFRF(m_ID, "X", nQref);
                //    Thread.Sleep(50);
                //}
            }
        }
        public void SetZeroPosition()
        {
            foreach(KeyValuePair<string,double> Axis in m_dicOfAxis)
            {
  //              Move(Axis.Key, 0);
            }
        }

        public void Move(string strAxis, double[] dbPos)
        {
            if (nErrorCode == 0)
            {
                // 이동 명령
                PI.GCS2.MOV(m_ID, strAxis, dbPos);
            }

        }

        public void SetImmediatelyMoveMode()
        {
            // 설정 변경 후
            uint[] nTrajectorySource = new uint[1];
            nTrajectorySource[0] = 0x19001900;
            long[] lSetData = { 0 };
            PI.GCS2.SPA_int64(m_ID, "X", nTrajectorySource, lSetData);
        }
        
        public void SetCyclicMode(int nBufferSize)
        {
            uint[] nTrajectorySource = new uint[1];
            nTrajectorySource[0] = 0x19001900;
            long[] lSetData = new long[1];
            lSetData[0] = 1;
            PI.GCS2.SPA_int64(m_ID, "X", nTrajectorySource, lSetData);


            nTrajectorySource[0] = 0x19001901;
            lSetData[0] = 1;
            PI.GCS2.SPA_int64(m_ID, "X", nTrajectorySource, lSetData);

            nTrajectorySource[0] = 0x19001903;
            lSetData[0] = nBufferSize;
            PI.GCS2.SPA_int64(m_ID, "X", nTrajectorySource, lSetData);
        }

        public bool IsMoving()
        {
            bool bResult = false;
            foreach (KeyValuePair<string, double> Axis in m_dicOfAxis)
            {
                string strAxis = Axis.Key;
                int[] nCurrentAxisMoving = { 0 };
                PI.GCS2.IsMoving(m_ID, strAxis, nCurrentAxisMoving);
                if (nCurrentAxisMoving[0] > 0)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        public void StopAllAxis()
        {
            PI.GCS2.STP(m_ID);
        }

        public string CheckError()
        {
            PI.GCS2.qERR(ID, ref nErrorCode);
            StringBuilder strError = new StringBuilder(256);
            PI.GCS2.TranslateError(nErrorCode, strError, 256);
            string strResult = strError.ToString();
            return strResult;
        }

        public void SetWaveGenerator(int nName, int nStart ,double[] dbDataPoint)
        {
         //   int nOffsetStartPoint = 0;
            PI.GCS2.WAV_PNT(m_ID, nName, nStart, dbDataPoint.Length, 0, dbDataPoint);
            
        }

        public void SetWaveGeneratorToAxis(int nName, string strAxis)
        {
            if (false == m_dicOfAxis.ContainsKey(strAxis)) { return; }

            int nAxis = ConvertStringAxisToInt(strAxis);
            int[] nIndex = { nAxis };
            int[] nTableName = { nName };

            PI.GCS2.WSL(m_ID, nIndex, nTableName, 1);
            int[] nRepeat = { 0 };
            PI.GCS2.WGC(m_ID, nIndex, nRepeat, 1);
        }

        public int ConvertStringAxisToInt(string strAxis)
        {
            List<string> listOfAxis = new List<string>(m_dicOfAxis.Keys);
            int nResult = 0;
            for (int i = 0; i < listOfAxis.Count; i++)
            {
                if (listOfAxis[i] == strAxis)
                {
                    nResult = i + 1;
                }
            }
            return nResult;
        }
        public void StartWavegenerator(string[] ar_strAxis)
        {
            int[] nName      = new int[ar_strAxis.Length];
            int[] nStartMode = new int[nName.Length];
            for (int nIndex =0; nIndex < nName.Length; nIndex++)
            {
                nName[nIndex] = ConvertStringAxisToInt(ar_strAxis[nIndex]);
                nStartMode[nIndex] = 1;
            }

            PI.GCS2.WGO(m_ID, nName, nStartMode, 2);
        }

        public void SetPivotPosition()
        {
            // R = X, S = Y , T = Z;
            double[] dbToolOffset = new double[3];
            dbToolOffset[0] = 1;
            dbToolOffset[1] = 2;
            dbToolOffset[2] = 3;
            PI.GCS2.SPI(m_ID, "R S T", dbToolOffset);
        }

        public bool IsRunningWaveGenerator(int nAxis)
        {
            bool bResult = false;
            string strItem = "STATUS";
            StringBuilder strAnswer = new StringBuilder(256);
            PI.GCS2.qWGS(m_ID, nAxis, strItem, strAnswer, 256);
            string[] strBuffer = strAnswer.ToString().Split('=');
            string strResult = strBuffer[1].Trim();

            if(strResult.Contains("0x1"))
            {
                bResult = true;
            }

            return bResult;
        }
         
        public void Stop()
        {
            PI.GCS2.STP(m_ID);
        }
    }
}
