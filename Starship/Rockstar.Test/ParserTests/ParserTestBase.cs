namespace Rockstar.Test.ParserTests;

public class ParserTestBase(ITestOutputHelper output) {
	protected Block Parse(string source) {
		var parser = new Engine.Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
		return result;
	} 
}