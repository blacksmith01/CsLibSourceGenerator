using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibSourceGenerator
{
    public static class MyConstants
    {
        public static string AttrName_AddMemberUsingEnum = "LIbCommon.SourceGenerators.SrcGen_Class_AddMemberUsingEnumAttribute";
        public static string AttrName_EnumToEnum = "LIbCommon.SourceGenerators.SrcGen_Enum_ToEnumMethodsAttribute";
        public static string AttrName_JoinEnums = "LIbCommon.SourceGenerators.SrcGen_Enum_JoinEnumsAttribute";
        public static string AttrName_GenericParamInc = "LIbCommon.SourceGenerators.SrcGen_Method_GenericParamIncAttribute"; 

    }
}
