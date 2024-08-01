using System.Text;

namespace Rockstar.Engine.Statements;

public class WildcardStatement(string wildcard) : Statement {
	public string Wildcard => wildcard;
	public override StringBuilder Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).Append("break").AppendLine(String.IsNullOrEmpty(wildcard) ? "" : $"'{wildcard}'");

}
public class Break(string wildcard = "") : WildcardStatement(wildcard) { }