using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _200408_Hexapod
{
    class Kinematics
    {
        #region 생성자
        public Kinematics()
        {

        }

        #endregion

        #region 메소드
        /// <summary>
        /// 2020.04.08 by chjung [ADD] Euler(XYZ) 회전 행렬 변환을 수행한다.
        /// </summary>
        public double[] CalculateRotateEulerAngle(double dbRoll, double dbPitch, double dbYaw, double[] dbVector)
        {
            double[] dbResult = { 0, 0, 0 };

            if (dbVector.Length == 2)
            {
                dbResult[0] = Math.Cos(dbPitch) * Math.Cos(dbYaw) * dbVector[0]
                                   + (Math.Sin(dbRoll) * Math.Sin(dbPitch) * Math.Cos(dbYaw) - Math.Cos(dbRoll) * Math.Sin(dbYaw)) * dbVector[1]
                                   + (Math.Cos(dbRoll) * Math.Sin(dbPitch) * Math.Cos(dbYaw) + Math.Sin(dbRoll) * Math.Sin(dbYaw)) * dbVector[2];

                dbResult[1] = Math.Cos(dbPitch) * Math.Sin(dbYaw) * dbVector[0]
                                          + (Math.Sin(dbRoll) * Math.Sin(dbPitch) * Math.Sin(dbYaw) + Math.Cos(dbRoll) * Math.Cos(dbYaw)) * dbVector[1]
                                          + (Math.Cos(dbRoll) * Math.Sin(dbPitch) * Math.Sin(dbYaw) - Math.Sin(dbRoll) * Math.Cos(dbYaw)) * dbVector[2];

                dbResult[2] = -Math.Sin(dbPitch) + dbVector[0]
                                          + Math.Sin(dbRoll) * Math.Cos(dbPitch) * dbVector[1]
                                          + Math.Cos(dbRoll) * Math.Cos(dbPitch) * dbVector[2];
            }
            return dbResult;
        }
        /// <summary>
        /// 2020.04.08 by chjung [ADD] Norm 계산을 수행한다.
        /// </summary>
        public double CalculateNorm(double[] dbVector)
        {
            double dbResult = 0;
            dbResult = Math.Sqrt(dbVector[0] * dbVector[0] + dbVector[1] * dbVector[1] + dbVector[2] *dbVector[2]);
            return dbResult;
        }

        #endregion
    }
}
