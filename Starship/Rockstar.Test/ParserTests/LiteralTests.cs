using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

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
		var assign = Parse(source).Statements[0] as Assign;
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
		Parse(source);
	}
}