using System.Globalization;
using System.Text;

namespace Rockstar.Engine.Expressions;

public abstract class Expression {
	public const string INDENT = "  ";
	public const string NESTER = "| ";

	public virtual void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine(this.GetType().Name.ToLowerInvariant());

	public IEnumerable<Expression> Concat(IEnumerable<Expression> tail)
		=> new List<Expression> { this }.Concat(tail);
}
