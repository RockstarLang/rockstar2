using System.Text;

namespace Rockstar.Engine.Expressions;

public class Looküp(Variable variable, Source source)
	: Expression(source) {
	public Variable Variable => variable;

	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"lookup: {variable.Name} ({variable.GetType().Name})");
}
