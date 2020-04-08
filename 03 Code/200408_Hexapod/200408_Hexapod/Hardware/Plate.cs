using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Plate
    {
        #region 멤버

        private int m_nNumberOfJoint = 0;
        private double m_height   = 0;
        private double m_dbRadius = 0;
        private double m_dbAngleOfOffset = 0;

        Dictionary<int, double[]> m_dicOfJointVector = new Dictionary<int, double[]>();

        #endregion

        #region 속성

        public int NumberOfJoint { get { return m_nNumberOfJoint; } }
        public double Radius { get { return m_dbRadius; } set { m_dbRadius = value; } }
        public double AngleOfOffset { get { return m_dbAngleOfOffset; } set { m_dbAngleOfOffset = value; } }
        public Dictionary<int, double[]> dicOfJointVector { get { return m_dicOfJointVector; } }
        #endregion

        #region 생성자

        public Plate(int nNumberOfJoint, double dbRadius, double dbAngleOfOffset)
        {
            m_nNumberOfJoint = nNumberOfJoint;
            m_dbRadius = dbRadius;
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
        public void MakeJointVector(bool bIsUpper)
        {
            double dbAngleOfAxis = (2 * Math.PI) / m_nNumberOfJoint;
            double dbAngleOfPoint = 0;
            for (int i = 0; i < m_nNumberOfJoint; i++)
            {
                int nOrder = i + 1;
                if (true == bIsUpper) // 상판일 경우
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
                else // 하판일 경우
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
                double dbVector_X = m_dbRadius * Math.Cos(dbAngleOfPoint);
                double dbVector_Y = m_dbRadius * Math.Sin(dbAngleOfPoint);
                double[] arVector = { dbVector_X, dbVector_Y, 0 };
                m_dicOfJointVector[i] = arVector;
            }
        }


        #endregion
    }
}
