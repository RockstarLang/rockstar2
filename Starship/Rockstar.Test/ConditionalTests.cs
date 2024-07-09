namespace Rockstar.Test;

public class LoopTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("while true say loop")]
	[InlineData("""
	            while true
	            say loop1
	            say loop2
	            say loop3

	            say done
	            """)]
	public void ParserParsesLoop(string source) => Parse(source);

	[Theory]
	[InlineData("""
				X is 10
				While X is greater than nothing
				While Y is less than 3
				Build Y up
				Say Y

				Knock X down

				""")]
	public void ParserParsesNestedLoop(string source) {
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(2);
	}

	[Fact]
	public void WhileLoopWorks() {
		var source = """
		             let i be 0
		             while i is less than 5
		             say i
		             build i up

		             say "finished"
		             """;
		var parsed = Parse(source);
		var e = new TestEnvironment();
		var i = new Interpreter(e);
		var result = i.Exec(parsed);
		e.Output.ReplaceLineEndings().ShouldBe("0\n1\n2\n3\n4\nfinished\n".ReplaceLineEndings());
	}

	[Fact]
	public void UntilLoopWorks() {
		var source = """
		             let i be 0
		             until i is as great as 5
		             say i
		             build i up

		             say "finished"
		             """;
		var parsed = Parse(source);
		var e = new TestEnvironment();
		var i = new Interpreter(e);
		var result = i.Exec(parsed);
		e.Output.ReplaceLineEndings().ShouldBe("0\n1\n2\n3\n4\nfinished\n".ReplaceLineEndings());
	}

}

public class ConditionalTests(ITestOutputHelper output) {
	[Theory]
	[InlineData("if true say 1")]
	[InlineData("""
	            if true 
	            say 1
	            """)]

	[InlineData("""
	            if true
	            say 1
	            else
	            say 2
	            """)]
	[InlineData("""
	            if true say 1 else
	            say 2
	            
	            """)]
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
	            if true
	            say "hello"
	            say my darling
	            else
	            say "goodbye"
	            say my love
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
