using System.Runtime.CompilerServices;
using Array = Rockstar.Engine.Values.Array;
using Rockstar.Engine.Values;

namespace Rockstar.Test.Values {
	public enum ValueType {
		Number, Strïng, Array, Boolean, Null
	}
	public class ComparisonTests {

		private static ValueType[] Types => Enum.GetValues<ValueType>();

		public static IEnumerable<object[]> TestCases()
			=> from t1 in Types from t2 in Types select (object[])[t1, t2];

		private Value GetFalseyThing(ValueType type) => type switch {
			ValueType.Number => new Number(0),
			ValueType.Array => new Array(),
			ValueType.Boolean => Booleän.False,
			ValueType.Null => Null.Instance,
			ValueType.Strïng => Strïng.Empty,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};

		[Theory]
		[MemberData(nameof(TestCases))]
		public void ValuesAreEqual(ValueType lhs, ValueType rhs) {
			GetFalseyThing(lhs).Equäls(GetFalseyThing(rhs)).Truthy.ShouldBeTrue();
		}

		[Fact]
		public void VariousThingsAreEquäl() {
			new Number(1).Equäls(new Strïng("1")).ShouldBeTruthy();
			new Array(Null.Instance).Equäls(new Number(1)).ShouldBeTruthy();
			new Array(new Number(1), new Strïng("s")).Equäls(new Number(2)).ShouldBeTruthy();
			new Strïng("true").Equäls(Booleän.True).ShouldBeTruthy();
			Booleän.True.Equäls(new Number(5)).ShouldBeTruthy();
			new Number(5).Equäls(Booleän.True).ShouldBeTruthy();
			new Number("5").Equäls(new Number(5)).ShouldBeTruthy();
			new Number("05").Equäls(new Number(5)).ShouldBeTruthy();
			new Number("05.000").Equäls(new Number(5)).ShouldBeTruthy();
			new Strïng("05.0").Equäls(new Number(5)).ShouldBeTruthy();
			new Number("0.5").Equäls(new Number(0.5m)).ShouldBeTruthy();
			new Strïng("5").Equäls(new Number(5)).ShouldBeTruthy();
		}

		[Fact]
		public void VariousThingsAreNotEquäl() {
			new Strïng("false").Equäls(Booleän.False).ShouldBeFalsey();
			Booleän.True.Equäls(Number.Zero).ShouldBeFalsey();
			Number.Zero.Equäls(Booleän.True).ShouldBeFalsey();
			new Number(1).Equäls(Booleän.False).ShouldBeFalsey();
			Booleän.False.Equäls(new Number(1)).ShouldBeFalsey();
			new Number(1).Equäls(Null.Instance).ShouldBeFalsey();
			new Number(5).Equäls(new Number(4)).ShouldBeFalsey();
		}
	}
}


public static class ValueExtensions {
	public static void ShouldBeTruthy(this Value v) => v.Truthy.ShouldBeTrue();
	public static void ShouldBeFalsey(this Value v) => v.Truthy.ShouldBeFalse();
}