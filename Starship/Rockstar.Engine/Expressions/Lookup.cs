using System;
using System.Text;

namespace Rockstar.Engine.Expressions;

public class Lookup(Variable variable) : Expression {
	public Variable Variable => variable;
	public override void Print(StringBuilder sb, string prefix) {
		base.Print(sb, prefix);
		variable.Print(sb, prefix + INDENT);
	}

	public override string ToString() => $"lookup: {Variable.Name}";
}