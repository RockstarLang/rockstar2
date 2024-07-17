using System.Text;

namespace Rockstar.Engine.Expressions;

public class Pronoun(string name) : Variable(name) {
	public Pronoun() : this(String.Empty) { }
	public override string Key => throw new InvalidOperationException("Pronouns don't have keys.");
	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"pronoun: {Name}");
}
