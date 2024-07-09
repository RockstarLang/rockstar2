using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Conditional(Expression condition, Block consequent, Source source)
	: Statement(source) {
	public Expression Condition => condition;
	public Block Consequent => consequent;
	public Block Alternate { get; private set; } = Block.Empty;

	public Conditional Else(Block alternate) {
		this.Alternate = alternate;
		return this;
	}
	public override void Print(StringBuilder sb, int depth = 0) {
		sb.Indent(depth).AppendLine("if:");
		sb.Indent(depth + 1).AppendLine("test:");
		condition.Print(sb, depth + 2);
		sb.Indent(depth + 1).AppendLine("then:");
		consequent.Print(sb, depth + 2);
		if (Alternate.IsEmpty) return;
		sb.Indent(depth + 1).AppendLine("else:");
		Alternate.Print(sb, depth + 2);
	}
}
