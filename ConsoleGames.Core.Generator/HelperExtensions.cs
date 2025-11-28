

using Microsoft.CodeAnalysis;

namespace ConsoleGames.Core.Generator
{
    internal static class HelperExtensions
    {
        extension(ITypeSymbol typeSymbol)
        {
            public bool IsEnum() => typeSymbol is INamedTypeSymbol namedType && namedType.EnumUnderlyingType != null;
        }
    }
}
