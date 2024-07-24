using System.Text;

namespace Rockstar.Engine.Expressions;

public class Delist(Variable variable) : Expression {
	public Variable Variable => variable;
	public override StringBuilder Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		return variable.Print(sb, prefix + INDENT);
	}
}