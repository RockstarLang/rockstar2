using Rockstar.Engine.Values;

namespace Rockstar.Test.Values;

//public class ComparisonTest(ITestOutputHelper output) : FixtureBase(output) {

//	[Theory]
//	[InlineData(2, 1)]
//	[InlineData(1, 2)]
//	[InlineData("\"b\"", "\"a\"")]
//	[InlineData("\"b\"", "\"a\"")]
//	public void MoreThan(object lhs, object rhs) {
//		foreach (var op in (string[]) ["greater", "bigger", "higher", "stronger", "more"]) {
//			var source = $"Print {lhs} is {op} than {rhs}";
//			RunProgram(Parser.Parse(source)).ShouldBe("expected");
//		}
//	}

//	[Theory]
//	[InlineData(2, 1)]
//	[InlineData(1, 2)]
//	[InlineData("\"b\"", "\"a\"")]
//	[InlineData("\"b\"", "\"a\"")]
//	public void LessThan(object lhs, object rhs, string expected) {
//		foreach (var op in (string[]) ["less", "smaller", "lower", "weaker"]) {
//			var source = $"Print {lhs} is {op} than {rhs}";
//			RunProgram(Parser.Parse(source)).ShouldBe(expected);
//		}
//	}

//	[Theory]
//	[InlineData(2, 1)]
//	[InlineData(1, 2)]
//	[InlineData("\"b\"", "\"a\"")]
//	[InlineData("\"b\"", "\"a\"")]
//	public void LessThanOrEqual(object lhs, object rhs, string expected) {
//		foreach (var op in (string[]) ["small", "low", "weak"]) {
//			var source = $"Print {lhs} is as {op} as {rhs}";
//			RunProgram(Parser.Parse(source)).ShouldBe(expected);
//		}
//	}

//	[Theory]
//	[InlineData(2, 1)]
//	[InlineData(1, 2)]
//	[InlineData("\"b\"", "\"a\"")]
//	[InlineData("\"b\"", "\"a\"")]
//	public void MoreThanOrEqual(object lhs, object rhs, string expected) {
//		foreach (var op in (string[]) ["great", "big", "high", "strong"]) {
//			var source = $"Print {lhs} is as {op} as {rhs}";
//			RunProgram(Parser.Parse(source)).ShouldBe(expected);
//		}
//	}
//}

public class NumberTests(ITestOutputHelper output) {
	[Fact]
	public void NumberEqualityWorks() {
		var a = new Number(2);
		var b = new Number(2);
		(a == b).ShouldBe(true);
		a.Equals(b).ShouldBe(true);
	}
}