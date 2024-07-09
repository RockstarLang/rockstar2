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
	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).AppendLine("if:");
		sb.Append(prefix + INDENT).AppendLine("test:");
		condition.Print(sb, prefix + INDENT + INDENT);
		sb.Append(prefix + INDENT).AppendLine("then:");
		consequent.Print(sb, prefix + INDENT + INDENT);
		if (Alternate.IsEmpty) return;
		sb.Append(prefix + INDENT).AppendLine("else:");
		Alternate.Print(sb, prefix + INDENT + INDENT);
	}
}
