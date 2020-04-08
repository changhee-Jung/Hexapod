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

        private double[] BasetoHeight = { 0, 0, 0 };

        #endregion

        #region 메소드
        public void SetBasetoHeightVector(double dbHeight)
        {
            BasetoHeight[2] = dbHeight;
        }
        
        #endregion
    }
  
    
}
