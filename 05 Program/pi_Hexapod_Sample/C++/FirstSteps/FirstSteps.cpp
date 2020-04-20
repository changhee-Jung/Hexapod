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
//				: Sample Program to demonstrate
//					- opening a connection via RS232 or optional TCP/IP  
//					- query controller information ("qIDN")
//					- referencing the hexapod ("FRF") if needed
//					- random six axes movements of hexapod

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


bool ReferenceIfNeeded ( int ID )
{
	BOOL bReferenced;
	BOOL bFlag;
	char axis[] = "X";
	if ( !PI_qFRF ( ID, axis, &bReferenced ) )
		return false;
	if ( !bReferenced )
	{// if needed,
		// reference the axis using the refence switch
		printf ( "Referencing axis %s...\n", axis );
		if ( !PI_FRF ( ID, axis ) )
			return false;

		// Wait until the reference move is done.
		bFlag = false;
		while ( TRUE != bFlag )
		{
			if ( !PI_IsControllerReady ( ID, &bFlag ) )
				return false;
		}
	}
	return true;
}

bool MoveToPosition ( int ID, char* axis, double *dVal )
{
	if ( !PI_MOV ( ID, axis, dVal ) )
		return FALSE;
	
	// Wait until the closed loop move is done.
	BOOL bIsMoving[6] = { TRUE, TRUE, TRUE, TRUE, TRUE, TRUE };
	double dPos[6] = { 0, 0 ,0 , 0, 0, 0 };
	
	while ( (( TRUE == bIsMoving[0] ) ||

			 ( TRUE == bIsMoving[1] ) ||
			 ( TRUE == bIsMoving[2] ) ||
			 ( TRUE == bIsMoving[3] ) ||
			 ( TRUE == bIsMoving[4] ) ||
			 ( TRUE == bIsMoving[5] ) ) && ( !_kbhit () ) 
		  )
	{
		if ( !PI_qPOS ( ID, "X Y Z U V W", dPos ) )
			return FALSE;
		if ( !PI_IsMoving ( ID, "X Y Z U V W", bIsMoving ) )
			return FALSE;
		printf ( "Current position axis (%s): %f,%f,%f,%f,%f,%f \n", axis, dPos[0], dPos[1], dPos[2], dPos[3], dPos[4], dPos[5] );
		Sleep ( 50 );
	}

	printf ( "############ Position reached ############\n\n" );
	
	Sleep ( 1000 );
	return true;
}

int main ( int argc, char* argv[] )
{
	srand ( ( int ) time ( NULL ) );
	char axis[] = "X Y Z U V W";		// modify here to change moved axes
	double dMaxTravelPositive[6];
	BOOL bIsMoving = TRUE;
	double dPos[6] = { 0, 0, 0, 0, 0, 0 };

	// connect to controller 
	int ID = PI_InterfaceSetupDlg ( "" );

	// optional:
	// int iComPort = 1;
	// int iBaudRate = 115200;
	// int iID = pConnectRS232(iComPort,iBaudRate);

	// int iPort = 50000
	// int ID = PI_ConnectTCPIP ( "XXX.XXX.XXX.XXX", iPort );
	// int ID = ConnectFirstFoundHexapodViaTCPIP ();
	if ( 0 > ID )
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
	if ( FALSE == ReferenceIfNeeded ( ID ) )
	{
		printf ( "Not referenced, Referencing failed.\n" );
		return FALSE;
	}

	// get max positive travelrange
	PI_qTMX ( ID, "X Y Z U V W", dMaxTravelPositive );

	printf ( "Press any key to start hexapod motion.\n");
	_getch ();

	do // random motion until key pressed
	{
		
		// generate random position within travelrange
		BOOL bMovePossible = false;
		do 
		{
			for (int i = 0; i < 6; i++) 
				dPos[i] = 0.01 * ( rand () % ( int ) ( 100 * dMaxTravelPositive[i] ) );
			
			PI_qVMO ( ID, axis, dPos, &bMovePossible );
		}
		while ( FALSE == bMovePossible );

		if ( !MoveToPosition ( ID, axis, dPos ) )
			throw ( -4 );

	} while ( !_kbhit () );

	printf ( "Stopping hexapod motion. Press any key to exit.\n" );

	PI_STP ( ID );
	_getch ();
	PI_CloseConnection ( ID );
	_getch ();
	return 0;
}

