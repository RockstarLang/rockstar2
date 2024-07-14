namespace Rockstar.Test.ParserTests;

public class ArrayTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParseRock() {
		var source = "rock first with 0, 1, 2";
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(1);
	}

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