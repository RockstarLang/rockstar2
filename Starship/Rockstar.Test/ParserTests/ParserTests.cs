using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

public class ParserTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\t")]
	[InlineData("\n")]
	[InlineData("\n\n\n\n\n\n\n\n")]
	[InlineData("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n")]
	[InlineData("   \n")]
	[InlineData("()   \n")]
	[InlineData("()   \r\n")]
	[InlineData("   ()   \r\n")]
	public void ParserParsesEmptyPrograms(string source) {
		var parser = new Parser();
		var result = parser.Parse(source);
		result.Statements.Count.ShouldBe(0);
	}

	[Theory]
	[InlineData("say 1")]
	[InlineData("\nsay 1")]
	[InlineData("\r\nsay 1")]
	[InlineData("     \r\nsay 1")]
	[InlineData("   say 1\n")]
	[InlineData("()   say 1\n")]
	[InlineData("say () 1  \r\n")]
	[InlineData("   () say 1  \r\n")]
	[InlineData("(start with a comment line)\nsay 1  \r\n")]
	[InlineData("(start with a comment line)\r\n\r\nsay 1  \r\n")]
	[InlineData("""
	            say "pass"
	            """)]
	[InlineData("say \"pass\" (prints: pass)")]
	public void ParserParsesWeirdPrograms(string source) {
		var result = Parse(source);
		Console.WriteLine(result);
		result.Statements.Count.ShouldBe(1);
	}

	[Theory]
	[InlineData("say 1\nsay 2")]
	[InlineData("\nsay 1\r\nsay 2")]
	[InlineData("\r\nsay 1\nsay 2")]
	[InlineData("     \r\nsay 1\nsay 2")]
	[InlineData("   say 1\nsay 2")]
	[InlineData("()   say 1\nsay 2")]
	[InlineData("say () 1  \r\nsay 2")]
	[InlineData("   () say 1  \r\nsay 2")]
	public void ParserParsesWeirdProgramsWithMultipleStatements(string source) {
		var parser = new Parser();
		var result = parser.Parse(source);
		result.Statements.Count.ShouldBe(2);
	}

	[Theory]
	[InlineData("x is true")]
	[InlineData("x is false")]
	//TODO: [InlineData("x is false or 5")]
	[InlineData("say x and y")]
	public void ParserParsesBooleäns(string source) {
		var parser = new Parser();
		var result = parser.Parse(source);
	}

	[Theory]
	[InlineData("Alpha says a", 1)]
	[InlineData("Alpha says a   ", 1)]
	[InlineData("""
	            Alpha says a
	            """, 1)]
	[InlineData("""
	            
	            Alpha says a
	            
	            """, 1)]
	[InlineData("""
	            Alpha says a
	            Beta says b
	            Gamma says hey now, gonna "make" you "groove", &(!"£$\\!
	            Delta says rn
	            """, 4)]
	public void ParserParsesSaysLiterals(string source, int count) {
		var result = Parse(source);
		result.Statements.Count.ShouldBe(count);
	}

	[Theory]
	[InlineData("an variable is 1", 1)]
	[InlineData("""
	            an    variable is 1
	            """, 1)]
	[InlineData("""
	            a     variable is 1
	            a    variable is 2
	            """, 2)]
	public void ParserParsesCommonVariables(string source, int count) {
		var result = Parse(source);
		result.Statements.Count.ShouldBe(count);
	}
}