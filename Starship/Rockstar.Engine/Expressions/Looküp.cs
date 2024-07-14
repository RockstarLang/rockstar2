using System.Runtime.CompilerServices;
using System.Text;
using Rockstar.Engine.Statements;

namespace Rockstar.Engine.Expressions;

public class Looküp(Variable variable, Source source)
	: Expression(source) {
	public Variable Variable => variable;

	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"lookup: {variable.Name} ({variable.GetType().Name})");
}

public class Delist(Variable variable, Source source)
	: Expression(source) {
	public Variable Variable => variable;

	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
	}
}

public class Enlist(Variable variable, Source source)
	: Statement(source) {

	public Variable Variable => variable;
	public List<Expression> Expressions = new();

	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
		foreach (var expr in Expressions) expr.Print(sb, prefix + INDENT);
	}

	public Enlist(Variable variable, Expression expr, Source source)
		: this(variable, source) {
		Expressions.Add(expr);
		
	}

	public Enlist(Variable variable, IEnumerable<Expression> list, Source source)
		: this(variable, source) {
		Expressions.AddRange(list);
	}

}