using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
namespace _200406_hexapod
{
    public partial class Hexapod_View : Form
    {

        #region 멤버

        Hexapod_Model Model = new Hexapod_Model();
        static float fangle = 0.0f;

        #endregion

        public Hexapod_View()
        {

            InitializeComponent();
      
        }

        private void btnCalculate_InverseKinematics_Click(object sender, EventArgs e)
        {
            double[] dbTranslation = { 0, 0, 0 };
            dbTranslation[0] = Convert.ToDouble(txtInput_X.ToString());
            dbTranslation[1] = Convert.ToDouble(txtInput_Y.ToString());
            dbTranslation[2] = Convert.ToDouble(txtInput_Z.ToString());
            
            double[] dbRotation = { 0, 0, 0 };
            dbRotation[0] = Convert.ToDouble(txtInput_Roll.ToString());
            dbRotation[1] = Convert.ToDouble(txtInput_Pitch.ToString());
            dbRotation[2] = Convert.ToDouble(txtInput_Yaw.ToString());

            Model.CalculateRotateEulerAngle(dbRotation[0], dbRotation[1], dbRotation[2], dbTranslation);

        }

    }

    class Draw3D : GameWindow
    {
        public Draw3D(int width, int height) : base(width,height)
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.White);

            this.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);
            base.OnResize(e);
        }
    }
    
}
