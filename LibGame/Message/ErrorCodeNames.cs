using LIbCommon.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGame.Message
{
    [SrcGen_Class_AddMemberUsingEnum("LibGame.Message.ErrorCodes", "Success")]
    public static partial class ErrorCodeNames
    {
        public readonly static string Success = ErrorCodes.Success.ToString();
    }
}
