namespace Rockstar.Test.ParserTests;

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
	            Say "begin"
	            X is 10
	            While X is greater than nothing
	            Y is 0
	            While Y is less than 3
	            Build Y up
	            Say Y
	            
	            Knock X down
	            Say X
	            
	            Say "end"
	            

	            """)]
	public void ParserParsesNestedLoop(string source) {
		var parsed = Parse(source);
		output.WriteLine(parsed.ToString());
		parsed.Statements.Count.ShouldBe(4);
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
		var result = e.Exec(parsed);
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
		var result = e.Exec(parsed);
		e.Output.ReplaceLineEndings().ShouldBe("0\n1\n2\n3\n4\nfinished\n".ReplaceLineEndings());
	}

}