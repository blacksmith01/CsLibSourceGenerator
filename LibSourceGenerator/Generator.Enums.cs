using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSourceGenerator
{
    public partial class MySourceGenerator
    {
        CompilationUnitSyntax Append_Enum_JoinEnums(GeneratorExecutionContext context, MySyntaxReceiver receiver, CompilationUnitSyntax compUnit)
        {
            Dictionary<(string, string), SortedDictionary<int, string>> dic = new Dictionary<(string, string), SortedDictionary<int, string>>();

            foreach (var (syntax, symbol, attrData) in receiver.LoopEnumSyntaxes(context, MyConstants.AttrName_JoinEnums))
            {
                if (attrData == null || attrData.ConstructorArguments.Length < 3)
                {
                    throw new Exception($"!Match Attribute ConstructorArguments, {syntax}, {MyConstants.AttrName_JoinEnums}");
                }

                var nsName = attrData.ConstructorArguments[0].Value.ToString();
                var newEnumName = attrData.ConstructorArguments[1].Value.ToString();
                var order = (int)attrData.ConstructorArguments[2].Value;
                if (!dic.TryGetValue((nsName, newEnumName), out var targetDic))
                {
                    targetDic = new SortedDictionary<int, string>();
                    dic.Add((nsName, newEnumName), targetDic);
                }
                if (targetDic.ContainsKey(order))
                {
                    throw new Exception($"Duplicated Enum Order {order}, {MyConstants.AttrName_JoinEnums}");
                }
                targetDic.Add(order, symbol.ToString());
            }
            
            foreach (var x in dic)
            {
                var nsName = x.Key.Item1;
                var enumName = x.Key.Item2;
                
                var enumDeclaration = SyntaxFactory.EnumDeclaration(enumName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                
                List<string> enumNames = new List<string>();
                foreach (var y in x.Value)
                {
                    foreach (var m in context.Compilation.GetTypeByMetadataName(y.Value).GetMembers().OfType<IFieldSymbol>())
                    {
                        try
                        {
                            enumDeclaration = enumDeclaration.AddMembers(NewSyntax_EnumMemberDeclaration(m.Name, int.Parse(m.ConstantValue.ToString())));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"!enumDeclaration.AddMembers, {ex}");
                        }
                        enumNames.Add(m.Name);
                    }
                }
                _generatedEnums.Add($"{nsName}.{enumName}", enumNames);

                var nsSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(nsName));
                compUnit = compUnit.AddMembers(nsSyntax.AddMembers(enumDeclaration));
            }

            return compUnit;
        }

        CompilationUnitSyntax Append_Enum_ToEnumMethod(GeneratorExecutionContext context, MySyntaxReceiver receiver, CompilationUnitSyntax compUnit)
        {
            foreach (var (syntax, symbol, attrData) in receiver.LoopEnumSyntaxes(context, MyConstants.AttrName_EnumToEnum))
            {
                if (attrData == null || attrData.ConstructorArguments.Length < 4)
                {
                    throw new Exception($"!Match Attribute ConstructorArguments, {syntax}, {MyConstants.AttrName_EnumToEnum}");
                }

                var enumNameFrom = symbol.ToString();
                var enumNameTo = attrData.ConstructorArguments[0].Value.ToString();
                var nsName = attrData.ConstructorArguments[1].Value.ToString();
                var className = attrData.ConstructorArguments[2].Value.ToString();
                var methodPrefix = attrData.ConstructorArguments[3].Value.ToString();

                SplitClassName(enumNameFrom, out var enumNameNs, out var enumNameClass);
                SplitClassName(enumNameTo, out var enumFlagNameNs, out var enumFlagNameClass);

                var finalEnumNameFrom = (nsName == enumNameNs) ? enumNameClass : enumNameFrom;
                var finalEnumNameTo = (nsName == enumFlagNameNs) ? enumFlagNameClass : enumNameTo;

                List<string> fields1 = new List<string>();
                foreach (var m in context.Compilation.GetTypeByMetadataName($"{nsName}.{finalEnumNameFrom}").GetMembers().OfType<IFieldSymbol>())
                {
                    fields1.Add(m.Name);
                }
                List<string> fields2 = new List<string>();
                foreach (var m in context.Compilation.GetTypeByMetadataName($"{nsName}.{finalEnumNameTo}").GetMembers().OfType<IFieldSymbol>())
                {
                    if (fields1.Contains(m.Name))
                        fields2.Add(m.Name);
                }

                var sb = new StringBuilder();
                sb.AppendLine($"switch(value) {{");
                foreach (var name in fields2)
                {
                    sb.AppendLine($"case {finalEnumNameFrom}.{name}: return {finalEnumNameTo}.{name};");
                }
                sb.AppendLine($"default: return default({finalEnumNameTo}); }}");

                var methodDec = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(finalEnumNameTo), $"{methodPrefix}{finalEnumNameTo}")
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword))
                        .AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier("value")).WithType(SyntaxFactory.ParseTypeName(finalEnumNameFrom)))
                        .AddBodyStatements(SyntaxFactory.ParseStatement(sb.ToString()));

                var classDec = SyntaxFactory.ClassDeclaration(className)
                        .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                var nsDec = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(nsName));

                compUnit = compUnit.AddMembers(nsDec.AddMembers(classDec.AddMembers(methodDec)));
            }

            return compUnit;
        }
    }
}
