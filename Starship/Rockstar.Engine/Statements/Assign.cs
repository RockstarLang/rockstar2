using System.Formats.Asn1;
using System.Net;
using System.Text;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expression) : ExpressionStatement(expression) {
	public Variable Variable => variable;
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
		return Expression.Print(sb, prefix + INDENT);
	}
}

public class Listen : Statement {
	public Variable? Variable { get; init; } = default;
	public Listen() {}
	public Listen(Variable variable) => this.Variable = variable;
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("listen:");
		if (Variable != default) sb.Append(" => ").Append(Variable.Name);
		return sb.AppendLine();
	}
}
