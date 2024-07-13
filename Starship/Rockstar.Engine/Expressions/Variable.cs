using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Expressions;


public abstract class Variable(string name, Source source) : Expression(source) {
	public string Name => name;
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"variable: {name}");
		if (Index == default) return;
		sb.Append(prefix).AppendLine("index:");
		Index.Print(sb, prefix + "  ");
	}

	private static readonly Regex whitespace = new("\\s+", RegexOptions.Compiled);

	protected string NormalizedName
		=> String.Join("_", whitespace.Split(Name));

	public abstract string Key { get; }

	public List<Variable> Concat(List<Variable> list)
		=> new List<Variable> { this }.Concat(list).ToList();

	public Expression? Index { get; private set; }

	public Variable AtIndex(Expression index) {
		this.Index = index;
		return this;
	}
}