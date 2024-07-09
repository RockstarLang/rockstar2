using System.Text;

namespace Rockstar.Engine.Expressions;

public class Pronoun(string name, Source source) : Variable(name, source) {
	public Pronoun() : this(String.Empty) { }
	public Pronoun(string name) : this(name, Source.None) { }
	public override string Key => Name.ToLowerInvariant();
	public override void Print(StringBuilder sb, string prefix) {
		sb.Append(prefix).AppendLine($"pronoun: {Name}");
	}
}