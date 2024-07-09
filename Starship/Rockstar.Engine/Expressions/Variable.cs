using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Expressions;


public abstract class Variable(string name, Source source) : Expression(source) {
	public string Name => name;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"variable: {name}");
	}

	private static readonly Regex whitespace = new("\\s+", RegexOptions.Compiled);

	protected string NormalizedName
		=> String.Join("_", whitespace.Split(Name));

	public abstract string Key { get; }
}