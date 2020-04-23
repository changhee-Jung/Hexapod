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
            controller = new Controller(this);
            controller.ThreadStart();
        }

        Controller controller = null;
        
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
                controller.MovePosition(strAxis, dbPosition);
            }
        }

        public void UpdatePositionData()
        {
            foreach(KeyValuePair<string,string> AxisData in controller.DicOfAxisPosition)
            {
                string strName = "lblPosition_" + AxisData.Key; //lblPosition_X
                foreach (Control ctl in this.Controls)
                {
                    if(ctl.Name == strName)
                    {
                        if(ctl.Text != AxisData.Value)
                        {
                            ctl.Text = AxisData.Value;
                        }
                    }
                }
            }
        }
        public void UpdateAxis()
        {
            cboAvailableAxis.Items.Clear();
            foreach(string strAxis in controller.ListOfAxis)
            {
                cboAvailableAxis.Items.Add(strAxis);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDialog.Clear();
        }

        private void btnRotation_Click(object sender, EventArgs e)
        {
            if(controller.SeqState == SeqState.wait)
            {
                controller.SeqState = SeqState.start;
            }
            else if(controller.SeqState == SeqState.Action)
            {
                controller.SeqState = SeqState.stop;
            }
        }

        public void DisplayError(string strError)
        {
            txtErrorLog.Text = strError.Trim();
        }

        private void btnZeroPosition_Click(object sender, EventArgs e)
        {
            controller.MovePosition("X", 0);
            controller.MovePosition("Y", 0);
            controller.MovePosition("Z", 0);
            controller.MovePosition("U", 0);
            controller.MovePosition("V", 0);
            controller.MovePosition("W", 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            controller.ThreadStop();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            controller.SearchCommand(txtSend.Text.Trim());
        }
    }

}

