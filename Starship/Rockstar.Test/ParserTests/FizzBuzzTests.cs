namespace Rockstar.Test.ParserTests;

public class FizzBuzzTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParserParsesFizzBuzz() {
		var source = """
		             If Modulus taking Counter, Fizz is 0 and Modulus taking Counter, Buzz is 0
		             Say "FizzBuzz!"
		             Continue
		                 (blank line ending 'If' Block)
		             If Modulus taking Counter & Fizz is 0
		             Say "Fizz!"
		             Continue
		                 (blank line ending 'If' Block)
		             If Modulus taking Counter & Buzz is 0
		             Say "Buzz!"
		             Continue
		                 (blank line ending 'If' Block)
		             Say Counter
		                 (EOL ending Until block)

		             """;
		Parse(source).Statements.Count.ShouldBe(4);
	}
}
