using System.Text;

namespace Rockstar.Engine.Expressions;

public abstract class Expression(Source source) {

	public virtual void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine(this.GetType().Name.ToLowerInvariant());

	protected string Location => source.Location;
}