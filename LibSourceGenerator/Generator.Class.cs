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
        CompilationUnitSyntax Append_Class_AddMemberUsingEnum(GeneratorExecutionContext context, MySyntaxReceiver receiver, CompilationUnitSyntax compUnit)
        {
            var attrName = MyConstants.AttrName_AddMemberUsingEnum;
            foreach (var orgClassDec in receiver.ClassSyntaxes[attrName])
            {
                var classSemanticModel = context.Compilation.GetSemanticModel(orgClassDec.SyntaxTree);
                var classSymbol = classSemanticModel.GetDeclaredSymbol(orgClassDec) as INamedTypeSymbol;
                if (classSymbol == null)
                {
                    throw new Exception($"!Found Symbol, {orgClassDec}, {attrName}");
                }

                var classAttrData = classSymbol.GetAttributes().FirstOrDefault(x => attrName == x.AttributeClass.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat));
                if (classAttrData == null || classAttrData.ConstructorArguments.Length < 2)
                {
                    throw new Exception($"!Match Attribute ConstructorArguments, {orgClassDec}, {attrName}");
                }

                var enumNameWithNs = classAttrData.ConstructorArguments[0].Value.ToString();
                var nsName = enumNameWithNs;
                var enumName = enumNameWithNs;
                {
                    var splited = enumNameWithNs.Split('.');
                    if (splited.Length < 2)
                    {
                        throw new Exception($"!enumNameWithNs {enumNameWithNs}, {orgClassDec}, {attrName}");
                    }

                    var idx = enumNameWithNs.LastIndexOf('.');
                    if (idx >= 0 && idx < enumNameWithNs.Length)
                    {
                        nsName = enumNameWithNs.Substring(0, idx);
                        enumName = enumNameWithNs.Substring(idx + 1);
                    }
                }
                var baseEnum = classAttrData.ConstructorArguments[1].Value.ToString();
                var baseEnumFull = $"{enumName}.{baseEnum}";

                List<string> addEnumNames = new List<string>();
                if (_generatedEnums.ContainsKey(enumNameWithNs))
                {
                    addEnumNames.AddRange(_generatedEnums[enumNameWithNs]);
                }
                else
                {
                    foreach (var m in context.Compilation.GetTypeByMetadataName(enumNameWithNs).GetMembers().OfType<IFieldSymbol>())
                    {
                        addEnumNames.Add(m.Name);
                    }
                }
                addEnumNames.Remove(baseEnum);

                if (!addEnumNames.Any())
                {
                    throw new Exception($"!Found Enum Data, {orgClassDec}, {attrName}");
                }

                var newClassDec = orgClassDec.WithMembers(default)
                    .WithAttributeLists(orgClassDec.AttributeLists.Remove(orgClassDec.AttributeLists.Where(x => x.Attributes.Where(y => y.Name.ToString() == "SrcGen_Class_AddMemberUsingEnum").Any()).First()));

                foreach (var fieldDec in orgClassDec.DescendantNodes().Where(x => x is FieldDeclarationSyntax).Select(x => x as FieldDeclarationSyntax))
                {
                    if (fieldDec.FindFistDescendantNode(x => x is MemberAccessExpressionSyntax && x.ToString() == baseEnumFull) == null)
                    {
                        continue;
                    }

                    if (fieldDec.Declaration.Variables.Count != 1)
                    {
                        throw new Exception($"!Support Multiple FieldDeclaration, {orgClassDec}, {attrName}");
                    }

                    //AppendLine($"// node = {fieldDec}, node.GetType = {fieldDec.GetType()}");

                    {
                        var varDec = fieldDec.Declaration.Variables[0];
                        if (varDec.Identifier.ToString() != baseEnum)
                        {
                            throw new Exception($"!Field Name should equal enum name, {orgClassDec}, {attrName}");
                        }
                    }

                    foreach (var addEnumName in addEnumNames)
                    {
                        var addFieldDec = fieldDec;
                        var varDec = addFieldDec.Declaration.Variables[0];
                        var newVarDec = varDec.WithIdentifier(SyntaxFactory.Identifier(addEnumName));
                        addFieldDec = addFieldDec.ReplaceNode(varDec, newVarDec);

                        while (true)
                        {
                            var enumNode = addFieldDec.FindFistDescendantNode(x => x.ToString() == baseEnum && x is IdentifierNameSyntax) as IdentifierNameSyntax;
                            if (enumNode == null)
                            {
                                break;
                            }
                            addFieldDec = addFieldDec.ReplaceNode(enumNode, enumNode.WithIdentifier(SyntaxFactory.Identifier(addEnumName)));
                        }
                        newClassDec = newClassDec.AddMembers(addFieldDec);
                    }

                    var newNsSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(nsName)).AddMembers(newClassDec);
                    //AppendLine(newNsSyntax.ToFullString());
                    compUnit = compUnit.AddMembers(newNsSyntax);
                }
            }

            return compUnit;
        }
    }
}
