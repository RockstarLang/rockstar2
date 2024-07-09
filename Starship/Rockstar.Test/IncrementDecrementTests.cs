namespace Rockstar.Test;

public class IncrementDecrementTests(ITestOutputHelper output) {
	[Theory]
	[InlineData("build X up")]
	[InlineData("build X up up")]
	[InlineData("build X up, up")]
	[InlineData("knock X down")]
	[InlineData("knock X down, down, down")]
	[InlineData("knock X down down down")]
	public void ParserParsesIncrementsAndDecrements(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
	}
}