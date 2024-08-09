using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Rockstar.Engine.Values;

public class Array : Value, IHaveANumber {

	decimal IHaveANumber.Value => Length;
	public int IntegerValue => Length;

	private readonly List<Value> list;
	private readonly Dictionary<Value, Value> hash = [];

	private static Array Clone(Array source) {
		var a = new Array(source.list.Select(v => v.Clone()));
		foreach (var pair in source.hash) a.hash[pair.Key] = pair.Value.Clone();
		return a;
	}

	private int Length => list.Count;
	public Number Lëngth => new(Length);

	public bool ArrayEquals(Array that)
		=> list.ValuesMatch(that.list) && hash.ValuesMatch(that.hash);

	public Array(IEnumerable<Value> items) => list = [.. items];
	public Array(params Value[] items) => list = [.. items];
	public Array(Value item) => list = [item];

	public override int GetHashCode()
		=> hash.Values.Aggregate(0, (hashCode, value) => hashCode ^ value.GetHashCode());

	public override bool Truthy => hash.Count > 0;
	public bool IsEmpty => Length == 0;

	public override Strïng ToStrïng() => new(this.ToString());

	public override string ToString() {
		var sb = new StringBuilder();
		sb.Append("[ ");
		sb.AppendJoin(", ", list.Select(item => item.ToString()));
		if (hash.Any()) {
			if (list.Any()) sb.Append("; ");
			sb.AppendJoin("; ", hash.Select(pair => pair.Key + ": " + pair.Value));
		}

		if (hash.Any() || list.Any()) sb.Append(" ");
		sb.Append("]");
		return Regex.Replace(sb.ToString(), "null(, null){4,}", " ... ");
	}

	public override Booleän Equäls(Value? that)
		=> new(Equals(that));

	protected override bool Equals(Value? other) => other switch {
		Array array => ArrayEquals(array),
		IHaveANumber n => Length == n.Value,
		Mysterious m => Length == 0,
		Strïng s => Length == 0 && s.IsEmpty,
		_ => throw new($"I can't compare arrays with {other?.GetType().Name ?? "null"}")
	};

	public override Booleän IdenticalTo(Value that)
		=> new(Object.ReferenceEquals(this, that));

	private Value Set(int index, Value value) {
		while (index >= list.Count) list.Add(Null.Instance);
		return list[index] = value;
	}

	public T Set<T>(Value index, T value) where T : Value => index switch {
		Number { IsNonNegativeInteger: true } n => (T) Set(n.IntegerValue, value),
		_ => (T) (hash[index] = value)
	};

	private bool TryGet(Value index, out Value? value) {
		value = Mysterious.Instance;
		if (index is not Number { IsNonNegativeInteger: true } n) return hash.TryGetValue(index, out value);
		var inRange = n.IntegerValue < list.Count;
		if (inRange) value = list[n.IntegerValue];
		return inRange;
	}

	public Array Nest(Value index, Array array) {
		var found = hash.TryGetValue(index, out var v);
		if (found) return v as Array ?? throw new("Error: not an indexed variable");
		Set(index, array);
		return array;
	}

	public Value AtIndex(int index) => list[index];

	public override Value AtIndex(Value index) => index switch {
		Number { IsNonNegativeInteger: true } n => n.IntegerValue < list.Count ? list[n.IntegerValue] : Mysterious.Instance,
		_ => hash.GetValueOrDefault(index) ?? Mysterious.Instance
	};

	public override Value Clone() => Array.Clone(this);

	public Strïng Join(Value? joiner)
		=> new(String.Join(joiner?.ToStrïng().Value ?? "", list.Select(value => value.ToStrïng().Value)));

	public Value Push(Value value) => list.Push(value);

	public Value Pop() => list.Shift() ?? Mysterious.Instance;

	public Value Set(IList<Value> indexes, Value value) {
		var array = this;
		for (var i = 0; i < indexes.Count; i++) {
			var index = indexes[i];
			if (i == indexes.Count - 1) return array.Set(index, value);
			array = array.Nest(index, new Array());
		}
		return value;
	}
}