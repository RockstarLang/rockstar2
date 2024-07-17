using System.Text;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Expressions;

public class Binary : Expression {
	private readonly Operator op;
	private readonly Expression lhs;
	private readonly ICollection<Expression> rhs;

	public Binary(Operator op, Expression lhs, Expression rhs) {
		this.op = op;
		this.lhs = lhs;
		this.rhs = new List<Expression> { rhs };
	}

	public Binary(Operator op, Expression lhs, ICollection<Expression> rhs, Source source) {
		this.op = op;
		this.lhs = lhs;
		this.rhs = rhs;
	}

	public Value Resolve(Func<Expression, Value> eval) {
		var v = eval(lhs);
		return op switch {
			Operator.Times => v * rhs.Select(eval),
			Operator.Divide => v / rhs.Select(eval),
			Operator.Plus => v + rhs.Select(eval),
			Operator.Minus => v - rhs.Select(eval),

			//Operator.Equals => v.Equäls(eval(rhs.Single())),
			//Operator.NotEquals => v.NotEquäls(eval(rhs.Single())),
			//Operator.LessThanEqual => v.LessThanEqual(eval(rhs.Single())),
			//Operator.MoreThanEqual => v.MoreThanEqual(eval(rhs.Single())),
			//Operator.LessThan => v.LessThan(eval(rhs.Single())),
			//Operator.MoreThan => v.MoreThan(eval(rhs.Single())),

			//Operator.Nor => new Booleän(v.Falsy && eval(rhs.Single()).Falsy),
			//Operator.And => v.Truthy ? eval(rhs.Single()) : v,
			//Operator.Or => v.Truthy ? v : eval(rhs.Single()),
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"{op}:".ToLowerInvariant());
		lhs.Print(sb, prefix + INDENT);
		foreach (var expr in rhs) expr.Print(sb, prefix + INDENT);
	}

	public override string ToString() {
		var sb = new StringBuilder();
		this.Print(sb, String.Empty);
		return sb.ToString();
	}
}
