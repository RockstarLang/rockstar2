using System.Text;

namespace Rockstar.Engine.Expressions;

public abstract class Expression(Source source) {
	public const string INDENT = "  ";

	public virtual void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine(this.GetType().Name.ToLowerInvariant());

	protected string Location => source.Location;

	public List<Expression> Concat(List<Expression> list)
		=> new List<Expression> { this }.Concat(list).ToList();
}