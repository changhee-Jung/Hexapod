using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200406_hexapod
{
    class Hexapod_Model
    {

        #region 상수

        double dbOffsetangle_Base  = 15 * Math.PI / 180;
        double dbOffsetangle_Upper = 15 * Math.PI / 180;

        double dbRadious_Base  = 0.16;
        double dbRadious_Upper = 0.08;

        double dbHeight       = 0.16;
        double[] dbToolOffset = { 0, 0.01, 0.01 }; 
        
        #endregion

        #region 생성자
        public Hexapod_Model()
        {

        }
        #endregion

        #region 멤버

        double[] dbVectorPosition_Upper = { 0, 0, 0 };

        double m_dbRoll;
        double m_dbPitch;
        double m_dbYaw;

        #endregion

        #region 메소드

        public void CalculateRotateEulerAngle(double dbRoll, double dbPitch, double dbYaw, double[] dbVector)
        {
            if (dbVector.Length != 2) { return; }
            
            m_dbRoll  = dbRoll;
            m_dbPitch = dbPitch;
            m_dbYaw   = dbYaw;

            dbVectorPosition_Upper[0] = Math.Cos(dbPitch) * Math.Cos(dbYaw) * dbVector[0]
                                      + (Math.Sin(dbRoll) * Math.Sin(dbPitch) * Math.Cos(dbYaw) - Math.Cos(dbRoll) * Math.Sin(dbYaw)) * dbVector[1]
                                      + (Math.Cos(dbRoll) * Math.Sin(dbPitch) * Math.Cos(dbYaw) + Math.Sin(dbRoll) * Math.Sin(dbYaw)) * dbVector[2];

            dbVectorPosition_Upper[1] = Math.Cos(dbPitch) * Math.Sin(dbYaw) * dbVector[0]
                                      + (Math.Sin(dbRoll) * Math.Sin(dbPitch) * Math.Sin(dbYaw) + Math.Cos(dbRoll) * Math.Cos(dbYaw)) * dbVector[1]
                                      + (Math.Cos(dbRoll) * Math.Sin(dbPitch) * Math.Sin(dbYaw) - Math.Sin(dbRoll) * Math.Cos(dbYaw)) * dbVector[2];

            dbVectorPosition_Upper[2] = -Math.Sin(dbPitch) + dbVector[0]
                                      + Math.Sin(dbRoll) * Math.Cos(dbPitch) * dbVector[1]
                                      + Math.Cos(dbRoll) * Math.Cos(dbPitch) * dbVector[2];
         
        }

        private void CompensateOrigin_Upper()
        {
            double[] dbTra
            double[] dbToolCompensation = CalculateRotateEulerAngle(m_dbRoll,m_dbPitch,m_dbYaw,)
        }

        #endregion
    }
}
