namespace Rockstar.Test.ParserTests;

public class MutationTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParserUnderstandsRoll() {
		var source = "Roll the list";
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(1);
	}

	[Fact]
	public void JoinWorks() {
		var source = """
		             Let the array at 0 be "a"
		             Let the array at 1 be "b"
		             Let the array at 2 be "c"
		             Join the array into ResultA
		             Shout ResultA

		             Join the array into ResultB with "-"
		             Shout ResultB

		             Join the array
		             Shout the array

		             Split "abcde" into tokens
		             Join tokens with ";"
		             Shout tokens

		             """;
		var parsed = Parse(source);
	}
}
