using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Expressions;

public abstract class Variable(string name) : Expression {
	public string Name => name;

	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"variable: {name}");

	private static readonly Regex whitespace = new("\\s+", RegexOptions.Compiled);

	protected string NormalizedName
		=> String.Join("_", whitespace.Split(Name));

	public abstract string Key { get; }

	public IEnumerable<Variable> Concat(IEnumerable<Variable> tail)
		=> new List<Variable> { this }.Concat(tail);
}