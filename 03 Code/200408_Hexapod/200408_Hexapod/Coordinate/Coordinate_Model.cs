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

        private double[] dbBasetoHeightVector = { 0, 0, 0 };

        Dictionary<int, double[]> m_dicOfBaseToBaseJointVector   = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfUpperJointVectorforBase = new Dictionary<int, double[]>();
        Dictionary<int, double[]> m_dicOfCylinderVector          = new Dictionary<int, double[]>();
      
        #endregion

        #region 메소드
        public void SetBasetoHeightVector(double dbHeight)
        {
            dbBasetoHeightVector[2] = dbHeight;
        }
        /// <summary>
        /// 2020.04.08 by chjung [ADD] BaseJoint와 UpperPlateCenter 사이의 벡터를 계산한다.
        /// </summary>
        public void CalculateBaseToBaseJoint(Dictionary<int, double[]> dicOfBaseJointVector)
        {
            for(int nIndex = 0; nIndex < dicOfBaseJointVector.Count; nIndex++)
            {
                double[] dbVector = dicOfBaseJointVector[nIndex];

                if (dbVector.Length != 2) { return; }

                for (int  i= 0; i < dbVector.Length; i++)
                {
                    m_dicOfBaseToBaseJointVector[nIndex][i] = dbBasetoHeightVector[i] - dbVector[i];
                }
            }                          
        }

        public void CalculateBaseToUpperJoint(Dictionary<int, double[]> dicOfUpperJointVector)
        {
            for (int nIndex = 0; nIndex < dicOfUpperJointVector.Count; nIndex++)
            {
                double[] dbVector = dicOfUpperJointVector[nIndex];

                if (dbVector.Length != 2) { return; }

                for (int i = 0; i < dbVector.Length; i++)
                {
                   
                }
            }         
        }
        
        #endregion
    }
  
    
}
