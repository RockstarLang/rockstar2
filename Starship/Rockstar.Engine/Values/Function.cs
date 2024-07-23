using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;

namespace Rockstar.Engine.Values;

public class Function(IEnumerable<Variable> args, Block body)
	: Value {
	public Function(Block body) : this(new List<Variable>(), body) { }

	public IList<Variable> Args => args.ToList();
	public Block Body => body;
	protected override bool Equals(Value other) => false;

	public override int GetHashCode() => args.GetHashCode() ^ body.GetHashCode();

	public override bool Truthy => true;
	public override Strïng ToStrïng()
		=> new($"function({String.Join(", ", args.Select(a => a.Key).ToArray())}");

	public override Booleän Equäls(Value that)
		=> IdenticalTo(that);

	public override Booleän IdenticalTo(Value that)
		=> new(Object.ReferenceEquals(this, that));

	public override Value AtIndex(Value index) => this;

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append($"function(");
		sb.Append(String.Join(", ", args.Select(a => a.Name)));
		sb.AppendLine("):");
		body.Print(sb, prefix + "|" + INDENT);
	}
}

public class Return(Expression expr) : ExpressionStatement(expr) {
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		Expression.Print(sb, prefix + INDENT);
	}
}

public class FunctionCall(Variable function, IEnumerable<Expression>? args = default)
	: Statement {
	public Variable Function { get; } = function;
	public List<Expression> Args { get; } = (args ?? []).ToList();

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"function call: {Function.Name}");
		foreach (var arg in Args) arg.Print(sb, prefix + INDENT);
	}
}