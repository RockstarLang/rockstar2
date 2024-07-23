using Rockstar.Engine.Values;
using Rockstar.Test.Parsing;
using Array = Rockstar.Engine.Values.Array;

namespace Rockstar.Test.Values;

public class ArrayTests(ITestOutputHelper testOutput) : ParserTestBase(testOutput) {

	[Fact]
	public void EmptyArrayHasZeroLength() {
		var a = new Array();
		a.Length.ShouldBe(new(0));
	}

	[Fact]
	public void AddingNonNumericValuesDoesNotIncreaseLength() {
		var a = new Array();
		a.Set(new Strïng("foo"), new Number(0));
		a.Length.ShouldBe(new(0));
	}

	[Fact]
	public void PushingValueIncrementsLength() {
		var a = new Array();
		a.Length.ShouldBe(new(0));
		a.Push(new Number(1));
		a.Length.ShouldBe(new(1));
	}

	[Fact]
	public void PopActuallyShiftsBecauseRockstarIsBroken() {
		var a = new Array();
		a.Push(new Number(1));
		a.Push(new Number(2));
		a.Push(new Number(3));
		a.Pop().ShouldBe(new Number(1));
		a.Pop().ShouldBe(new Number(2));
		a.Pop().ShouldBe(new Number(3));
	}

	[Fact]
	public void PushingValueDecrementsLength() {
		var a = new Array();
		a.Length.ShouldBe(new(0));
		a.Push(new Number(1));
		a.Pop().ShouldBe(new Number(1));
		a.Length.ShouldBe(new(0));
	}

	[Fact]
	public void AssigningNumericIndexSetsLength() {
		var a = new Array();
		a.Set(new Number(9), new Strïng("foo"));
		a.Length.ShouldBe(new(10));
		a.AtIndex(new Number(8)).ShouldBe(Null.Instance);
	}

	[Fact]
	public void AssigningNumericIndexAndThenPoppingDecrementsLength() {
		var a = new Array();
		a.Set(new Number(1), new Strïng("foo"));
		a.Length.ShouldBe(new(2));
		a.Pop().ShouldBe(Mysterious.Instance);
		a.Length.ShouldBe(new(1));
		a.Pop().ShouldBe(new Strïng("foo"));
		a.Length.ShouldBe(new(0));
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
}