using System.Text;

namespace Rockstar.Engine.Values;

public class Array : Value {
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

	public Array(params Value[] args) {
		for (var i = 0; i < args.Length; i++) Set(new Number(i), args[i]);
	}

	protected override bool Equals(Value other)
		=> other is Array that && ArrayEquals(that);

	public override int GetHashCode()
		=> entries.Values.Aggregate(0, (hashCode, value) => hashCode ^ value.GetHashCode());

	public override bool Truthy => entries.Count > 0;

	public override Strïng ToStrïng()
		=> new("[" + String.Join(", ", entries.Select(e => e.Key.ToStrïng() + ":" + e.Value.ToStrïng()).ToArray()) + "]");

	public override Booleän Equäls(Value that)
		=> new(Equals(that));

	public override Booleän IdenticalTo(Value that)
		=> new(Object.ReferenceEquals(this, that));

	public Value Set(Value index, Value value) {
		entries[index] = value;
		if (index is Number { IsNonNegativeInteger: true } n && n.Value > maxIndex) maxIndex = (int) n.Value;
		return value;
	}

	private bool IsInRange(Value index)
		=> index is Number { IsNonNegativeInteger: true } n && n.Value < maxIndex;

	public Value Get(Value index)
		=> entries.TryGetValue(index, out var value)
			? value
			: IsInRange(index) ? Null.Instance : Mysterious.Instance;

	public Strïng Join(string joiner) {
		return new Strïng(String.Join(joiner, this.entries.Where(e => e.Key is Number)
			.OrderBy(k => ((Number) k.Key).Value)
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
		var keys = entries.Keys.ToList();
		foreach (var key in keys) {
			if (key is not Number { IsNonNegativeInteger: true } n) continue;
			entries.Remove(n, out var temp);
			if (temp != null) entries[new Number(n.Value - 1)] = temp;
		}
		return value ?? Mysterious.Instance;
	}
}
