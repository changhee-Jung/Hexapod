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
#include "GCSMethods.h"
#include "Helper.h"
#include "Menu.h"


int main ()
{
    int ID = -1;
    int MenuSelector = 0;

    //get Ini-file data
    IniConfs l_IniConfs;
    StringScans l_StringScans;
    if (!GetIniFile ("FA.ini", l_IniConfs, l_StringScans))
    {
        std::cout << "GetIniFile() == FALSE" << "\n";
        std::cout << "Press any key to exit" << "\n";
        std::cin.get ();
        return 0;
    }

    //Convert INI data to struct
    FDRScans l_FDRs;
    FDGScans l_FDGs;
    ConvertStringScansToFDRScans (l_StringScans, l_FDRs);
    ConvertStringScansToFDGScans (l_StringScans, l_FDGs);

    //Connect
    ConnectMap Connections;
    if (!ControllerConnect (l_IniConfs, &ID, Connections))
    {
        std::cout << "ControllerConnect() == FALSE" << "\n";
    }

    //StringScans (INI) => Controller
    SetControllerFDRScans (Connections, l_FDRs);
    SetControllerFDGScans (Connections, l_FDGs);

    //Readback FDR-/FDGScans (merge INI-data scans + controller scans)
    l_FDRs.clear ();
    l_FDGs.clear ();
    GetControllerFDRScans (Connections, l_FDRs);
    GetControllerFDGScans (Connections, l_FDGs);

    ID = -1;
    //Menue
    while (0 <= MenuSelector)
    {
        Header (ID, Connections);
        switch (MenuSelector)
        {
            case (Menu::eMainMenu):
                MainMenue (MenuSelector);
                break;
            case (Menu::eSelectController):
                SelectController (&ID, MenuSelector, Connections);
                break;
            case (Menu::eAddEditRoutine):
                AddEditRoutine (ID, MenuSelector, l_FDRs, l_FDGs, Connections);
                break;
            case (Menu::eStartSingleRoutineDR):
                StartSingleRoutineDR (ID, MenuSelector, l_FDRs, l_FDGs, Connections);
                break;
            case (Menu::eStartMultipleRoutine):
                StartMultipleRoutine (ID, MenuSelector, l_FDRs, l_FDGs, Connections);
                break;
            case (Menu::eShowAnalogValues):
                ShowAnalogValues (ID, MenuSelector);
                break;
            case (Menu::eMoveAxes):
                MoveAxes (ID, MenuSelector, l_FDRs, l_FDGs);
                break;

            default:
                MenuSelector = 0;
        }   
    }
    return 0;
}

