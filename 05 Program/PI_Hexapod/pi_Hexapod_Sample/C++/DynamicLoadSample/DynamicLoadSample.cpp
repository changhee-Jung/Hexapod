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
//				: Sample Program to demonstrate usage of Lib file to link dll statically in MS VC++
//					- opening a connection via RS232 or TCP/IP  
//					- referencing the hexapod ("FRF")
//					- random movement with X axis of hexapod

#define GCS_DLLNAME  "PI_GCS2_DLL.dll"
#define GCS_FUNC_PREFIX "PI_"
#include <stdio.h>
#include <conio.h>
#include <windows.h>
#include <time.h>
typedef BOOL ( WINAPI *FP_ConnectRS232 ) ( int, int );
typedef BOOL ( WINAPI *FP_CloseConnection ) ( int );
typedef BOOL ( WINAPI *FP_qIDN ) ( int, char*, int );
typedef BOOL ( WINAPI *FP_STP ) ( int );
typedef BOOL ( WINAPI *FP_GcsCommandset ) ( int, char* );
typedef BOOL ( WINAPI *FP_GcsGetAnswer ) ( int, char*, int );
typedef BOOL ( WINAPI *FP_GcsGetAnswerSize ) ( int, int* );
typedef BOOL ( WINAPI *FP_qERR ) ( int, int* );
typedef BOOL ( WINAPI *FP_IsMoving ) ( int, char*, BOOL* );
typedef BOOL ( WINAPI *FP_qFRF ) ( int, char*, BOOL* );
typedef BOOL ( WINAPI *FP_qTMX ) ( int ID, const char* szAxes, double* pdValueArray );
typedef long ( WINAPI *FP_ConnectTCPIP ) ( const char*, long );
typedef long ( WINAPI *FP_EnumerateTCPIPDevices ) ( char*, long, const char* );

// Function Pointer Variables
FP_ConnectRS232 pConnectRS232;
FP_qIDN pqIDN;
FP_CloseConnection pCloseConnection;
FP_GcsCommandset pGcsCommandset;
FP_GcsGetAnswer pGcsGetAnswer;
FP_GcsGetAnswerSize pGcsGetAnswerSize;
FP_qERR pqERR;
FP_IsMoving pIsMoving;
FP_qFRF	pqFRF;
FP_ConnectTCPIP pConnectTCPIP;
FP_EnumerateTCPIPDevices pEnumerateTCPIPDevices;
FP_qTMX pqTMX;
FP_STP pSTP;

bool IsControllerReady ( int iID )
{
	int iAS = 0;
	// you have to make sure the answer buffer is emptied before calling this function
	pGcsCommandset ( iID, "\7" );
	while( 0 == iAS )
	{
		Sleep ( 50 );
		pGcsGetAnswerSize ( iID, &iAS );
	}
	char szAns[10];
	pGcsGetAnswer ( iID, szAns, 9 );
	return szAns[0] & 1;
	
}


bool ReferenceStage ( int iID, char* szAxis )
{
	char szCommand[50];

	sprintf ( szCommand, "FRF %s", szAxis );
	if ( !pGcsCommandset ( iID, szCommand ) )
		return false;
	do
	{
		Sleep ( 1 );
	} while ( !IsControllerReady ( iID ) );
	BOOL bOK;
	pqFRF ( iID, szAxis, &bOK );
	return ( TRUE == bOK );
}


bool MoveTo ( int iID, char* szAxis, double dTarget )
{
	BOOL bOK = true;
	char szCommand [ 50 ];
	sprintf ( szCommand, "MOV %s %g", szAxis, dTarget );
	if ( !pGcsCommandset ( iID, szCommand ) )
		return false;
	BOOL bFlag;
	printf ( "Moving..." );
	do
	{	printf ( "." );
		Sleep ( 100 );
		bOK &= pIsMoving ( iID, szAxis, &bFlag );
		
		if ( _kbhit () )
			return true;

	} while ( bFlag );

	sprintf ( szCommand, "POS? %s", szAxis );
	if ( !pGcsCommandset ( iID, szCommand ) )
		return false;
	int iAS = 0;
	while ( 0 == iAS )
	{
		Sleep ( 50 );
		bOK &= pGcsGetAnswerSize ( iID, &iAS );
	}

	char szAns [ 50 ];
	bOK &= pGcsGetAnswer ( iID, szAns, 49 );
	printf ( "\nPosition after move: %s\n", szAns );
	return ( TRUE == bOK );
}


long ConnectFirstFoundHexapodViaTCPIP ()
{
	char szFoundDevices [ 100000 ];
	printf ( "searching TCPIP devices...\n" );
	if ( 0 == pEnumerateTCPIPDevices ( szFoundDevices, 99999, "" ) )
	{
		return -1;
	}
	char* szAddressToConnect = NULL;
	int port = 0;
	char * pch = strtok ( szFoundDevices, "\n" );
	while (NULL != pch )
	{
		_strupr ( pch );
		
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
			szAddressToConnect = new char [ strlen ( strstr ( pch, "(" ) + 1 ) + 1 ];
			strcpy ( szAddressToConnect, strstr ( pch, "(" ) + 1 );

			*strstr ( colon + 1, ")" ) = '\0';
			port = atoi ( colon + 1 );
			break;
		}
		pch = strtok ( NULL, "\n" );
	}

	if ( NULL != szAddressToConnect )
	{
		printf ( "trying to connect to %s, port %d\n", szAddressToConnect, port );
		int iD = pConnectTCPIP ( szAddressToConnect, port );
		delete []szAddressToConnect;
		return iD;
	}
	return -1;

}

void LoadFunctions ( HINSTANCE hPI_Dll )
{
	char* szFuncName [ 150 ];
	try
	{
		// get function pointers
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX , "ConnectRS232" ) ;
		pConnectRS232 = ( FP_ConnectRS232 ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pConnectRS232 ) 
			throw ( -1 ) ;
		sprintf (  ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "qIDN" ) ;
		pqIDN = ( FP_qIDN ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pqIDN ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "CloseConnection" ) ;
		pCloseConnection = ( FP_CloseConnection ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pCloseConnection ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "GcsCommandset" ) ;
		pGcsCommandset = ( FP_GcsCommandset ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pGcsCommandset ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "GcsGetAnswer" ) ;
		pGcsGetAnswer = ( FP_GcsGetAnswer ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pGcsGetAnswer ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "GcsGetAnswerSize" ) ;
		pGcsGetAnswerSize = ( FP_GcsGetAnswerSize ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pGcsGetAnswerSize ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "qERR" ) ;
		pqERR =  ( FP_qERR ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pqERR ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "IsMoving" ) ;
		pIsMoving = ( FP_IsMoving ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pIsMoving ) 
			throw ( -1 ) ;
		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "qFRF" ) ;
		pqFRF = ( FP_qFRF ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pqFRF ) 
			throw ( -1 ) ;

		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "qTMX" ) ;
		pqTMX = ( FP_qTMX ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pqTMX ) 
			throw ( -1 ) ;

		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "STP" ) ;
		pSTP = ( FP_STP ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pSTP ) 
			throw ( -1 ) ;


		sprintf ( ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "ConnectTCPIP" ) ;
		pConnectTCPIP = ( FP_ConnectTCPIP ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pConnectTCPIP ) 
			throw ( -1 ) ;
		sprintf (  ( char* ) szFuncName, "%s%s", GCS_FUNC_PREFIX, "EnumerateTCPIPDevices" ) ;
		pEnumerateTCPIPDevices = ( FP_EnumerateTCPIPDevices ) GetProcAddress ( hPI_Dll, ( LPCSTR ) szFuncName ) ;
		if ( NULL == pEnumerateTCPIPDevices ) 
			throw ( -1 ) ;

	}
	catch (...)
	{
		char* msg = new char [ 100 ];
		sprintf ( msg, "Loading %s failed", szFuncName );
		throw ( msg );
	}
}
int main ( int argc, char* argv[] )
{
	int iID = -1;
	int iComPort = 1;
	int iBaudRate = 115200;
	char szAxis[] = "X";
	double dMaxTravelPositive;


	srand ( ( int ) time ( NULL ) );
	char szDLLDirectory[] = ".\\";
	char* szGCSDLLName = new char [ strlen ( szDLLDirectory ) + strlen ( GCS_DLLNAME ) + 1 ];
	sprintf ( szGCSDLLName, "%s%s", szDLLDirectory, GCS_DLLNAME );
	printf ( "Open %s dynamically\n", szGCSDLLName );
	HINSTANCE hPI_Dll = LoadLibrary ( szGCSDLLName );
	if ( hPI_Dll != NULL )
	{
		printf ( "LoadLibrary(\"%s\") successfull\n", szGCSDLLName );
		try
		{
			LoadFunctions ( hPI_Dll );
			printf ( "All function pointers are loaded\n" );

			// connect to controller 
			int iPort = 50000;
			int iID = pConnectTCPIP ( "XXX.XXX.XXX.XXX", iPort );

			// int iID = pConnectRS232 ( iComPort, iBaudRate );
			// int iID = ConnectFirstFoundHexapodViaTCPIP ();
			if ( 0 > iID )
				throw ( -2 );
			char szIDN [ 100 ];
			if ( !pqIDN ( iID, szIDN, 99) )
				throw ( -3 );
			printf ( "Successfully connected to %s\n", szIDN );

			// Reference
			if ( !ReferenceStage ( iID, szAxis ) )
				throw ( -4 );


			// get max positive travelrange
			pqTMX ( iID, szAxis, &dMaxTravelPositive );

			printf ( "Press any key to start hexapod motion.\n" );
			_getch ();


			double dPos;
			do // random motion of axis "X" between 0 and "dMaxTravelPositive"
			{
				dPos = rand() % ( int ) dMaxTravelPositive;
				printf ( "Targetposition: %f \n", dPos );
				Sleep ( 1000 );
				if ( !MoveTo ( iID, szAxis, dPos ) )
					throw ( -4 );
			} while ( !_kbhit () );

			printf ( "\nStopping hexapod motion. Press any key to exit.\n" );
			pSTP ( iID );
			_getch ();
			pCloseConnection ( iID );
			iID = -1;
		}
		catch ( int iErr )
		{
			switch ( iErr )
			{
			
			case ( -2 ):
				{
					printf ( "Connecting failed\n" );
				}break;
			case ( -3 ):
				{
					int iGCSERR;
					if ( !pqERR ( iID, &iGCSERR ) )
						printf ( "Reading the GCS error failed\n" );
					else
						printf ( "A GCS function failed with Error %d\n", iGCSERR );
					pCloseConnection ( iID );
				}
			case ( -4 ):
				{
					printf ( "A Special Function failed\n" );
					pCloseConnection ( iID );
				}
			}
		}
		catch ( char* Msg )
		{
			printf ( "Error: %s\n", Msg );
			delete[] Msg;
		}
		catch (...)
		{

		}
		if ( 0 > iID )
		{
			pCloseConnection ( iID );
		}
	}
	else
	{
		LPVOID lpMsgBuf;
		DWORD dw = GetLastError (); 
		FormatMessage (
			FORMAT_MESSAGE_ALLOCATE_BUFFER | 
			FORMAT_MESSAGE_FROM_SYSTEM |
			FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL,
			dw,
			MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT ),
			( LPTSTR ) &lpMsgBuf,
			0, NULL );

		printf ( "LoadLibrary(\"%s\") failed with error %d:\n%s\n", szGCSDLLName, dw, lpMsgBuf );
		LocalFree ( lpMsgBuf );
	}
	_getch ();
	delete[] szGCSDLLName;
	return 0;
}

