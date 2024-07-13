namespace Rockstar.Test.ParserTests;

public class CastingTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("Cast X")]
	[InlineData("Join X")]
	[InlineData("Turn x down")]
	[InlineData("Turn x up")]
	public void ParseCast(string source) {
		var result = Parse(source);
		output.WriteLine(result.ToString());
		result.Statements.Count.ShouldBe(1);
	}
}