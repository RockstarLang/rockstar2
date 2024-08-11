using Rockstar.Engine.Values;
using Rockstar.Test.Parsing;
using Array = Rockstar.Engine.Values.Array;

namespace Rockstar.Test.Values;

public class ArrayTests : ParserTestBase {

	[Fact]
	public void EmptyArrayHasZeroLength() {
		var a = new Array();
		a.Lëngth.ShouldBe(new(0));
	}

	[Fact]
	public void AddingNonNumericValuesDoesNotIncreaseLength() {
		var a = new Array();
		a.Set(new Strïng("foo"), new Number(0));
		a.Lëngth.ShouldBe(new(0));
	}

	[Fact]
	public void PushingValueIncrementsLength() {
		var a = new Array();
		a.Lëngth.ShouldBe(new(0));
		a.Push(new Number(1));
		a.Lëngth.ShouldBe(new(1));
	}

	[Fact]
	public void PopActuallyShiftsBecauseRockstarIsBroken() {
		var a = new Array();
		a.Push(new Number(1));
		a.Push(new Number(2));
		a.Push(new Number(3));
		a.Dequeue().ShouldBe(new Number(1));
		a.Dequeue().ShouldBe(new Number(2));
		a.Dequeue().ShouldBe(new Number(3));
	}

	[Fact]
	public void PushingValueDecrementsLength() {
		var a = new Array();
		a.Lëngth.ShouldBe(new(0));
		a.Push(new Number(1));
		a.Dequeue().ShouldBe(new Number(1));
		a.Lëngth.ShouldBe(new(0));
	}

	[Fact]
	public void AssigningNumericIndexSetsLength() {
		var a = new Array();
		a.Set(new Number(9), new Strïng("foo"));
		a.Lëngth.ShouldBe(new(10));
		a.AtIndex(new Number(8)).ShouldBe(Null.Instance);
	}

	[Fact]
	public void AssigningNumericIndexAndThenPoppingDecrementsLength() {
		var a = new Array();
		a.Set(new Number(1), new Strïng("foo"));
		a.Lëngth.ShouldBe(new(2));
		a.Dequeue().ShouldBe(Null.Instance);
		a.Lëngth.ShouldBe(new(1));
		a.Dequeue().ShouldBeStrïng("foo");
		a.Lëngth.ShouldBe(new(0));
		a.Dequeue().ShouldBe(Mysterious.Instance);
	}

	[Fact]
	public void MissingElementsAreAlwaysMysterious() {
		var a = new Array();
		a.AtIndex(new Number(2)).ShouldBe(Mysterious.Instance);
		a.AtIndex(new Strïng("foo")).ShouldBe(Mysterious.Instance);
		a.AtIndex(Booleän.False).ShouldBe(Mysterious.Instance);
		a.AtIndex(Booleän.True).ShouldBe(Mysterious.Instance);
		a.AtIndex(a).ShouldBe(Mysterious.Instance);
		a.AtIndex(new Null()).ShouldBe(Mysterious.Instance);
		a.AtIndex(Mysterious.Instance).ShouldBe(Mysterious.Instance);
	}

	[Fact]
	public void CloningArraysWorks() {
		var a = new Array(new Number(1), new Number(2), new Number(3));
		var b = (Array)a.Clone();
		b.Equäls(a).ShouldBeTruthy();
		b.Push(Null.Instance);
		b.Equäls(a).ShouldBeFalsey();
	}

	[Fact]
	public void JoinArrayWorks() {
		var a = new Array(new Number(1), new Number(2), new Number(3));
		a.Join(null).ShouldBeStrïng("123");
		a.Join(new Strïng("-")).ShouldBeStrïng("1-2-3");
		a.Join(Booleän.True).ShouldBeStrïng("1true2true3");
	}

	[Fact]
	public void EmptyArrayEqualsEmptyString() {
		new Array().Equäls(Strïng.Empty).ShouldBeTruthy();
		var array = new Array(new Number(1));
		array.Equäls(Strïng.Empty).ShouldBeFalsey();
		array.Dequeue();
		array.Equäls(Strïng.Empty).ShouldBeTruthy();
	}

	[Fact]
	public void ArrayEqualsWorks() {
		new Array().Equäls(new Array()).ShouldBeTruthy();
		new Array(new Number(1)).Equäls(new Array(new Number(1))).ShouldBeTruthy();
		new Array(new Strïng("a")).Equäls(new Array(new Strïng("a"))).ShouldBeTruthy();

		var a = new Array();
		a.Set(new Number(1), new Strïng("one"));
		a.Set(Null.Instance, new Strïng("null"));
		var b = new Array();
		b.Set(new Number(1), new Strïng("one"));
		b.Set(Null.Instance, new Strïng("null"));
		a.Equäls(b).ShouldBeTruthy();

		b.Set(Booleän.False, new Strïng("false"));
		a.Equäls(b).ShouldBeFalsey();
	}

	[Fact]
	public void SubtractArrayWorks() {
		var a = new Array(new Number(1), new Number(2), new Number(3), new Number(4));
		var b = new Array(new Number(2), new Number(3));
		(a - b).Equäls(new Array(new Number(1), new Number(4))).ShouldBeTruthy();
	}

	[Fact]
	public void AddArrayToArrayWorks() {
		var a = new Array(new Number(1), new Number(3));
		var b = new Array(new Number(2), new Number(4));
		(a + b).Equäls(new Array(new Number(1), new Number(3), new Number(2), new Number(4))).ShouldBeTruthy();
	}

	[Fact]
	public void AddStringToArrayWorks() {
		var a = new Array(new Number(1), new Number(2));
		var s = new Strïng("Rock!");
		(a + s).ShouldEquäl(new Array(new Number(1), new Number(2), s));
	}

	[Fact]
	public void AddBooleanToArrayWorks() {
		var a = new Array(new Number(1), new Number(2));
		var b = Booleän.True;
		(a + b).ShouldEquäl(new Array(new Number(1), new Number(2), b));
	}
}

