using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Expressions;


public abstract class Variable(string name, Source source) : Expression(source) {
	public string Name => name;
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"variable: {name}");
	}

	protected static Regex whitespace = new("\\s+", RegexOptions.Compiled);

	protected string NormalizedName
		=> String.Join("_", whitespace.Split(Name));

	public abstract string Key { get; }
}

public class Pronoun(string name, Source source) : Variable(name, source) {
	public Pronoun() : this(String.Empty) { }
	public Pronoun(string name) : this(name, Source.None) { }
	public override string Key => Name.ToLowerInvariant();
	public override void Print(StringBuilder sb, int depth) {
		sb.Indent(depth).AppendLine($"pronoun: {name}");
	}
}

public class SimpleVariable(string name, Source source) : Variable(name, source) {
	public SimpleVariable(string name) : this(name, Source.None) { }
	public override string Key => Name.ToLowerInvariant();
}

public class ProperVariable(string name, Source source) : Variable(name, source) {
	public ProperVariable(string name) : this(name, Source.None) { }
	public override string Key => NormalizedName.ToUpperInvariant();
}

public class CommonVariable(string name, Source source) : Variable(name, source) {
	public CommonVariable(string name) : this(name, Source.None) { }
	public override string Key => NormalizedName.ToLowerInvariant();
}
