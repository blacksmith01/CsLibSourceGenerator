using LIbCommon.SourceGenerators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibGame
{
    public static partial class LoggerEx
    {
        public class SepP { public static readonly SepP G = new(); }
        public static void Info(this ILogger logger, string msg
            , SepP sep = null!, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            logger.LogDebug(msg);
        }

        [SrcGen_Method_GenericParamInc("t", "r", 20)]
        public static void Info<T1>(this ILogger logger, string fmt, T1 t1
           , SepP sep = null!, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            logger.LogInformation(fmt, t1);
        }
    }
}
