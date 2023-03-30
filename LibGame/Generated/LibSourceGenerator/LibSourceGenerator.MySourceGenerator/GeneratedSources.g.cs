using LIbCommon.SourceGenerators;
using System;
using LibGame;
using LibClient.Message;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace LibGame.Message
{
    public enum ErrorCodes
    {
        Success = 0,
        Common_NotExecuted = 1,
        Common_InvalidErrorCodes = 2,
        Common_ServerInternalError = 3,
        Common_ServerInternalException = 4,
        Auth_Login_InvalidGameVersion = 5,
        _CleintErrorMaxValue = 6,
        _ServerErrorMinValue = 6,
        Srv_Auth_Login_InvalidGameVersion = 7
    }
}

namespace LibGame
{
    public static partial class GameDefines
    {
        public static EntityTypeFlags ToEntityTypeFlags(EntityTypes value)
        {
            switch (value)
            {
                case EntityTypes.None:
                    return EntityTypeFlags.None;
                case EntityTypes.Player:
                    return EntityTypeFlags.Player;
                case EntityTypes.Monster:
                    return EntityTypeFlags.Monster;
                case EntityTypes.Object:
                    return EntityTypeFlags.Object;
                default:
                    return default(EntityTypeFlags);
            }
        }
    }
}

namespace LibGame
{
    public static partial class GameDefines
    {
        public static EntityTypes ToEntityTypes(EntityTypeFlags value)
        {
            switch (value)
            {
                case EntityTypeFlags.None:
                    return EntityTypes.None;
                case EntityTypeFlags.Player:
                    return EntityTypes.Player;
                case EntityTypeFlags.Monster:
                    return EntityTypes.Monster;
                case EntityTypeFlags.Object:
                    return EntityTypes.Object;
                default:
                    return default(EntityTypes);
            }
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2>(this ILogger logger, string fmt, T1 t1, T2 t2, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            logger.LogInformation(fmt, t1,t2);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            logger.LogInformation(fmt, t1,t2,t3);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            logger.LogInformation(fmt, t1,t2,t3,t4);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            var r16 = t16;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            var r16 = t16;
            var r17 = t17;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            var r16 = t16;
            var r17 = t17;
            var r18 = t18;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            var r16 = t16;
            var r17 = t17;
            var r18 = t18;
            var r19 = t19;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19);
        }
    }
}

namespace LibGame
{
    public static partial class LoggerEx
    {
        public static void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this ILogger logger, string fmt, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, T19 t19, T20 t20, SepP sep = null !, [CallerFilePath] string cFile = "", [CallerLineNumber] int cLine = 0)
        {
            var r1 = t1;
            var r2 = t2;
            var r3 = t3;
            var r4 = t4;
            var r5 = t5;
            var r6 = t6;
            var r7 = t7;
            var r8 = t8;
            var r9 = t9;
            var r10 = t10;
            var r11 = t11;
            var r12 = t12;
            var r13 = t13;
            var r14 = t14;
            var r15 = t15;
            var r16 = t16;
            var r17 = t17;
            var r18 = t18;
            var r19 = t19;
            var r20 = t20;
            logger.LogInformation(fmt, t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19,t20);
        }
    }
}

namespace LibGame.Message
{
    public static partial class ErrorCodeNames
    {
        public readonly static string Common_NotExecuted = ErrorCodes.Common_NotExecuted.ToString();
        public readonly static string Common_InvalidErrorCodes = ErrorCodes.Common_InvalidErrorCodes.ToString();
        public readonly static string Common_ServerInternalError = ErrorCodes.Common_ServerInternalError.ToString();
        public readonly static string Common_ServerInternalException = ErrorCodes.Common_ServerInternalException.ToString();
        public readonly static string Auth_Login_InvalidGameVersion = ErrorCodes.Auth_Login_InvalidGameVersion.ToString();
        public readonly static string _CleintErrorMaxValue = ErrorCodes._CleintErrorMaxValue.ToString();
        public readonly static string _ServerErrorMinValue = ErrorCodes._ServerErrorMinValue.ToString();
        public readonly static string Srv_Auth_Login_InvalidGameVersion = ErrorCodes.Srv_Auth_Login_InvalidGameVersion.ToString();
    }
}

namespace SourceGeneratedTestNS
{
    public static class SourceGeneratedTestClass
    {
        public static void Test() 
        {
            Console.WriteLine("Hello from generated code!");
            Console.WriteLine("The following syntax trees existed in the compilation that created this program:");
        }
    }
}
