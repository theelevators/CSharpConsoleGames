

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace ConsoleGames.Core.Generator
{
    [Generator]
    public class EnumOptionExtensionGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var provider = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => s is EnumDeclarationSyntax,
                    transform: static (ctx, _) => (EnumDeclarationSyntax)ctx.Node)
                .Where(static m => m is not null);
            var compilation = context.CompilationProvider.Combine(provider.Collect());
            context.RegisterSourceOutput(compilation, Register);

        }

        private void Register(SourceProductionContext context, (Compilation Left, ImmutableArray<EnumDeclarationSyntax> Right) tuple)
        {
            var (compilation, enums) = tuple;
            List<string> implementations = [];

            foreach(var @enum in enums)
            {
                var symbol = compilation
                            .GetSemanticModel(@enum.SyntaxTree)
                            .GetDeclaredSymbol(@enum) as INamedTypeSymbol;
                
                if (symbol is null) continue;
                var extension = $$"""
                        extension({{symbol.ToDisplayString()}} enumValue)
                        {
                            public string AsRendeable(bool useNumberIndicator = true)
                            {
                                var opt = enumValue.ToInt();
                                var name = Enum.GetName(enumValue);

                                if (useNumberIndicator)
                                {
                                    return $"{opt}. {name}";
                                }
                                else
                                {
                                    return $"{name}";
                                }
                            }

                            public void Render(int x, int y, bool underline, bool useNumberIndicator = false)
                            {
                                var renderable = underline ? enumValue.AsRendeable(useNumberIndicator).Underlined() : enumValue.AsRendeable(useNumberIndicator);
                                Console.SetCursorPosition(x, y);
                                Console.Write(renderable);
                            }
                            public {{symbol.ToDisplayString()}} Next()
                            {

                                var values = Enum.GetValues<{{symbol.ToDisplayString()}}>();
                                var index = Array.IndexOf(values, enumValue);
                                var nextIndex = (index + 1) % values.Length;
                                return values[nextIndex];
                            }
                            public {{symbol.ToDisplayString()}} Previous()
                            {
                                var values = Enum.GetValues<{{symbol.ToDisplayString()}}>();
                                var index = Array.IndexOf(values, enumValue);
                                var previousIndex = (index - 1 + values.Length) % values.Length;
                                return values[previousIndex];

                            }
                        }
                        """;
                implementations.Add(extension);
            }

            var code = $$"""
                namespace ConsoleGames.Core.Render.Utils;
                
                    internal static class EnumOptionExtensions
                    {
                        {{string.Join("\n", implementations)}}
                    }
                """;


            context.AddSource($"EnumOptionExtensions.g.cs", code);
        }
    }
}
