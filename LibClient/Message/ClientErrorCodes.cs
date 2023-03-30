using LIbCommon.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibClient.Message
{
    [SrcGen_Enum_JoinEnums("LibGame.Message", "ErrorCodes", 0)]
    public enum ClientErrorCodes
    {
        /* Common */
        Success = 0,

        Common_NotExecuted,
        Common_InvalidErrorCodes,
        Common_ServerInternalError,
        Common_ServerInternalException,

        /* Client Start */

        /* Auth */
        Auth_Login_InvalidGameVersion,

        /* Client End */
        _CleintErrorMaxValue,
    }
}
