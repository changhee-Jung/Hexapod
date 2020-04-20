//(c)2018 Physik Instrumente (PI) GmbH & Co. KG 
//Software products that are provided by PI are subject to the General Software License Agreement of Physik Instrumente
//(PI) GmbH & Co. KG and may incorporate and/or make use of third-party software components. 
//
//For more information, please read the General Software License Agreement and the Third Party Software Note linked below. 
//General Software License Agreement: 
//http://www.physikinstrumente.com/download/EULA_PhysikInstrumenteGmbH_Co_KG.pdf 
//Third Party Software Note: 
//http://www.physikinstrumente.com/download/TPSWNote_PhysikInstrumenteGmbH_Co_KG.pdf
//
//              : Wavegenerator Sample Program (Version 1.2.0.0) to demonstrate
//					- opening a connection via TCP/IP or RS232
//					- query controller information ("qIDN")
//					- referencing the hexapod ("FRF") 
//					- using the wavegenerator to realize a cyclic, circular motion in the xy-plane
//                  This sample was designed to work with C-887.5X controllers and fast hexapods (H-811, H-820, H-840.D, ...). 
//                  Please adjust "dAmplitudeOfWave" and/or "iTableRateArray" when working with slow hexapods (H-840.H, H-850.H,...) 
//                  or other hexapod-controllers (C-887.11, C-887.21, ...) 
//                  Only valid for C-887.5X with FW-version: V 2.2.1.0 and higher


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using PI;
namespace Wavegenerator
{
    public partial class MainWindow : Form
    {
        private int ID;
        static System.Timers.Timer _timer;
        int iWaveTableID_1 = 10;
        int iWaveTableID_2 = 20;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            // connect to controller via dialog
            ID = PI.GCS2.InterfaceSetupDlg("");
            if (-1 < ID)
            {
                GetInterfaceInformation();
                btnFRF.Enabled = true;


                btnWAV.Enabled = true;

                int[] iQref = new int[1];
                PI.GCS2.qFRF(ID, "X", iQref);

                if (1 == iQref[0])
                {
                    label2.Text = "referenced";
                    btnStartWG.Enabled = true;
                    btnMOV.Enabled = true;
                }

                // timer for cyclic "ERR?" query
                _timer = new System.Timers.Timer(3 * 1000);
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
            }
        }


        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // in case of an error, display errortext
            int iErr = PI.GCS2.GetError(ID);
            if (0 != iErr)
            {
                StringBuilder sErrText = new StringBuilder(256);
                PI.GCS2.TranslateError(iErr, sErrText, 256);
                MessageBox.Show("Error: " + iErr.ToString() + " (" + sErrText.ToString() + ")");
            }
        }


        public void GetInterfaceInformation()
        {
            // query and display "IDN?"
            StringBuilder IdnBuffer = new StringBuilder(256);
            PI.GCS2.qIDN(ID, IdnBuffer, 255);
            IDNDisplay.Text = IdnBuffer.ToString();
            btnConnect.Enabled = false;
        }


        private void btnFRF_Click(object sender, EventArgs e)
        {
            // reference hexapod and wait until hexapod is referenced
            // use "X" for all hexapod axes
            Cursor = Cursors.WaitCursor;
            PI.GCS2.FRF(ID, "X");

            int[] iQref = new int[1];

            while (0 == iQref[0])
            {
                PI.GCS2.qFRF(ID, "X", iQref);
                Thread.Sleep(50);
            }
            label2.Text = "referenced";
            btnMOV.Enabled = true;
            btnStartWG.Enabled = true;

            Cursor = Cursors.Default;
        }


        private void btnMOV_Click(object sender, EventArgs e)
        {
            // move to start-position of wavetables
            // !! adjust start-position when changing waveform or assignment !!
            double[] dVals = new double[2];
            dVals[0] = 0;
            dVals[1] = 1;

            PI.GCS2.MOV(ID, "X Y", dVals);
        }


        private void btnWAV_Click(object sender, EventArgs e)
        {
            // define sine-waveform using shape-parameters
            int iOffsetOfFirstPointInWaveTable = 0;     // = "StartPoint"
            int iNumberOfPoints = 1000;                 // = "Curve length in points"
            int iAddAppendWave = 0;                     // = "0" = clears the wave table and starts writing with the first point in the table
            int iCenterPointOfWave = 500;               // = "CurveCenterPoint"
            double dAmplitudeOfWave = 2;
            double dOffsetOfWave = 0;
            int iSegmentLength = 1000;

            PI.GCS2.WAV_SIN_P(ID, iWaveTableID_1, iOffsetOfFirstPointInWaveTable, iNumberOfPoints, iAddAppendWave, iCenterPointOfWave, dAmplitudeOfWave, dOffsetOfWave, iSegmentLength);

            // define 90 deg shifted sine-waveform in order to realize a circle 
            iOffsetOfFirstPointInWaveTable = 250;

            PI.GCS2.WAV_SIN_P(ID, iWaveTableID_2, iOffsetOfFirstPointInWaveTable, iNumberOfPoints, iAddAppendWave, iCenterPointOfWave, dAmplitudeOfWave, dOffsetOfWave, iSegmentLength);
        }


        private void btnStartWG_Click(object sender, EventArgs e)
        {
            // define assignment of wavetables and wavegenerators
            int[] iWaveTable = new int[2];
            iWaveTable[0] = iWaveTableID_1;
            iWaveTable[1] = iWaveTableID_2;

            int[] iWaveGenerator = new int[2];
            iWaveGenerator[0] = 1;
            iWaveGenerator[1] = 2;

            PI.GCS2.WSL(ID, iWaveGenerator, iWaveTable, 2);


            // define number of repetitions
            int[] iNumRep = new int[2];

            if ("" == txtWGC.Text)
            {
                iNumRep[0] = 0;
                iNumRep[1] = 0;
            }
            else
            {
                iNumRep[0] = Convert.ToInt16(txtWGC.Text);
                iNumRep[1] = Convert.ToInt16(txtWGC.Text);
            }

            PI.GCS2.WGC(ID, iWaveGenerator, iNumRep, 2);


            // define 1ms cycle time with linear interpolation
            int[] iTableRateArray = new int[2];
            iTableRateArray[0] = 10;        // 10 = 1 ms
            iTableRateArray[1] = 10;

            int[] iInterpolationsType = new int[2];
            iInterpolationsType[0] = 1;     // 1 = linear interpolation
            iInterpolationsType[1] = 1;

            PI.GCS2.WTR(ID, iWaveGenerator, iTableRateArray, iInterpolationsType, 2);


            // start wavegenerator
            int[] iStartMode = new int[2];
            iStartMode[0] = 1;              // 1 = start
            iStartMode[1] = 1;
            PI.GCS2.WGO(ID, iWaveGenerator, iStartMode, 2);
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            PI.GCS2.STP(ID);
        }
    }
}