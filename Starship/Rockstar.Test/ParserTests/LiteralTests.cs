using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test.ParserTests;

public class BinaryTests(ITestOutputHelper output) {
	[Fact]
	public void ComplicatedExpressionListReducesProperly() {
		var list = new OpList(Operator.Plus, [new Number(2), new Number(3), new Number(4), new Number(5)]);
		var binary = list.Reduce(new Number(1));
		output.WriteLine(binary.ToString());
	}

	[Fact]
	public void ComplicatedExpressionListsReduceProperly() {
		var list1 = new OpList(Operator.Plus, [new Number(2), new Number(3), new Number(4), new Number(5)]);
		var list2 = new OpList(Operator.Times, [new Number(5), new Number(6), new Number(7), new Number(8)]);
		var list3 = new OpList(Operator.Plus, [new Number(9), new Number(10), new Number(11), new Number(12)]);

		var binary = Binary.Reduce(new Number(1), new List<OpList> { list1, list2, list3 }, Source.None);
		output.WriteLine(binary.ToString());
	}
}
public class LiteralTests(ITestOutputHelper output) : ParserTestBase(output) {

	[Theory]
	[InlineData("say 1")]
	[InlineData("say 1.1")]
	[InlineData("say .1")]
	public void ParserParsesNumberLiterals(string source) {
		var result = Parse(source);
		result.Statements.Count.ShouldBe(1);
	}

	[Theory]
	[InlineData("the sky is crying", 6)]
	[InlineData("Tommy was a lovestruck ladykiller", 100)]
	[InlineData("Tommy was a", 1)]
	[InlineData("Tommy was a aa", 12)]
	[InlineData("Tommy was a aa aaa", 123)]
	[InlineData("Tommy was a aa aaa aaaa aaaaa", 12345)]
	[InlineData("Tommy was a. aa aaa aaaa", 1.234)]
	[InlineData("Tommy was a aa. aaa aaaa", 12.34)]
	public void PoeticLiteralAssignsCorrectValue(string source, decimal value) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var assign = parser.Parse(source).Statements[0] as Assign;
		((Number) assign.Expr).Value.ShouldBe(value);
	}

	[Theory]
	[InlineData("""
	            variables are 1
	            say variables
	            Variables Are 2
	            say variables
	            """)]
	public void PoeticLiteralWorks(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
	}
}