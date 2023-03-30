using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSourceGenerator
{
    [Generator]
    public partial class MySourceGenerator : ISourceGenerator
    {
        StringBuilder _sb = new StringBuilder(4096);

        Dictionary<string, List<string>> _generatedEnums = new Dictionary<string, List<string>>();


        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = context.SyntaxContextReceiver as MySyntaxReceiver;
            if (receiver == null)
            {
                return;
            }

            _sb.Clear();
            var compUnit = SyntaxFactory.CompilationUnit();
            compUnit = AddUsingDirective(context, compUnit, receiver.LoopAll());
            compUnit = Append_Enum_JoinEnums(context, receiver, compUnit);
            compUnit = Append_Enum_ToEnumMethod(context, receiver, compUnit);
            compUnit = Append_Method_GenericParamInc(context, receiver, compUnit);
            compUnit = Append_Class_AddMemberUsingEnum(context, receiver, compUnit);
            AppendLine(compUnit.NormalizeWhitespace().ToFullString());
            //AppendLine("/*");
            //AppendLine("*/");

            AppendSample();
            context.AddSource("GeneratedSources.g", SourceText.From(_sb.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }

        void AppendLine(string text)
        {
            _sb.Append(text);
            _sb.Append("\r\n");
        }
        void AppendLine()
        {
            _sb.Append("\r\n");
        }

        static void SplitClassName(string name, out string nsNams, out string className)
        {
            var idx = name.LastIndexOf('.');
            if (idx == -1)
            {
                nsNams = string.Empty;
                className = name;
            }
            else
            {
                nsNams = name.Substring(0, idx);
                className = name.Substring(idx + 1);
            }
        }

        void AppendSample()
        {
            AppendLine(@"
namespace SourceGeneratedTestNS
{
    public static class SourceGeneratedTestClass
    {
        public static void Test() 
        {
            Console.WriteLine(""Hello from generated code!"");
            Console.WriteLine(""The following syntax trees existed in the compilation that created this program:"");
        }
    }
}");
        }

        CompilationUnitSyntax AddUsingDirective(GeneratorExecutionContext context, CompilationUnitSyntax syntaxUnit, IEnumerable<SyntaxNode> roots)
        {
            List<string> namespaces = new List<string>();
            foreach (var root in roots)
            {
                SemanticModel semanticModel = null;
                try
                {
                    semanticModel = context.Compilation.GetSemanticModel(root.SyntaxTree);
                }
                catch
                {
                    continue;
                }
                if (semanticModel == null)
                {
                }

                foreach (var node in root.DescendantNodes())
                {
                    try
                    {
                        ISymbol symbol = null;
                        if (node is MemberAccessExpressionSyntax maes)
                        {
                            symbol = semanticModel.GetTypeInfo(maes.Expression).Type;
                        }
                        else if (node is ConstructorInitializerSyntax cis)
                        {
                            symbol = semanticModel.GetTypeInfo(cis).Type;
                        }
                        else if (node is SelectOrGroupClauseSyntax sogcs)
                        {
                            symbol = semanticModel.GetTypeInfo(sogcs).Type;
                        }
                        else if (node is AttributeSyntax attrs)
                        {
                            symbol = semanticModel.GetTypeInfo(attrs).Type;
                        }
                        else if( node is ExpressionSyntax expr)
                        {
                            symbol = semanticModel.GetSymbolInfo(expr).Symbol;
                        }

                        if (symbol == null || symbol.ContainingNamespace.IsGlobalNamespace)
                        {
                            continue;
                        }
                        
                        var nsName = symbol.ContainingNamespace.ToString().Trim();
                        if (!namespaces.Contains(nsName))
                        {
                            namespaces.Add(nsName);
                            syntaxUnit = syntaxUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(nsName)));
                            //AppendLine($"// node = {node}, nameSpace = {nsName}, nodet= {node.GetType()}");
                        }
                    }
                    catch {
                    }
                }
            }

            return syntaxUnit;
        }

        EnumMemberDeclarationSyntax NewSyntax_EnumMemberDeclaration(string memberName, int value)
        {
            return SyntaxFactory.EnumMemberDeclaration(default, SyntaxFactory.Identifier(memberName), NewSyntax_EqualsValueClause_Numeric(value));
        }
        EqualsValueClauseSyntax NewSyntax_EqualsValueClause_Numeric(int value)
        {
            return SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value)));
        }
    }

    public static partial class SyntaxNodeEx
    {
        public static SyntaxNode FindFistDescendantNode(this SyntaxNode node, Func<SyntaxNode, bool> func)
        {
            foreach (var d in node.DescendantNodes())
            {
                if (func(d))
                {
                    return d;
                }
            }
            return null;
        }
        public static IEnumerable<SyntaxNode> LoopDescendantNode(this SyntaxNode node, Func<SyntaxNode, bool> func)
        {
            foreach (var d in node.DescendantNodes())
            {
                if (func(d))
                {
                    yield return d;
                }
            }
        }
        public static SyntaxNode FindChild<T>(this SyntaxNode node, string name)
        {
            foreach (var child in node.ChildNodes())
            {
                if (child is T && child.ToString() == name)
                {
                    return child;
                }
                var ret = child.FindChild<T>(name);
                if (ret != null)
                {
                    return ret;
                }
            }
            return null;
        }

        public static IEnumerable<SyntaxNode> ForEachChildWithoutBlock(this SyntaxNode node)
        {
            foreach (var child in node.ChildNodes())
            {
                if (child is BlockSyntax)
                {
                    foreach (var child2 in child.ForEachChildWithoutBlock())
                    {
                        yield return child2;
                    }
                }
                else
                {
                    yield return child;
                }
            }
        }
    }
}
