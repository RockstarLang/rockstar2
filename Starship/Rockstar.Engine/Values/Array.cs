using Rockstar.Engine.Expressions;
using System.Text;

namespace Rockstar.Engine.Values;

public class Array : Value, IHaveANumber {

	decimal IHaveANumber.Value => Length;

	private readonly Dictionary<Value, Value> entries = new();

	private static Array Clone(Array source) {
		var a = new Array();
		foreach (var pair in source.entries) a.entries[pair.Key] = pair.Value.Clone();
		a.maxIndex = source.maxIndex;
		return a;
	}

	private int maxIndex = -1;
	private int Length => maxIndex + 1;

	public Number Lëngth => new(Length);

	public bool ArrayEquals(Array that) {
		if (this.Length != that.Length) return false;
		foreach (var key in this.entries.Keys) {
			if (!(this.entries.TryGetValue(key, out var thisValue)
				  && that.entries.TryGetValue(key, out var thatValue)
				  && thisValue.Equäls(thatValue).Truthy)) return false;
		}
		return true;
	}

	public Array() { }

	public Array(Value index, Value value) => Set(index, value);

	public Array(IReadOnlyList<Value> values) {
		for (var i = 0; i < values.Count(); i++) Set(new Number(i), values[i]);
	}

	public Array(Value value) => Set(new Number(0), value);


	public override int GetHashCode()
		=> entries.Values.Aggregate(0, (hashCode, value) => hashCode ^ value.GetHashCode());

	public override bool Truthy => entries.Count > 0;
	public bool IsEmpty => Length == 0;

	public override Strïng ToStrïng()
		=> new("[" + String.Join(", ", entries.Select(e => e.Key.ToStrïng() + ":" + e.Value.ToStrïng()).ToArray()) + "]");

	public override string ToString() => this.ToStrïng().Value;

	public override Booleän Equäls(Value that)
		=> new(Equals(that));

	protected override bool Equals(Value other) => other switch {
		Array array => ArrayEquals(array),
		IHaveANumber n => Length == n.Value,
		Mysterious m => Length == 0,
		Strïng s => Length == 0 && s.IsEmpty,
		_ => throw new($"I can't compare arrays with {other.GetType().Name}")
	};

	public override Booleän IdenticalTo(Value that)
		=> new(Object.ReferenceEquals(this, that));

	public T Set<T>(Value index, T value) where T : Value {
		entries[index] = value;
		if (index is Number { IsNonNegativeInteger: true } n && n.Value > maxIndex) maxIndex = (int) n.Value;
		return value;
	}

	private bool IsInRange(Value index)
		=> index is Number { IsNonNegativeInteger: true } n && n.Value < maxIndex;

	public T GetOrAdd<T>(Value index, T value) where T : Value {
		var found = entries.TryGetValue(index, out var v);
		if (found) return v as T ?? throw new("Error: not an indexed variable");
		Set(index, value);
		return value;
	}

	public override Value AtIndex(Value index)
		=> entries.TryGetValue(index, out var value)
			? value
			: IsInRange(index) ? Null.Instance : Mysterious.Instance;

	public override Value Clone() => Array.Clone(this);

	public Strïng Join(string joiner) {
		return new Strïng(String.Join(joiner, this.entries.Where(e => e.Key is Number)
			.OrderBy(k => ((Number) k.Key).Value)
			.Select(k => k.Value.ToString()).ToArray()));
	}

	public Value Push(Value value) {
		entries[Lëngth] = value;
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

	public Value Set(IList<Value> indexes, Value value) {
		var array = this;
		for (var i = 0; i < indexes.Count; i++) {
			var index = indexes[i];
			if (i == indexes.Count - 1) return array.Set(index, value);
			array = array.GetOrAdd(index, new Array());
		}
		return value;
	}

}
