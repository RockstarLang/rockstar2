using System.Text;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Engine.Statements;

public abstract class Loop(Expression condition, bool compareTo, Block body, Source source)
	: Statement(source) {
	public bool CompareTo => compareTo;
	public Expression Condition => condition;
	public Block Body => body;
	protected abstract string LoopType { get; }
	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).AppendLine($"{LoopType}:");
		sb.Append(prefix + " \u2502 " ).AppendLine("test:");
		condition.Print(sb, prefix + " \u2502 " + INDENT);
		sb.Append(prefix + " \u2502 ").AppendLine("then:");
		body.Print(sb, prefix + " \u2502 " + INDENT);
		sb.AppendLine(prefix + " \u2514".PadRight(40, '\u2500'));
	}
}