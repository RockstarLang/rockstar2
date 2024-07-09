namespace Rockstar.Test;

public class ParserTestBase(ITestOutputHelper output) {
	protected void Parse(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
	}
}

public class FunLyricTests(ITestOutputHelper output) : ParserTestBase(output) {

	[Theory]
	[InlineData("It's more than a feeling (More than a feeling)")]
	[InlineData("If it's more than a feeling (More than a feeling)")]
	public void ParserParsesLyric(string source)
		=> Parse(source);
}