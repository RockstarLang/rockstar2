using System.Runtime.CompilerServices;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Return(Expression expr, Source source)
	: Statement(source) {
	public Expression Expression => expr;
}

public class Listen(Source source)
	: Statement(source) {
	public Variable? Variable { get; init; } = default;
	public Listen(Variable variable, Source source) : this(source) {
		this.Variable = variable;
	}
}
