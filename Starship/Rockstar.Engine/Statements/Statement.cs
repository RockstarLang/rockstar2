using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public abstract class Statement(Source source) {
	public const string INDENT = "  ";
	public abstract void Print(StringBuilder sb, string prefix = "");
	protected string Location => source.Location;
}

public class Function(Variable name, IEnumerable<Variable> args, Block body, Source source)
	: Statement(source) {
	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).Append($"function: {name.Name}(");
		sb.Append(String.Join(", ", args.Select(a => a.Name)));
		sb.AppendLine(")");
		body.Print(sb, prefix + "|" + INDENT);
	}
}

public class FunctionCall(Variable name, List<Expression> args, Source source)
	: Statement(source) {
	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).AppendLine($"function call: {name.Name}");
		foreach (var arg in args) {
			arg.Print(sb, prefix + INDENT);
		}
	}
}

