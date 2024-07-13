using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Expressions;

public class Binary : Expression {
	private readonly Operator op;
	private readonly Expression lhs;
	private readonly Expression rhs;

	public static Binary Reduce(Expression lhs, IList<OpList> lists, Source source) {
		var binary = lists.First().Reduce(lhs);
		return lists.Skip(1).Aggregate(binary, (current, list) => list.Reduce(current));
	}

	public Binary(Operator op, Expression lhs, Expression rhs, Source source)
		: base(source) {
		this.op = op;
		this.lhs = lhs;
		this.rhs = rhs;
	}

	public Binary(Operator op, Expression lhs, ICollection<Expression> rhs, Source source)
		: base(source) {
		this.op = op;
		this.lhs = lhs;
		this.rhs = rhs.Count > 1
			? new Binary(op, rhs.First(), rhs.Skip(1).ToList(), source)
			: rhs.Single();
	}

	// public Operator Op => op;
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

			Operator.Nor => new Booleän(v.Falsy && eval(rhs).Falsy),
			Operator.And => v.Truthy ? eval(rhs) : v,
			Operator.Or => v.Truthy ? v : eval(rhs),
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"{op}:".ToLowerInvariant());
		lhs.Print(sb, prefix + INDENT);
		rhs.Print(sb, prefix + INDENT);
	}

	public override string ToString() {
		var sb = new StringBuilder();
		this.Print(sb, String.Empty);
		return sb.ToString();
	}
}

public class OpList(Operator op, List<Expression> list) {
	public Operator Operator => op;
	public List<Expression> List => list;

	public Binary Reduce(Expression lhs) {
		var binary = new Binary(op, lhs, list.First(), Source.None);
		return list.Skip(1).Aggregate(binary, (current, tail) => new(op, current, tail, Source.None));
	}
}