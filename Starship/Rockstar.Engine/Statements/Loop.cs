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
	public override void Print(StringBuilder sb, int depth = 0) {
		sb.Indent(depth).AppendLine($"{LoopType}:");
		sb.Indent(depth + 1).AppendLine("test:");
		condition.Print(sb, depth + 2);
		sb.Indent(depth + 1).AppendLine("then:");
		body.Print(sb, depth + 2);
	}
}