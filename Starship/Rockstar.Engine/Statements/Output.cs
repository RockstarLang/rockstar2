using System.Text;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Statements;

public class Output(Expression expr, string suffix = "") : ExpressionStatement(expr) {
	public string Suffix { get; } = suffix;

	public override StringBuilder Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append("output: ");
		return Expression switch {
			Lookup lookup => sb.AppendLine(lookup.ToString()),
			Value value => sb.AppendLine(value.ToString()),
			_ => Expression.Print(sb, prefix + INDENT)
		};
	}
}