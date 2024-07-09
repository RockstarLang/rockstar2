namespace Rockstar.Test;

public class ConditionalTests(ITestOutputHelper output) {
	[Theory]
	[InlineData("if true say 1")]
	[InlineData("if true say 1 else say 2")]
	[InlineData("""
	            if true
	            say 1

	            say 2

	            say 3

	            say 4
	            """)]
	[InlineData("""
	            if true
	            say "hello"
	            else
	            say "goodbye"
	            (end block)

	            """)]
	[InlineData("""
	            if true say 1 else
	            say 2
	            say 3

	            say 4
	            """)]
	public void ParserParsesSimpleConditionals(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());

	}
}
