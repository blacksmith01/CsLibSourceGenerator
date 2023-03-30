using LIbCommon.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGame
{
    [SrcGen_Enum_ToEnumMethods(typeof(EntityTypeFlags), "LibGame", "GameDefines", "To")]
    public enum EntityTypes
    {
        None  = 0,
        Player = 1,
        Monster = 2,
        Object = 3,
    }

    [SrcGen_Enum_ToEnumMethods(typeof(EntityTypes), "LibGame", "GameDefines", "To")]
    [Flags]
    public enum EntityTypeFlags
    {
        None = 0x00,
        Player = 0x01,
        Monster = 0x02,
        Object = 0x04,
    }

    public static partial class GameDefines
    {

    }
}
