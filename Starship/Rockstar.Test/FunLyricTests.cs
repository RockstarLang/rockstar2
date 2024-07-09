namespace Rockstar.Test;

public class FunLyricTests(ITestOutputHelper output) : ParserTestBase(output) {

	[Theory]
	[InlineData("It's more than a feeling (More than a feeling)")]
	[InlineData("If it's more than a feeling (More than a feeling) say yeah")]
	public void ParserParsesLyric(string source)
		=> Parse(source);
}