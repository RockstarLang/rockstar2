using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Assign(Variable variable, Expression expression) : ExpressionStatement(expression) {
	public Variable Variable => variable;
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
		expression.Print(sb, prefix + INDENT);
	}
}

public class Continue : Statement { }

public class Break : Statement { }

public class WhileLoop(Expression condition, Block body)
	: Loop(condition, body, true) {
	protected override string LoopType => "while";
}

public class UntilLoop(Expression condition, Block body)
	: Loop(condition, body, false) {
	protected override string LoopType => "until";
}

public abstract class Loop(Expression condition, Block body, bool compareTo)
	: Statement {
	public bool CompareTo => compareTo;
	public Expression Condition => condition;
	public Block Body => body;
	protected abstract string LoopType { get; }
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"{LoopType}:");
		sb.Append(prefix + NESTER).AppendLine("test:");
		condition.Print(sb, prefix + NESTER + INDENT);
		sb.Append(prefix + NESTER).AppendLine("then:");
		body.Print(sb, prefix + NESTER + INDENT);
	}
}