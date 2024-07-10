using System.Text;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;

namespace Rockstar.Engine.Values;

public class Function(IEnumerable<Variable> args, Block body, Source source)
	: Value(source) {
	public IEnumerable<Variable> Args => args;
	public Block Body => body;
	public override bool Truthy => true;

	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).Append($"function(");
		sb.Append(String.Join(", ", args.Select(a => a.Name)));
		sb.AppendLine(")");
		body.Print(sb, prefix + "|" + Statement.INDENT);
	}

	public Result Apply(RockstarEnvironment env, List<Expression> callArgs) {
		var scope = env.Extend();


	}
}