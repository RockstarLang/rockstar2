namespace Rockstar.Test.ParserTests;

public class CompoundAssignmentTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("let x be with 5")]
	[InlineData("let x be without 5")]
	[InlineData("let x be over 2")]
	[InlineData("let the night be without regret")]
	public void ParserParsesSimpleConditionals(string source)
			=> Parse(source);
}