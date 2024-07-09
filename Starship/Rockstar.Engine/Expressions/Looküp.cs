using System.Text;

namespace Rockstar.Engine.Expressions;

public class Looküp(Variable variable, Source source)
	: Expression(source) {
	public Variable Variable => variable;

	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine($"lookup: {variable.Name} ({variable.GetType().Name})");
}
