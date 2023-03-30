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
        public struct GenericParamIncIterContext
        {
            public string PrefixType;
            public string PrefixParams;
            public string PrefixRepeat;
            public int MaxParamCount;

            public int Idx;
            public string OldParamTypeName => PrefixType + Idx.ToString();
            public string OldParamsValueName => PrefixParams + Idx.ToString();
            public string OldRepeatValueName => PrefixRepeat + Idx.ToString();
            public string OldParamsValueAppended;
            public string OldRepeatValueAppended => PrefixRepeat + Idx.ToString();

            public string NewParamTypeName => PrefixType + (Idx + 1).ToString();
            public string NewParamsValueName => PrefixParams + (Idx + 1).ToString();
            public string NewRepeatValueName => PrefixRepeat + (Idx + 1).ToString();
            public string NewParamsValueAppended;
            public string NewRepeatValueAppended => PrefixRepeat + (Idx + 1).ToString();

            public string FirstParamsValueName => PrefixParams + "1";
            public string FirstRepeatValueName => PrefixRepeat + "1";

            public List<string> CheckAddedOldSyntaxes;
            public List<string> CheckAddedNewSyntaxes;
            public void Next()
            {
                OldParamsValueAppended = NewParamsValueAppended;
                Idx++;

                NewParamsValueAppended = $"{OldParamsValueAppended},{NewParamsValueName}";
            }
        }
        CompilationUnitSyntax Append_Method_GenericParamInc(GeneratorExecutionContext context, MySyntaxReceiver receiver, CompilationUnitSyntax compUnit)
        {
            //compUnit = AddUsingDirective(context, compUnit, receiver.LoopMethodSyntaxes(context, Constants.AttrName_GenericParamInc).Select(x => x.Item1 as SyntaxNode).ToArray());

            foreach (var (method, symbol) in receiver.LoopMethodSyntaxes(context, MyConstants.AttrName_GenericParamInc))
            {
                var orgClassSyntax = symbol.ContainingSymbol.DeclaringSyntaxReferences.First().GetSyntax() as ClassDeclarationSyntax;

                var attrList = method.AttributeLists.Where(x => x.Attributes.Where(y => y.Name.ToString() == "SrcGen_Method_GenericParamInc").Any()).FirstOrDefault();
                if (attrList == null)
                {
                    throw new Exception($"!found attribute, {MyConstants.AttrName_GenericParamInc}");
                }
                if (attrList.Attributes.Count != 1 || attrList.Attributes[0].ArgumentList?.Arguments.Count != 3)
                {
                    throw new Exception($"!attribute format, {MyConstants.AttrName_GenericParamInc}");
                }

                var paramValuePrefixParams = attrList.Attributes[0].ArgumentList?.Arguments[0].ToString().Trim('"');
                var paramValuePrefixRepeat = attrList.Attributes[0].ArgumentList?.Arguments[1].ToString().Trim('"');
                var maxParamCount = int.Parse(attrList.Attributes[0].ArgumentList?.Arguments[2].ToString());

                var firstParamsValueName = paramValuePrefixParams + "1";
                var firstRepeatValueName = paramValuePrefixRepeat + "1";
                var prefixType = method.ParameterList.Parameters.Where(p => p.Identifier.ToString() == firstParamsValueName || p.Identifier.ToString() == firstRepeatValueName).FirstOrDefault()?.Type;
                if (prefixType == null)
                {
                    throw new Exception($"!invalid prefix, {MyConstants.AttrName_GenericParamInc}");
                }

                var firstParamTypeName = prefixType.ToString();
                if (firstParamTypeName.Length < 2 || !firstParamTypeName.EndsWith("1"))
                {
                    throw new Exception($"!invalid generic type name, {MyConstants.AttrName_GenericParamInc}");
                }
                var paramTypePrefix = firstParamTypeName.Substring(0, firstParamTypeName.Length - 1);

                var returnType = method.ReturnType;
                var isReturnTypeHasGenericParam = false;
                if (method.ReturnType.GetType() == typeof(TupleTypeSyntax))
                {
                    isReturnTypeHasGenericParam = (method.ReturnType as TupleTypeSyntax).Elements.Where(n => n.ToString() == firstParamTypeName).Any();
                }

                var isConstraintHasGenericParam = method.ConstraintClauses.Any(x => x.Name.ToString() == firstParamTypeName);

                GenericParamIncIterContext iterCtx = new GenericParamIncIterContext
                {
                    PrefixType = paramTypePrefix,
                    PrefixParams = paramValuePrefixParams,
                    PrefixRepeat = paramValuePrefixRepeat,
                    MaxParamCount = maxParamCount,
                    Idx = 0,
                    NewParamsValueAppended = firstParamsValueName,
                    CheckAddedOldSyntaxes = new List<string>(),
                    CheckAddedNewSyntaxes = new List<string>(),
                };


                var lastMethod = method.WithAttributeLists(method.AttributeLists.Remove(method.AttributeLists.Where(x => x.Attributes.Where(y => y.Name.ToString() == "SrcGen_Method_GenericParamInc").Any()).First()));

                for (int iParam = 1; iParam < maxParamCount; iParam++)
                {
                    iterCtx.Next();

                    var newIdentifier = SyntaxFactory.IdentifierName(iterCtx.NewParamTypeName);
                    var newTypeSyntax = SyntaxFactory.IdentifierName(SyntaxFactory.Identifier(iterCtx.NewParamTypeName));

                    if (isReturnTypeHasGenericParam)
                    {
                        var lastRetParamType = (lastMethod.ReturnType as TupleTypeSyntax).Elements.Where(n => n.ToString() == iterCtx.OldParamTypeName).First();
                        lastMethod = lastMethod.WithReturnType(lastMethod.ReturnType.InsertNodesAfter(lastRetParamType, new[] { SyntaxFactory.TupleElement(newTypeSyntax) }));
                    }

                    {
                        lastMethod = lastMethod.WithTypeParameterList(lastMethod.TypeParameterList.AddParameters(SyntaxFactory.TypeParameter(iterCtx.NewParamTypeName)));
                    }

                    {
                        var lastParameter = lastMethod.ParameterList.Parameters.Where(p => p.Identifier.ToString() == iterCtx.OldParamsValueName).First();
                        lastMethod = lastMethod.WithParameterList(lastMethod.ParameterList.InsertNodesAfter(lastParameter, new[] { lastParameter.WithType(newTypeSyntax).NormalizeWhitespace().WithIdentifier(SyntaxFactory.Identifier(iterCtx.NewParamsValueName)) }));
                    }

                    if (isConstraintHasGenericParam)
                    {
                        var lastConstrant = lastMethod.ConstraintClauses.Where(x => x.Name.ToString() == iterCtx.OldParamTypeName).First();
                        lastMethod = lastMethod.AddConstraintClauses(lastConstrant.WithName(newIdentifier));
                    }

                    {
                        while (UpdateMethodBlock(lastMethod, ref iterCtx, out var methodNew))
                        {
                            lastMethod = methodNew;
                        }
                    }

                    {
                        var newClassSyntax = orgClassSyntax.WithMembers(new SyntaxList<MemberDeclarationSyntax>(lastMethod));
                        var newNsSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(symbol.ContainingNamespace.ToString())).AddMembers(newClassSyntax);
                        compUnit = compUnit.AddMembers(newNsSyntax);
                    }
                }
            }

            return compUnit;
        }

        bool UpdateMethodBlock(MethodDeclarationSyntax methodOld, ref GenericParamIncIterContext iterCtx, out MethodDeclarationSyntax methodNew)
        {
            foreach (var statement in methodOld.Body.Statements)
            {
                foreach (var syntaxOld in statement.ForEachChildWithoutBlock())
                {
                    if (syntaxOld is VariableDeclarationSyntax vd)
                    {
                        var rootOld = syntaxOld.FirstAncestorOrSelf<LocalDeclarationStatementSyntax>();
                        if (rootOld != null && !iterCtx.CheckAddedOldSyntaxes.Contains(rootOld.ToString()))
                        {
                            foreach (var vds in vd.Variables)
                            {
                                var vid = vds.Identifier;
                                if ((vid.ToString() == iterCtx.OldParamsValueName && vds.Initializer.Value.ToString() == iterCtx.OldRepeatValueName)
                                    || (vid.ToString() == iterCtx.OldRepeatValueName && vds.Initializer.Value.ToString() == iterCtx.OldParamsValueName))
                                {
                                    // vid = vinit
                                    var isParams = vid.ToString() == iterCtx.OldParamsValueName;
                                    var vds2 = vds.ReplaceToken(vid, SyntaxFactory.Identifier(isParams ? iterCtx.NewParamsValueName : iterCtx.NewRepeatValueName));
                                    var vinit = vds2.Initializer.Value as IdentifierNameSyntax;
                                    vds2 = vds2.ReplaceNode(vinit, SyntaxFactory.IdentifierName(isParams ? iterCtx.NewRepeatValueName : iterCtx.NewParamsValueName));
                                    var rootNew = rootOld.ReplaceNode(vds, vds2);
                                    //AppendLine($"[1] vds = {vds}");
                                    //AppendLine($"[1] vds2 = {vds2}");
                                    //AppendLine($"[1] rootOld = {rootOld}");
                                    //AppendLine($"[1] rootNew = {rootNew}");
                                    iterCtx.CheckAddedOldSyntaxes.Add((rootOld.ToString()));
                                    if (!iterCtx.CheckAddedNewSyntaxes.Contains(rootNew.ToString()))
                                    {
                                        methodNew = methodOld.WithBody(methodOld.Body.InsertNodesAfter(rootOld, new SyntaxNode[] { rootNew }));
                                        iterCtx.CheckAddedNewSyntaxes.Add((rootNew.ToString()));
                                        return true;
                                    }
                                }
                            }
                        }
                        continue;
                    }

                    foreach (var node in syntaxOld.DescendantNodes())
                    {
                        if (node is IdentifierNameSyntax id && (node.ToString() == iterCtx.OldRepeatValueName || node.ToString() == iterCtx.OldParamTypeName))
                        {
                            var rootOld = syntaxOld.FirstAncestorOrSelf<StatementSyntax>(x => x.Parent is BlockSyntax);
                            if (rootOld == null)
                            {
                                throw new Exception($"!Found StatementSyntax where parent is BlockSyntax, {syntaxOld.GetType()}");
                            }
                            if (!iterCtx.CheckAddedOldSyntaxes.Contains(rootOld.ToString()))
                            {
                                var newName = node.ToString() == iterCtx.OldRepeatValueName ? iterCtx.NewRepeatValueName : iterCtx.NewParamTypeName;
                                var rootNew = rootOld.ReplaceNode(node, SyntaxFactory.IdentifierName(newName));
                                //AppendLine($"rootNew, {rootNew.GetType()} => {rootNew}");
                                var OldRepeatValueName = iterCtx.OldRepeatValueName;
                                var OldParamTypeName = iterCtx.OldParamTypeName;
                                var OldParamsValueAppended = iterCtx.OldParamsValueAppended;
                                while (true)
                                {
                                    SyntaxNode node2 = rootNew.FindFistDescendantNode(x => x is IdentifierNameSyntax && (x.ToString() == OldRepeatValueName || x.ToString() == OldParamTypeName));
                                    if (node2 == null)
                                    {
                                        break;
                                    }
                                    newName = node2.ToString() == OldRepeatValueName ? iterCtx.NewRepeatValueName : iterCtx.NewParamTypeName;
                                    rootNew = rootNew.ReplaceNode(node2, SyntaxFactory.IdentifierName(newName));
                                }
                                while (true)
                                {
                                    SyntaxNode node2 = rootNew.FindFistDescendantNode(x => x is IdentifierNameSyntax && x.ToString() == OldParamsValueAppended);
                                    if (node2 == null)
                                    {
                                        break;
                                    }
                                    rootNew = rootNew.ReplaceNode(node2, SyntaxFactory.IdentifierName(iterCtx.NewParamsValueAppended));
                                }

                                iterCtx.CheckAddedOldSyntaxes.Add((rootOld.ToString()));

                                if (!iterCtx.CheckAddedNewSyntaxes.Contains(rootNew.ToString()))
                                {
                                    try
                                    {
                                        methodNew = methodOld.WithBody(methodOld.Body.InsertNodesAfter(rootOld, new SyntaxNode[] { rootNew }));
                                        iterCtx.CheckAddedNewSyntaxes.Add(rootNew.ToString());
                                        //AppendLine($"[2] rootOld, {rootOld}");
                                        //AppendLine($"[2] rootNew, {rootNew}");
                                        return true;
                                    }
                                    catch (Exception ex)
                                    {
                                        //AppendLine($"{expressionOld.GetType()} => {expressionOld}");
                                        throw new Exception($"!InsertNodesAfter, {ex}");
                                    }
                                }
                            }
                        }
                    }

                    foreach (var node in syntaxOld.DescendantNodes())
                    {
                        if (node is IdentifierNameSyntax id && node.ToString() == iterCtx.OldParamsValueAppended)
                        {
                            //AppendLine($"syntaxOld, {syntaxOld.GetType()} = {syntaxOld}");
                            var syntaxNew = syntaxOld.ReplaceNode(node, SyntaxFactory.IdentifierName(iterCtx.NewParamsValueAppended));
                            methodNew = methodOld.WithBody(methodOld.Body.ReplaceNode(statement, statement.ReplaceNode(syntaxOld, syntaxNew)));
                            return true;
                        }
                    }
                }
            }
            methodNew = null;
            return false;
        }


    }
}
