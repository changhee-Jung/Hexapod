using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        ErrorCode m_Error;

        public int ID { get { return m_ID; } set { m_ID = value; } }
        public bool IsConnected { get { return m_bIsConnected; } set { m_bIsConnected = value; } }
        public Dictionary<string, double> DicOfAxis { get { return m_dicOfAxis; } }
        public ErrorCode ErrorCode { get { return m_Error; } set { m_Error = value; } }

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
            List<string> listOfAxis = new List<string>(m_dicOfAxis.Keys);

            for(int nIndex = 0; nIndex < listOfAxis.Count; nIndex++)
            {
                double[] dbPos = { 0 };

                PI.GCS2.qPOS(ID, listOfAxis[nIndex], dbPos);
                DicOfAxis[listOfAxis[nIndex]] = dbPos[0];
            }         
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
            if (m_Error == ErrorCode.PI_CNTR_NO_ERROR)
            {
                /// reference 초기 위치로 이동, 모든 축에 대해 실행(Fast Reference Move To Referece Switch)
                PI.GCS2.FRF(m_ID, "");
                // 진행중인지 파악할때는 qFRF
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
            if (m_Error == ErrorCode.PI_CNTR_NO_ERROR)
            {
                PI.GCS2.MOV(m_ID, strAxis, dbPos);
            }

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

        public string CheckError()
        {
            int nError = 0;
            PI.GCS2.qERR(ID, ref nError);
            string strError = "";
            if (true == Enum.IsDefined(typeof(ErrorCode), nError))
            {
                m_Error = (ErrorCode)nError;

                strError = m_Error.ToString();
            }
            else
            {
                strError = nError.ToString();
            }

            return strError;
        }
    }
}
