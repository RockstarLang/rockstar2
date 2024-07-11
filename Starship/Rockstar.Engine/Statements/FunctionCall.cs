using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class FunctionCall(Variable function, List<Expression> args, Source source)
	: Statement(source) {
	public Variable Function => function;
	public List<Expression> Args => args;
	public override void Print(StringBuilder sb, string prefix = "") {
		sb.Append(prefix).AppendLine($"function call: {function.Name}");
		foreach (var arg in args) {
			arg.Print(sb, prefix + INDENT);
		}
	}
}