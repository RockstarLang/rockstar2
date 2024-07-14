using System.Text;

namespace Rockstar.Engine.Expressions;

public abstract class Expression {
	public const string INDENT = "  ";

	public virtual void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine(this.GetType().Name.ToLowerInvariant());
}
