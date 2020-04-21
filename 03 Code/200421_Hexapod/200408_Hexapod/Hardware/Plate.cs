using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapod
{
    class Plate
    {
        public enum PlateSettings
        {
            None,
            Base,
            Upper
        }
        #region 멤버
        private PlateSettings m_Plate    = PlateSettings.None;
        private int m_nNumberOfJoint     = 0;
        private double m_dbHeight        = 0;
        private double m_dbRadius        = 0;
        private double m_dbAngleOfOffset = 0;
        private double[] m_dbPosition    = { 0, 0, 0 };
        private double[] m_dbRotation    = { 0, 0, 0 };
        private double[] m_dbToolOffset =  { 0, 0, 0 };
        Dictionary<int, double[]> m_dicOfJointVector = new Dictionary<int, double[]>();
        #endregion

        #region 속성
        public int NumberOfJoint { get { return m_nNumberOfJoint; } }
        public double Height { get { return m_dbHeight; } set { m_dbHeight = value; } }
        public double Radius { get { return m_dbRadius; } set { m_dbRadius = value; } }
        public double AngleOfOffset { get { return m_dbAngleOfOffset; } set { m_dbAngleOfOffset = value; } }
        public double[] Position { get { return m_dbPosition; } set { m_dbPosition = value; } }
        public double[] Rotation { get { return m_dbRotation; } set { m_dbRotation = value; } }
        public double[] ToolOffset { get { return m_dbToolOffset; } set { m_dbToolOffset = value; } }
        public Dictionary<int, double[]> dicOfJointVector { get { return m_dicOfJointVector; } }
        #endregion

        #region 생성자
        public Plate(PlateSettings SetPlate, int nNumberOfJoint, double dbRadius, double dbAngleOfOffset)
        { 
            m_Plate           = SetPlate;
            m_nNumberOfJoint  = nNumberOfJoint;
            m_dbRadius        = dbRadius;
            m_dbAngleOfOffset = dbAngleOfOffset;

            for (int i = 0; i < nNumberOfJoint; i++)
            {
                double[] dbVector = { 0, 0, 0 };
                m_dicOfJointVector.Add(i, dbVector);
            }
        }
        #endregion

        #region 메소드
        /// <summary>
        /// 2020.04.08 by chjung [ADD] 조인트 벡터를 설정한다.
        /// </summary>        
        public void SetJointVector(int nIndex, double[] dbVector)
        {
            if (false == m_dicOfJointVector.ContainsKey(nIndex)) { return; }

            m_dicOfJointVector[nIndex] = dbVector;

        }
        /// <summary>
        /// 2020.04.08 by chjung [ADD] 해당 조인트에 벡터 값을 가져온다.
        /// </summary>
        public double[] GetJointVector(int nIndex)
        {
            return m_dicOfJointVector[nIndex];

        }
        /// <summary>
        /// 2020.04.08 by chjung [ADD] 각 판 조인트 벡터를 계산한다.
        /// </summary>
        public void MakeJointVector() 
        {
            double dbAngleOfAxis = (2 * Math.PI) / m_nNumberOfJoint;
            double dbAngleOfPoint = 0;
            for (int i = 0; i < m_nNumberOfJoint; i++)
            {
                switch (m_nNumberOfJoint)
                {
                    case 3:
                        dbAngleOfPoint = -dbAngleOfAxis * i;
                        break;
                    case 6:
                        int nOrder = i + 1;
                        if (m_Plate == PlateSettings.Upper) // 상판일 경우
                        {
                            if (nOrder % 2 != 0) // 홀수 
                            {
                                dbAngleOfPoint = -dbAngleOfAxis * (nOrder - 1) - m_dbAngleOfOffset;

                            }
                            else
                            {
                                dbAngleOfPoint = -dbAngleOfAxis * nOrder + m_dbAngleOfOffset;
                            }
                        }
                        else if (m_Plate == PlateSettings.Base) // 하판일 경우
                        {
                            if (nOrder % 2 != 0)
                            {
                                dbAngleOfPoint = -dbAngleOfAxis * nOrder + m_dbAngleOfOffset;
                            }
                            else
                            {
                                dbAngleOfPoint = -dbAngleOfAxis * (nOrder - 1) - m_dbAngleOfOffset;
                            }
                        }
                        break;
                }
                double dbVector_X = m_dbRadius * Math.Cos(dbAngleOfPoint);
                double dbVector_Y = m_dbRadius * Math.Sin(dbAngleOfPoint);
                double[] arVector = { dbVector_X, dbVector_Y, 0 };
                m_dicOfJointVector[i] = arVector;
            }
                
          
        }

        public void CalculateTranslationVector(double[] ar_dbTranslationVector)
        {
            for(int i = 0; i < m_dbPosition.Length; i++)
            {
                m_dbPosition[i] += ar_dbTranslationVector[i];
            }
        }

        #endregion
    }
}
