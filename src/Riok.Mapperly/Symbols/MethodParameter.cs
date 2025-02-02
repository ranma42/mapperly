using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Symbols;

public readonly record struct MethodParameter(int Ordinal, string Name, ITypeSymbol Type)
{
    private static readonly SymbolDisplayFormat _parameterNameFormat =
        new(
            parameterOptions: SymbolDisplayParameterOptions.IncludeName,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
        );

    public MethodParameter(IParameterSymbol symbol)
        : this(symbol.Ordinal, symbol.ToDisplayString(_parameterNameFormat), symbol.Type.UpgradeNullable()) { }

    public MethodArgument WithArgument(ExpressionSyntax? argument) =>
        new(this, argument ?? throw new ArgumentNullException(nameof(argument)));

    public static MethodParameter? Wrap(IParameterSymbol? symbol) => symbol == null ? null : new(symbol);
}
