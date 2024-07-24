using System;
using System.Text;

namespace Rockstar.Engine.Expressions;

public class Lookup(Variable variable) : Expression {
	public Variable Variable => variable;
	public override StringBuilder Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).Append("lookup: ").AppendLine(variable.Name);

	public override string ToString() => $"lookup: {Variable}";
}