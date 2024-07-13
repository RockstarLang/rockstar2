using System.Text;

namespace Rockstar.Engine.Values;

public class Array(Source source)
	: Value(source) {
	private readonly Dictionary<Value, Value> entries = new();

	public Array(Value[] args) : this(Source.None) {
		for (var i = 0; i < args.Length; i++) entries.Add(new Number(i), args[i]);
	}

	public override bool Truthy => entries.Count > 0;

	public Value Length
		=> new Number(1 + this.entries.Keys.Where(k => k is Number)
			.Select(k => (int) ((Number) k).NumericValue)
			.Max());

	public Value Set(Value index, Value value)
		=> entries[index] = value;

	public Value Get(Value index)
		=> entries.TryGetValue(index, out var value) ? value : Mysterious.Instance;

	public Strïng Join(string joiner) {
		return new Strïng(String.Join(joiner, this.entries.Where(e => e.Key is Number)
			.OrderBy(k => ((Number) k.Key).NumericValue)
			.Select(k => k.Value.ToString()).ToArray()));
	}
}
