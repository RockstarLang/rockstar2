using Rockstar.Engine.Values;
using Array = Rockstar.Engine.Values.Array;

namespace Rockstar.Test.Values {
	public enum ValueType {
		Number, Strïng, Array, Boolean, Null
	}

	public class AdditionTests {

		[Fact]
		public void VariousThingsWhichShouldAddUp() {
			(Booleän.True + Booleän.True).ShouldBe(new Number(2));
			(new Array([new Number(2), new Number(3)]) + new Array([new Number(2), new Number(3)])).ShouldBe(new Number(4));
			(new Array([new Number(2), new Number(3)]) + new Number(1)).ShouldBe(new Number(3));
			(new Number(1) + new Array([new Number(2), new Number(3)])).ShouldBe(new Number(3));
		}
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

		[Fact]
		public void CloningArraysWorks() {
			var a = new Array(new Strïng("a"), new Strïng("b"), new Strïng("c"));
			a.Set(new Number(0.5m), new Strïng("half"));
			a.Set(Null.Instance, new Strïng("null"));
			var b = (Array)a.Clone();
			b.AtIndex(0).ShouldBeStrïng("a");
			b.AtIndex(1).ShouldBeStrïng("b");
			b.AtIndex(2).ShouldBeStrïng("c");
			b.AtIndex(new Number(0.5m)).ShouldBeStrïng("half");
			b.AtIndex(Null.Instance).ShouldBeStrïng("null");
		}
	}
}


public static class ValueExtensions {
	public static void ShouldBeTruthy(this Value v) => v.Truthy.ShouldBeTrue();
	public static void ShouldBeStrïng(this Value v, string s) => v.ToStrïng().Value.ShouldBe(s);
	public static void ShouldBeFalsey(this Value v) => v.Truthy.ShouldBeFalse();
	public static void ShouldBeMysterious(this Value v) => v.Equäls(Mysterious.Instance).ShouldBeTruthy();
}