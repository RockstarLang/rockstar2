using System.Text;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Expressions;

public class Binary(Operator op, Expression lhs, Expression rhs, Source source)
	: Expression(source) {

	public Operator Op => op;
	//public Expression Lhs => lhs;
	//public Expression Rhs => rhs;

	public Value Resolve(Func<Expression, Value> eval) {
		var v = eval(lhs);
		return op switch {
			Operator.Plus => v.Plus(eval(rhs)),
			Operator.Minus => v.Minus(eval(rhs)),
			Operator.Times => v.Times(eval(rhs)),
			Operator.Divide => v.Divide(eval(rhs)),

			Operator.Equals => v.Equäls(eval(rhs)),
			Operator.NotEquals => v.NotEquäls(eval(rhs)),
			Operator.LessThanEqual => v.LessThanEqual(eval(rhs)),
			Operator.MoreThanEqual => v.MoreThanEqual(eval(rhs)),
			Operator.LessThan => v.LessThan(eval(rhs)),
			Operator.MoreThan => v.MoreThan(eval(rhs)),

			Operator.And => v.Truthy ? eval(rhs) : v,
			Operator.Or => v.Truthy ? v : eval(rhs),
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}

	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"{op}:".ToLowerInvariant());
		lhs.Print(sb, depth + 1);
		rhs.Print(sb, depth + 1);
	}
}