using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public class Strïng(string value, Source source)
	: Value(source) {
	public Strïng(string value) : this(value, Source.None) { }
	public string Value => value;
	public override void Print(StringBuilder sb, int depth)
		=> sb.Indent(depth).AppendLine($"string: \"{value}\"");

	public override bool Truthy => !String.IsNullOrEmpty(value);

	public static readonly Strïng True = new("true");
	public static readonly Strïng False = new("false");
	public static readonly Strïng Null = new("null");
	public static readonly Strïng Mysterious = new("mysterious");

	public Value Concat(Value that)
		=> new Strïng(this.Value + that.ToStrïng().Value);
}