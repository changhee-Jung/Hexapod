using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using Seq;

namespace PI_Hexapod
{
    public partial class Main_UI : Form
    {
        public Main_UI()
        {
            InitializeComponent();

            controller     = new Controller(this);

            seqApplication = new SeqApplication(this, controller);
            seqApplication.ThreadStart();
        }
   
        Controller controller         = null;
        SeqApplication seqApplication = null;

        private void btnPIInterface_Click(object sender, EventArgs e)
        {
            controller.Connect();
        }

        private void btnHomming_Click(object sender, EventArgs e)
        {
            controller.SetHomming();
        }


        private void btnMoveAxis_Click(object sender, EventArgs e)
        {
            if(txtCmdPosition.Text != "")
            {
                string strAxis = cboAvailableAxis.Text;
                double dbPosition = Convert.ToDouble(txtCmdPosition.Text);
                double[] dbTargetPosition = { dbPosition };
                controller.MovePosition(strAxis, dbTargetPosition);
            }
        }

        public void UpdatePositionData(Dictionary<string, double> dicOfData)
        {
            foreach(KeyValuePair<string, double> AxisData in dicOfData)
            {
                string strName = "lblPosition_" + AxisData.Key.Trim(); //lblPosition_X
                string strValue = AxisData.Value.ToString();
                foreach (Control ctl in this.Controls)
                {
                    if(ctl.Name == strName)
                    {
                        if(ctl.Text != strValue)
                        {
                            ctl.Text = strValue;
                        }
                    }
                }
            }
        }
        public void UpdateAxis(string strAxis)
        {
            foreach(object Items in cboAvailableAxis.Items)
            {
                string strName = Items.ToString();
                if(strName == strAxis)
                {
                    cboAvailableAxis.Items.Remove(Items);
                }
            }
            cboAvailableAxis.Items.Add(strAxis);
    
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDialog.Clear();
        }

        private void btnRotation_Click(object sender, EventArgs e)
        {          
            if(controller.ActionState == ActionState.Wait)
            {
                controller.ActionState = ActionState.START;
            }
            else if(controller.ActionState == ActionState.MOVE)
            {
                controller.ActionState = ActionState.STOP;
            }
        }

        public void DisplayError(string strError)
        {
            string strNowTime = DateTime.Now.ToString("tt h:mm:ss");
            txtErrorLog.Text += strNowTime + ": " + strError.Trim() + "\r\n";
        }

        private void btnZeroPosition_Click(object sender, EventArgs e)
        {
            double[] dbZero = { 0, 0, 0, 0, 0, 0 };
            controller.MovePosition("X Y Z U V W", dbZero);

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            controller.SearchCommand(txtSend.Text.Trim());
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            seqApplication.ThreadStop();
        }

        private void btnWaveGenerator_Click(object sender, EventArgs e)
        {
             controller.WaveGeneratorAction();
         //   controller.ActionState = ActionState.Test;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            controller.Stop();
            controller.ActionState = ActionState.STOP;
        }
    }

}

