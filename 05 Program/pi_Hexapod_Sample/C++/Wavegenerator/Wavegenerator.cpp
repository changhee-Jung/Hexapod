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
// To build and execute the sample application, the following additional files are required:
// - PI_GCS2_DLL.h
// - PI_GCS2_DLL.lib
// - PI_GCS2_DLL.dll
// - PI_GCS2_DLL_x64.lib
// - PI_GCS2_DLL_x64.dll
//
// Proceed as follows to make these files available for the application:
// 1) Install the feature “PI_Programming_Files_PI_GCS2_DLL_Setup.exe” from the product CD. Afterwards, the required files will be located in “C:\Users\Public\PI\PI_Programming_Files_PI_GCS2_DLL”.
// 2) Copy these files to the location where the source code is built (.h. and .lib) and where the application is executed (.dll).
//
//              : Wavegenerator Sample Program (Version 1.2.0.0) to demonstrate
//					- opening a connection via the interface dialog
//					- query controller information ("qIDN")
//					- referencing the hexapod ("FRF") if needed
//					- using the wavegenerator to realize a cyclic, circular motion in the xy-plane
//                 
//                  This sample was designed to work with C-887.5X controllers and fast hexapods (H-811, H-820, H-840.D, ...). 
//                  Please adjust "dAmplitudeOfWave" and/or "iTableRateArray" when working with slow hexapods (H-840.H, H-850.H,...) 
//                  or other hexapod-controllers (C-887.11, C-887.21, ...) 
//                  Only valid for C-887.5X with FW-version: V 2.2.0.0 and higher


#include <conio.h>
#include <stdio.h>
#include <windows.h>
#include <time.h>
#include ".\Pi_gcs2_dll.h"


long ConnectFirstFoundHexapodViaTCPIP ()
{
	char szFoundDevices [ 100000 ];
	printf ( "Searching TCPIP devices...\n" );
	if ( 0 ==  PI_EnumerateTCPIPDevices ( szFoundDevices, 99999, "" ) )
	{
		return -1;
	}
	char* szAddressToConnect = NULL;
	int port = 0;
	printf ( "####= %s\n", szFoundDevices );
	char* pch = strtok ( szFoundDevices, "\n" );
	while ( pch != NULL )
	{
		_strupr( pch );
		printf ( "Devicename= %s\n", pch );
		if (
			( ( NULL != strstr ( pch, "F-HEX" ) ) && ( NULL != strstr ( pch, "LISTENING" ) ) )
			|| ( ( NULL != strstr ( pch, "HEXAPOD" ) ) && ( NULL != strstr ( pch, "LISTENING" ) ) )
			|| ( ( NULL != strstr ( pch, "F-206" ) ) && ( NULL != strstr ( pch, "LISTENING" ) ) )
			|| ( ( NULL != strstr ( pch, "M-8" ) ) && ( NULL != strstr ( pch, "LISTENING" ) ) )
			|| ( ( NULL != strstr ( pch, "C-887" ) ) && ( NULL != strstr ( pch, "LISTENING" ) ) )
			)
		{
			char* colon = strstr ( pch, ":" );
			*colon = '\0';
			szAddressToConnect = new char [ strlen ( strstr ( pch, "(") + 1 ) + 1 ];
			strcpy ( szAddressToConnect, strstr ( pch, "(") + 1 );

			*strstr ( colon + 1, ")" ) = '\0';
			port = atoi ( colon + 1 );
			printf (" Addr= >%s< port=>%d< \n", szAddressToConnect, port );
			break;
		}
		pch = strtok ( NULL, "\n" );
	}

	if ( szAddressToConnect != NULL )
	{
		printf ( "Trying to connect with %s, port %d\n", szAddressToConnect, port );
		int iD = PI_ConnectTCPIP ( szAddressToConnect, port );
		delete []szAddressToConnect;
		return iD;
	}
	return -1;
}


bool ReferenceIfNeeded ( int ID, char* axis )
{
	BOOL bReferenced;
	BOOL bFlag;
	if ( !PI_qFRF ( ID, axis, &bReferenced ) )
		return false;
	if ( !bReferenced )
	{// if needed,
		// reference the axis using the refence switch
		printf ( "Referencing axis %s...\n", axis );
		if ( !PI_FRF ( ID, axis ) )
			return false;

		// Wait until the reference move is done
		bFlag = false;
		while ( TRUE != bFlag )
		{
			if ( !PI_IsControllerReady ( ID, &bFlag ) )
				printf ( "Exit due to an error!\n" );
				return false;
		}
	}
	return true;
}


bool MoveTo ( int ID, char* axis, double *dVal )
{
	printf ( "Moving axis %s to startposition  %f |  %f...\n", axis, dVal[0], dVal[1] );

	if ( !PI_MOV ( ID, axis, dVal ) )
		return FALSE;

	// wait until move is done
	BOOL bIsMoving = TRUE;
	double dPos[2];
	while ( ( TRUE == bIsMoving ) && ( !_kbhit () ) )
	{
		if ( !PI_qPOS ( ID, axis, dPos ) )
			return FALSE;
		if ( !PI_IsMoving ( ID, "Y", &bIsMoving ) )
			return FALSE;
		printf ( "Current position(%s): %f | %f \n", axis, dPos[0], dPos[1] );
	}
	return true;
}


bool WaitForWaveGenerator ( int ID, char* axis )
{
	printf ( "Waiting for wavegenerator to finish\n" );

	BOOL bIsMoving = TRUE;
	double dPos[2];
	while ( ( TRUE == bIsMoving ) && ( !_kbhit () ) )
	{
		if ( !PI_qPOS ( ID, axis, dPos ) )
			return FALSE;
		if ( !PI_IsMoving ( ID, "y", &bIsMoving ) )
			return FALSE;
		if ( _kbhit () )
		{
			PI_STP ( ID );
			_getch ();
			return FALSE;
		}
		printf ( "Current position <%s>: %f | %f \n", axis, dPos[0], dPos[1] );
	}
	return true;
}


int main ( int argc, char* argv[] )
{
	// connect to controller 
	int ID = PI_InterfaceSetupDlg ( "" );

	// optional:
	// int iComPort = 1;
	// int iBaudRate = 115200;
	// int iID = pConnectRS232(iComPort,iBaudRate);

	// int iPort = 50000
	// int ID = PI_ConnectTCPIP ( "XXX.XXX.XXX.XXX", iPort );
	// int ID = ConnectFirstFoundHexapodViaTCPIP ();
	if ( ID < 0 )
	{
		printf ( "Connection error!\n" );
		_getch ();
		return FALSE;
	}

	// get controller information
	char szIDN [ 2000 ];
	if ( FALSE == PI_qIDN ( ID, szIDN, 1999 ) )
	{
		printf ( "qIDN failed. Exiting.\n" );
		return FALSE;
	}
	printf ( "qIDN returned: %s\n", szIDN );
	
	// reference hexapod if needed
	// use "X" for all hexapod axes
	if ( FALSE == ReferenceIfNeeded ( ID, "X" ) )
	{
		printf ( "Not referenced, Referencing failed.\n" );
		return FALSE;
	}

	// define sine-waveform using shape-parameters
	int iWaveTableID_1					= 10;
    int iWaveTableID_2					= 20;
	int iOffsetOfFirstPointInWaveTable	= 0;		// = "StartPoint"
	int iNumberOfPoints					= 1000;		// = "Curve length in points"
	int iAddAppendWave					= 0;		// = "0" = clears the wave table and starts writing with the first point in the table
	int iCenterPointOfWave				= 500;		// = "CurveCenterPoint"
	double dAmplitudeOfWave				= 2;
	double dOffsetOfWave				= 0;
	int iSegmentLength					= 1000;

	PI_WAV_SIN_P ( ID, iWaveTableID_1, iOffsetOfFirstPointInWaveTable, iNumberOfPoints, iAddAppendWave, iCenterPointOfWave, dAmplitudeOfWave, dOffsetOfWave, iSegmentLength );


	// define 90 deg shifted sine-waveform in order to realize a circle 
    iOffsetOfFirstPointInWaveTable		= 250;
	
	PI_WAV_SIN_P ( ID, iWaveTableID_2, iOffsetOfFirstPointInWaveTable, iNumberOfPoints, iAddAppendWave, iCenterPointOfWave, dAmplitudeOfWave, dOffsetOfWave, iSegmentLength );
	

	// define assignment of wavetables and wavegenerators
	const int iWaveGenerator[2]			= { 1, 2 };
	const int iWaveTable[2]				= { iWaveTableID_1, iWaveTableID_2 };
	
	PI_WSL ( ID, iWaveGenerator, iWaveTable, 2 );


	// define number of repetitions = 10
	const int iNumCycles[2]				= { 10, 10 };

	PI_WGC ( ID, iWaveGenerator, iNumCycles, 2 );


	// define 1 ms cycle-time with linear interpolation
	const int iTableRateArray[2]		= { 10, 10 };
	const int iInterpolationsType[2]	= { 1, 1 };
             
    PI_WTR ( ID, iWaveGenerator, iTableRateArray, iInterpolationsType, 2 ); 


	// move to start position of sine-waveform
	// !! adjust start-position when changing waveform or assignment !!
	char axis[] = "X Y";
	double dPos[2];
	dPos[0] = 0;
	dPos[1] = 1;
	printf ( "Press any key to start hexapod motion.\n" );
	_getch ();
	
	MoveTo ( ID, axis, dPos );


	// start wavegenerator
	const int iStartMode[2]				= { 1, 1 };
	printf ( "Start wavegenerator\n" );

	PI_WGO ( ID, iWaveGenerator, iStartMode, 2 );

	
	if ( WaitForWaveGenerator ( ID, axis ) )
		printf ( "Done\n" );
	else
		printf ( "Stopped\n" );


	printf ( "Press any key to exit\n" );
	PI_CloseConnection ( ID );
	_getch ();
	return 0;
}

