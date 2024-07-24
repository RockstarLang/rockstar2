using System.Text;

namespace Rockstar.Engine.Expressions;

public class Delist(Variable variable) : Expression {
	public Variable Variable => variable;
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
	}
}