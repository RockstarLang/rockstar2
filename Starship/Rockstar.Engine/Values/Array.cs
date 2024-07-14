using System.Text;

namespace Rockstar.Engine.Values;

public class Array(Source source)
	: Value(source) {
	private readonly Dictionary<Value, Value> entries = new();
	private int maxIndex = -1;
	public Number Length => new(maxIndex + 1);

	public bool ArrayEquals(Array that) {
		if (this.Length != that.Length) return false;
		foreach (var key in this.entries.Keys) {
			if (!(this.entries.TryGetValue(key, out var thisValue)
			      && that.entries.TryGetValue(key, out var thatValue)
			      && thisValue.Equäls(thatValue).Truthy)) return false;
		}
		return true;
	}

	public Array(params Value[] args) : this(Source.None) {
		for (var i = 0; i < args.Length; i++) Set(new Number(i), args[i]);
	}

	public override bool Truthy => entries.Count > 0;

	private IEnumerable<int> NumericIndices
		=> this.entries.Keys.Where(k => k is Number)
			.Select(k => (int) ((Number) k).NumericValue);

	public Value Set(Value index, Value value) {
		entries[index] = value;
		if (index is Number { IsPositiveInteger: true } n && n.Value > maxIndex) maxIndex = (int) n.Value;
		return value;
	}

	public Value Get(Value index)
		=> entries.TryGetValue(index, out var value) ? value : Mysterious.Instance;

	public Strïng Join(string joiner) {
		return new Strïng(String.Join(joiner, this.entries.Where(e => e.Key is Number)
			.OrderBy(k => ((Number) k.Key).NumericValue)
			.Select(k => k.Value.ToString()).ToArray()));
	}

	public Value Push(Value value) {
		entries[Length] = value;
		maxIndex++;
		return value;
	}

	public Value Pop() {
		entries.Remove(new Number(0), out var value);
		if (maxIndex >= 0) maxIndex--;
		var keys = this.entries.Keys.ToList();
		foreach (var key in keys) {
			if (key is not Number { IsPositiveInteger: true } n) continue;
			entries.Remove(n, out var temp);
			if (temp != null) entries[new Number(n.NumericValue - 1)] = temp;
		}
		return value ?? Mysterious.Instance;
	}
}