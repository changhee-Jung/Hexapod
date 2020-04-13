using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    
    class Coordinate_Model
    {
        public Coordinate_Model()
        {
            Hexapod_Kinematics = new Kinematics();
        }

        Kinematics Hexapod_Kinematics = null;

        #region 멤버
        private double[] m_dbBasetoHeightVector = { 0, 0, 0 };
        private double[] m_TargetPositionVector = { 0, 0, 0 };
      
        Dictionary<int, double> m_dicOfTargetLength                  = new Dictionary<int, double>();
        Dictionary<int, double[]> m_dicOfBaseJointToUpperPlateVector = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfUpperJointVectorforBase     = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfBaseJointToUpperJointVector = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfActuatorVector              = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfBaseToTargetVector          = new Dictionary<int, double[]>();

        #endregion

        #region 속성
        public Dictionary<int, double[]> BaseToTargetVector { get { return m_dicOfBaseToTargetVector; } }
        public Dictionary<int, double> TargetLenghsOfActuator { get { return m_dicOfTargetLength; } }
        #endregion

        #region 메소드
        /// <summary>
        /// 2020.04.08 by chjung [ADD] Base와 Upper 간의 높이를 벡터로 변환한다.
        /// </summary>
        public void SetBasetoHeightVector(double dbHeight)
        {
            m_dbBasetoHeightVector[2] = dbHeight;
        }
        /// <summary>
        ///  2020.04.09 by chjung [ADD] BaseJoint에서 UpperPlate 사이의 벡터를 계산한다.
        /// </summary>
        public void CalculateBaseToUpperPlateVector(Dictionary<int, double[]> dicOfBaseJointVector)
        {
            for(int nIndex = 0; nIndex < dicOfBaseJointVector.Count; nIndex++)
            {
                double[] dbVector = dicOfBaseJointVector[nIndex];
                double[] dbResult = new double[dbVector.Length];
                if (dbVector.Length != 3) { return; }

                for (int  i= 0; i < dbVector.Length; i++)
                {          
                    dbResult[i]  = m_dbBasetoHeightVector[i] - dbVector[i];
                }
                m_dicOfBaseJointToUpperPlateVector[nIndex] = dbResult; 
        
            }                          
        }
        /// <summary>
        /// 2020.04.09 by chjung [ADD] Base에서 바라본 Upper와 UpperJoint 사이의 벡터를 계산한다.
        /// </summary>
        public void CalculateBaseToUpperJoint(double[] dbTargetRotation, Dictionary<int, double[]> dicOfUpperJointVector)
        {
            for (int nIndex = 0; nIndex < dicOfUpperJointVector.Count; nIndex++)
            {
                double[] dbVector = dicOfUpperJointVector[nIndex];

                if (dbVector.Length != 3) { return; }

                m_dicOfUpperJointVectorforBase[nIndex] = Hexapod_Kinematics.CalculateRotateEulerAngle(dbTargetRotation,dbVector);
            }         
        }
        /// <summary>
        /// 2020.04.09 by chjung [ADD] BaseJoint와 UpperJoint 사이의 벡터를 계산한다.
        /// </summary>
        public void CalculateBaseJointToUpperJointVector()
        {
            for (int i = 0; i < m_dicOfBaseJointToUpperPlateVector.Count; i++)
            {
                m_dicOfBaseJointToUpperJointVector[i] = Hexapod_Kinematics.CalculateTranslationVector(m_dicOfBaseJointToUpperPlateVector[i], m_dicOfUpperJointVectorforBase[i]);
            }
        }
        public void CalculateTargetPostionTranslation(double[] m_dbTargetPosition)
        {
            for(int i =0; i < m_dicOfBaseJointToUpperPlateVector.Count; i++)
            {
                m_dicOfBaseToTargetVector[i] = Hexapod_Kinematics.CalculateTranslationVector(m_dicOfBaseJointToUpperJointVector[i], m_dbTargetPosition);
            }

        }
        /// <summary>
        /// 2020.04.09 by chjung [ADD] 각 축(엑추에이터)의 목표 길이 값을 계산한다.
        /// </summary>
        public void CalculateActuatorVector()
        {
            for(int i = 0; i < m_dicOfBaseJointToUpperJointVector.Count; i++)
            {
                m_dicOfTargetLength[i] = Hexapod_Kinematics.CalculateNorm(m_dicOfBaseToTargetVector[i]);
            }
        }
        
        #endregion
    }
  
    
}
