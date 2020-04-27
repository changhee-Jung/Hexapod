using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Hexapod
{ 
    enum ErrorCode
    {
        PI_INVALID_ARGUMENT = -1015,
        PI_CNTR_NO_ERROR = 0,
        PI_CNTR_PARAM_SYNTAX,
        PI_CNTR_UNKNOWN_COMMAND,
        PI_CNTR_POS_OUT_OF_LIMITS = 7,
        PI_CNTR_INVALID_AXIS_IDENTIFIER = 15,
        PI_CNTR_CONTROLLER_BUSY = 1005
    }
}
