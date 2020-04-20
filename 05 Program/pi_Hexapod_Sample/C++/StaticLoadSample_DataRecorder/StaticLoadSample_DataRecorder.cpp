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
// 1 )  Install the feature “PI_Programming_Files_PI_GCS2_DLL_Setup.exe” from the product CD. Afterwards, the required files will be located in “C:\Users\Public\PI\PI_Programming_Files_PI_GCS2_DLL”.
// 2 )  Copy these files to the location where the source code is built  ( .h. and .lib )  and where the application is executed  ( .dll ) .
//
//              : Sample Program to demonstrate usage of Lib file to link dll statically in MS VC++
//					- opening a connection via the interface dialog 
//					- query controller information  ( "qIDN" ) 
//					- referencing the hexapod  ( "FRF" )  if needed
//					- setup datarecorder
//					- move hexapod, record motion and display result

#include <windows.h>
#include <stdio.h>
#include <conio.h>
#include <time.h>
#include "PI_GCS2_DLL.h"


bool ReferenceIfNeeded ( int ID, char* axis ) 
{
	BOOL bReferenced;
	BOOL bFlag;
	if ( !PI_qFRF ( ID, axis, &bReferenced )  ) 
		return false;
	if ( !bReferenced ) 
	{// if needed,
		// reference the axis using the refence switch
		printf ( "Referencing axis %s...\n", axis );
		if ( !PI_FRF ( ID, axis )  ) 
			return false;

		// Wait until the reference move is done.
		bFlag = false;
		while ( bFlag != TRUE ) 
		{
			if ( !PI_IsControllerReady ( ID, &bFlag )  ) 
				return false;
		}
	}
	return true;
}


long ConnectFirstFoundHexapodViaTCPIP (  ) 
{
	char szFoundDevices [ 100000 ];
	printf ( "searching TCPIP devices...\n" );
	if ( 0 >= PI_EnumerateTCPIPDevices ( szFoundDevices, 99999, "" ) ) 
	{
		return -1;
	}
	char* szAddressToConnect = NULL;
	int port = 0;
	char * pch = strtok  ( szFoundDevices, "\n" );
	while ( NULL != pch ) 
	{
		_strupr ( pch );
		if ( 
			 (  ( NULL != strstr ( pch, "F-HEX" ) )  &&  ( NULL != strstr ( pch, "LISTENING" )  )  ) 
			|| (  ( NULL != strstr ( pch, "HEXAPOD" ) )  &&  ( NULL != strstr ( pch, "LISTENING" )  )  ) 
			|| (  ( NULL != strstr ( pch, "F-206" ) )  &&  ( NULL != strstr ( pch, "LISTENING" )  )  ) 
			|| (  ( NULL != strstr ( pch, "M-8" ) )  &&  ( NULL != strstr ( pch, "LISTENING" )  )  ) 
			|| (  ( NULL != strstr ( pch, "C-887" ) )  &&  ( NULL != strstr ( pch, "LISTENING" )  )  ) 
			 ) 
		{
			char* colon = strstr ( pch, ":" );
			if ( NULL == colon ) 
			{
				continue;
			}
			*colon = '\0';
			char* bracket = strstr ( pch, " ( " );
			if ( NULL == bracket ) 
			{
				continue;
			}
			szAddressToConnect = new char [strlen ( bracket + 1 ) +1 ];
			strcpy  ( szAddressToConnect, bracket + 1 );
			bracket = strstr ( colon + 1, " ) " );
			if ( NULL == bracket ) 
			{
				continue;
			}
			*bracket = '\0';
			port = atoi ( colon+1 );
			break;

		}
		pch = strtok  ( NULL, "\n" );
	}

	if ( NULL != szAddressToConnect ) 
	{
		printf ( "trying to connect with %s, port %d\n", szAddressToConnect, port );
		int iD = PI_ConnectTCPIP ( szAddressToConnect, port );
		delete []szAddressToConnect;
		return iD;
	}
	return -1;

}

bool SetupDataRecorder ( int iD, char* axis ) 
{
	// define the data recorder channels
	int iDataRecorderChannelIds[] = { 1, 2 };
	int iDataRecorderOptions[] = { 1, 2 };		// target (=commanded) position and real position of axis
	char szDataRecorderChannelSources[] = "                           ";
	sprintf ( szDataRecorderChannelSources, "%s %s", axis, axis );
	BOOL bOK = PI_DRC ( iD, iDataRecorderChannelIds, szDataRecorderChannelSources, iDataRecorderOptions );
	
	if ( !bOK ) 
	{
		printf ( "DRC failed\n" );
		return false;
	}
	// define the trigger setting to next motion command
	// this means, data will be written into the table as soon as the stage is commanded to 
	// move, regardless by which command
	int iTriggerOptions[] = { 1 };
	int iRecordTableRate = 10;

	iDataRecorderChannelIds[0] = 0;
	bOK = PI_DRT ( iD, iDataRecorderChannelIds, iTriggerOptions, "0", 1 );
	// set the record table rate, this means a sample will be taken each n-th cycle time, 
	// n being the iRecordTableRate value
	if ( bOK ) 
		bOK &= PI_RTR ( iD, iRecordTableRate );
	return  ( TRUE == bOK );
}

bool MoveRel ( int iD, const char *axis, double dVal ) 
{
	
	if ( !PI_MVR ( iD, axis, &dVal )  ) 
		return false;
		
	BOOL bFlag;
	int iTimeout = 100;
	do
	{// wait for motion to stop
		PI_IsMoving ( iD, axis, &bFlag );
		bFlag &= (  ( iTimeout-- ) > 0 );
		Sleep ( 100 );
	} while ( bFlag );
	return  ( iTimeout > 0 );
}

bool ReadRecordedData ( int iD ) 
{
	BOOL bOK = TRUE;
	double* dTableIndex;
	char szHeader [ 301 ];
	int iDataRecorderChannelIds[] = { 1, 2 };
	int iNReadChannels = 2;
	int iErr;
	int iNumberOfSamplesToRead = 100;
	PI_qERR ( iD, &iErr );

	// start reading asynchronously
	bOK = PI_qDRR ( iD, iDataRecorderChannelIds, iNReadChannels, 1, iNumberOfSamplesToRead, &dTableIndex, szHeader, 300 );

	// this means, the controller is now sending its recorded data. The function returns the header and
	// a pointer to memory allocated in the dll.
	// you could now analyze the header information in szHeader
	printf ( "GCS Header: \n%s\n", szHeader );
	if ( !bOK ) 
	{
		return false;
	}
	int iIndex = -1;
	printf ( "Reading...\n" );
	int iOldIndex = 0;
	int iTimeout = 20;
	do// wait until the read pointer has reached the number of expected samples
	{
		iOldIndex = iIndex;
		Sleep ( 100 );

		// while the controller sends data, the buffer index is increasing
		// this means the array pointed to by dTableIndex is filled with valid data
		iIndex = PI_GetAsyncBufferIndex ( iD );
		if ( iIndex == iOldIndex )  // if the index does not change for about 2 seconds, there is a problem
		{
			iTimeout--;
			if ( iTimeout < 0 ) 
			{
				printf ( "No more data after %d of %d samples\nStop Reading now.", iIndex, iNumberOfSamplesToRead * iNReadChannels );
				iNumberOfSamplesToRead =  ( iIndex - 1 )  / iNReadChannels;
				break;
			}
		}
		printf ( "\r %d", iIndex );
	} while ( iIndex < ( iNumberOfSamplesToRead * iNReadChannels )  );

	// after dTableIndex is filled with data, you should process it or stored it in a local variable,
	// as it will be cleared and/or overwritten with the next recording, e.g. started by a 
	// motion command
	printf ( "\n Finished\n" );
	int k;
	for ( iIndex = 0; iIndex < iNumberOfSamplesToRead; iIndex++ ) 
	{// print read data
	// the data columns 
	// c1_1 c2_1 c3_1 c4_1
	// c1_2 c2_2 c3_2 c4_2
	// ...
	// c1_n c2_n c3_n c4_n
	// are aligned as follows:
	// dTableIndex:
	// {c1_1,c2_1,c3_1,c4_1,c1_2,c2_2,...,c4_n}
		printf ( "%03d", iIndex );
		for ( k = 0; k < iNReadChannels; k++ ) 
		  printf ( "\t%05.05f", dTableIndex [ iIndex * iNReadChannels + k ] );
		printf ( "\n" );  
	}
	return true;
}

void ReportError ( int iD ) 
{
	int err = PI_GetError ( iD );
	char szErrMsg [ 300 ];
	if ( PI_TranslateError ( err, szErrMsg, 299 )  ) 
	{
		printf ( "Error %d occured: %s\n", err, szErrMsg );
	}
}

void CloseConnectionWithComment ( int iD, const char* comment ) 
{
	printf ( comment );
	ReportError ( iD );
	PI_CloseConnection ( iD );
	_getch (  );
}


int main ( int argc, char* argv[] ) 
{
	srand (  ( unsigned int ) time ( NULL )  );

	double dVal = 0.1;
	char szAxis[] = "X";

	// connect to controller 
	int iD = PI_InterfaceSetupDlg  (  ""  );

	// optional:
	// int iComPort = 1;
	// int iBaudRate = 115200;
	// int iD = pConnectRS232 ( iComPort,iBaudRate );

	// int iPort = 50000
	// int iD = PI_ConnectTCPIP  (  "XXX.XXX.XXX.XXX", iPort  );
	// int iD = ConnectFirstFoundHexapodViaTCPIP  (  );
	if ( -1 < iD ) 
	{
		char szIDN [ 200 ];
		if ( FALSE == PI_qIDN ( iD, szIDN, 199 ) ) 
		{
			CloseConnectionWithComment ( iD, "qIDN failed. Exiting.\n" );
			return FALSE;
		}
		printf ( "qIDN returned: %s\n", szIDN );

		if ( !ReferenceIfNeeded ( iD, szAxis )  ) 
		{
			CloseConnectionWithComment ( iD, "Not referenced, Referencing failed.\n" );
			return FALSE;
		}

		if ( !SetupDataRecorder ( iD, szAxis )  ) 
		{
			CloseConnectionWithComment ( iD, "Data Recorder could not be set up properly" );
			return FALSE;
		}

		// move relative
		dVal =  ( rand (  ) *1.0/RAND_MAX )  > 0 ? 0.5: -0.5;
		printf ( "Stepsize for movement: %f \n\n", dVal );

		printf  (  "Press any key to start hexapod motion.\n" );
		_getch  (  );

		if ( !MoveRel ( iD, szAxis, dVal )  ) 
		{
			CloseConnectionWithComment ( iD, "Motion failed\n" );
			return FALSE;
		}

		if ( !ReadRecordedData ( iD )  ) 
		{
			CloseConnectionWithComment ( iD, "could not read recorded data\n" );
			return FALSE;
		}
		
		PI_CloseConnection ( iD );
	}
	else
	{
		printf ( "Could not connect to Hexapod\n" );
	}

	printf ( "Done. Press any key to exit\n" );
	_getch (  );
	
	return 0;
}

