using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSourceGenerator
{
    public class MySyntaxReceiver : ISyntaxContextReceiver
    {
        public Dictionary<string, List<ClassDeclarationSyntax>> ClassSyntaxes { get; } = new Dictionary<string, List<ClassDeclarationSyntax>>();
        public Dictionary<string, List<EnumDeclarationSyntax>> EnumSyntaxes { get; } = new Dictionary<string, List<EnumDeclarationSyntax>>();
        public Dictionary<string, List<MethodDeclarationSyntax>> MethodSyntaxes { get; } = new Dictionary<string, List<MethodDeclarationSyntax>>();
        public MySyntaxReceiver()
        {
            ClassSyntaxes.Add(MyConstants.AttrName_AddMemberUsingEnum, new List<ClassDeclarationSyntax>());
            EnumSyntaxes.Add(MyConstants.AttrName_EnumToEnum, new List<EnumDeclarationSyntax>());
            EnumSyntaxes.Add(MyConstants.AttrName_JoinEnums, new List<EnumDeclarationSyntax>());
            MethodSyntaxes.Add(MyConstants.AttrName_GenericParamInc, new List<MethodDeclarationSyntax>());
        }

        public IEnumerable<SyntaxNode> LoopAll()
        {
            foreach (var p in ClassSyntaxes)
            {
                foreach (var dec in p.Value)
                    yield return dec;
            }
            foreach (var p in EnumSyntaxes)
            {
                foreach (var dec in p.Value)
                    yield return dec;
            }
            foreach (var p in MethodSyntaxes)
            {
                foreach (var dec in p.Value)
                    yield return dec;
            }
        }

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classSyntax && classSyntax.AttributeLists.Any())
            {
                var classSymbol = context.SemanticModel.GetDeclaredSymbol(classSyntax);
                foreach (var x in classSymbol.GetAttributes())
                {
                    var checkName = x.AttributeClass.ToDisplayString();
                    if (ClassSyntaxes.TryGetValue(checkName, out var targetList))
                    {
                        targetList.Add(classSyntax);
                    }
                }
            }
            else if (context.Node is EnumDeclarationSyntax enumSyntax && enumSyntax.AttributeLists.Any())
            {
                var classSymbol = context.SemanticModel.GetDeclaredSymbol(enumSyntax);
                foreach (var x in classSymbol.GetAttributes())
                {
                    var checkName = x.AttributeClass.ToDisplayString();
                    if (EnumSyntaxes.TryGetValue(checkName, out var targetList))
                    {
                        targetList.Add(enumSyntax);
                    }
                }
            }
            else if (context.Node is MethodDeclarationSyntax methodSyntax && methodSyntax.AttributeLists.Any())
            {
                var classSymbol = context.SemanticModel.GetDeclaredSymbol(methodSyntax);
                foreach (var x in classSymbol.GetAttributes())
                {
                    var checkName = x.AttributeClass.ToDisplayString();
                    if (MethodSyntaxes.TryGetValue(checkName, out var targetList))
                    {
                        targetList.Add(methodSyntax);
                    }
                }
            }
        }

        public IEnumerable<(ClassDeclarationSyntax, INamedTypeSymbol, AttributeData)> LoopClassSyntaxes(GeneratorExecutionContext context, string key)
        {
            foreach (var syntax in ClassSyntaxes[key])
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(syntax.SyntaxTree);
                if (semanticModel.GetDeclaredSymbol(syntax) is INamedTypeSymbol symbol)
                {
                    yield return (syntax, symbol, symbol.GetAttributes().FirstOrDefault(x => key == x.AttributeClass.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)));
                }
            }
        }
        public IEnumerable<(EnumDeclarationSyntax, INamedTypeSymbol, AttributeData)> LoopEnumSyntaxes(GeneratorExecutionContext context, string key)
        {
            foreach (var syntax in EnumSyntaxes[key])
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(syntax.SyntaxTree);
                if (semanticModel.GetDeclaredSymbol(syntax) is INamedTypeSymbol symbol)
                {
                    yield return (syntax, symbol, symbol.GetAttributes().FirstOrDefault(x => key == x.AttributeClass.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)));
                }
            }
        }
        public IEnumerable<(MethodDeclarationSyntax, IMethodSymbol)> LoopMethodSyntaxes(GeneratorExecutionContext context, string key)
        {
            foreach (var syntax in MethodSyntaxes[key])
            {
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(syntax.SyntaxTree);
                yield return (syntax, semanticModel.GetDeclaredSymbol(syntax) as IMethodSymbol);
            }
        }
    }
}
