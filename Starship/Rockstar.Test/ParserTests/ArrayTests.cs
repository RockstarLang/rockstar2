namespace Rockstar.Test.ParserTests;

public class MutationTests(ITestOutputHelper output) : ParserTestBase(output) {
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
public class ArrayTests(ITestOutputHelper testOutput) : ParserTestBase(testOutput) {

	[Fact]
	public void ParseThing() {
		var source = """
		             let x be 2
		             let x be 2
		             shout x
		             """;
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(3);
	}

	[Fact]
	public void ArrayWorks() {
		var source = """
		             let my array at 0 be "a"
		             let my array at 1 be "b"
		             shout my array at 0
		             shout my array at 1
		             shout my array
		             """;
		var parsed = Parse(source);
		var result = Run(parsed);
		result.ShouldBe("a\nb\n2\n".ReplaceLineEndings());
	}
	[Fact]
	public void ParserParsesArrays() {
		var source = """
		             Let my array at 0 be "foo"
		             Let my array at 1 be "bar"
		             Let my array at 2 be "baz"
		             Let my array at "key" be "value"
		             Shout my array at 0
		             Shout my array at 1
		             Shout my array at 2
		             Shout my array at "key"
		             Shout my array

		             """;
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(9);
	}
}