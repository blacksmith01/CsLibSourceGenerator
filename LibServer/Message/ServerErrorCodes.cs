using LibClient.Message;
using LIbCommon.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServer.Message
{
    [SrcGen_Enum_JoinEnums("LibGame.Message", "ErrorCodes", 1)]
    public enum ServerErrorCodes
    {
        _ServerErrorMinValue = ClientErrorCodes._CleintErrorMaxValue,

        /* Auth */
        Srv_Auth_Login_InvalidGameVersion,

    }
}
