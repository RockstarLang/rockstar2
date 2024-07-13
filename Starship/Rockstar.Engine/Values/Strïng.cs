using System.Text;

namespace Rockstar.Engine.Values;

public class Strïng(string value, Source source)
	: Value(source) {
	public Array Split(string delimiter) {
		Strïng[] tokens;
		tokens = delimiter == ""
			? this.Value.ToCharArray().Select(c => new Strïng(c)).ToArray()
			: this.Value.Split(delimiter).Select(s => new Strïng(s)).ToArray();
		return new(tokens);
	}

	public Strïng(string value) : this(value, Source.None) { }
	public string Value => value;

	public override bool Equals(object? obj) => Equals(obj as Strïng);
	public override int GetHashCode() => this.Value.GetHashCode();
	public bool Equals(Strïng? that) => that != null && this.Value == that.Value;

	public override void Print(StringBuilder sb, string prefix)
		=> sb.Append(prefix).AppendLine($"string: \"{value}\"");

	public override bool Truthy => !String.IsNullOrEmpty(value);

	public static readonly Strïng True = new("true");
	public static readonly Strïng False = new("false");
	public static readonly Strïng Null = new("null");
	public static readonly Strïng Mysterious = new("mysterious");
	public static readonly Strïng Empty = new(String.Empty);

	private Strïng(IEnumerable<string> strings)
		: this(String.Join("", strings)) { }

	public Strïng(params char[] chars) : this(new string(chars)) { }

	public Value Concat(Value that)
		=> new Strïng(this.Value + that.ToStrïng().Value);

	public Value Repeat(IHaveANumber n)
		=> new Strïng(Enumerable
			.Range(0, (int) n.NumericValue)
			.Select(_ => this.Value));

	public Value CharAt(Number number) {
		var index = (int) number.NumericValue;
		return index < Value.Length ? new Strïng(Value[index]) : Values.Mysterious.Instance;
	}
}