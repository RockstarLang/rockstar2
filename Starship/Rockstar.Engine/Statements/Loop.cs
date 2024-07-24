using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public abstract class Loop(Expression condition, Block body, bool compareTo)
	: Statement {
	public bool CompareTo => compareTo;
	public Expression Condition => condition;
	public Block Body => body;
	protected abstract string LoopType { get; }
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"{LoopType}:");
		sb.Append(prefix + NESTER).AppendLine("test:");
		condition.Print(sb, prefix + NESTER + INDENT);
		sb.Append(prefix + NESTER).AppendLine("then:");
		return body.Print(sb, prefix + NESTER + INDENT);
	}
}