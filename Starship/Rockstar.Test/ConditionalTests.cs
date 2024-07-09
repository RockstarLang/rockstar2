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
	[InlineData("if true if true if true say 2")]
	[InlineData("if foo say 1 else if bar say 2 else if baz say 3")]
	public void ParserParsesSimpleConditionals(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
	}
}
