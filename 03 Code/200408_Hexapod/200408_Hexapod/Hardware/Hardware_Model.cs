using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Hardware_Model
    {

        #region 생성자
        public Hardware_Model()
        {

        }
        #endregion

        #region 멤버
        Plate m_Plate_Base;
        Plate m_Plate_Upper;
        #endregion

        #region 속성
        public Plate Plate_Base { get { return m_Plate_Base; } }
        public Plate Plate_Upper { get { return m_Plate_Upper; } }
        #endregion

        #region 메소드
        /// <summary>
        /// 2020.04.08 by chjung [ADD] 상, 하판의 각 조인트 벡터를 계산한다.
        /// </summary>
        public void MakeHexapodPlate(int nNumOfJoint, double dbRadius_Base, double dbAngleOfOffset_Base, double dbRadius_Upper, double dbAngleOfOffset_Upper)
        {
            m_Plate_Base = new Plate(nNumOfJoint, dbRadius_Base, dbAngleOfOffset_Base);
            m_Plate_Base.MakeJointVector(false);

            m_Plate_Upper = new Plate(nNumOfJoint, dbRadius_Upper, dbAngleOfOffset_Upper);
            m_Plate_Upper.MakeJointVector(true);

        }
        #endregion
    }
}
