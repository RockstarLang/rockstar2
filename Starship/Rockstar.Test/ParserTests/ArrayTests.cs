namespace Rockstar.Test.ParserTests;

public class ArrayTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParseBigChunk() {
		var source = """
		             rock first with 0, 1, 2
		             rock second with 3, 4, 5

		             if first ain't second
		             say "arrays of the same length but different contents are not equal"

		             ArrayCopy takes source
		             rock dest
		             let i be 0
		             let len be source + 0
		             while i is less than len
		             let dest at i be source at i
		             build i up

		             return dest

		             let First Copy be ArrayCopy taking first
		             if First Copy is first
		             say "element-wise-copied arrays are equal"

		             let Second Copy be second
		             if Second Copy is second
		             say "assignment-copied arrays are equal"

		             rock First Nested with first, second
		             rock Second Nested with first, second
		             if First Nested is Second Nested
		             say "nested arrays with the same contents are equal"

		             rock Third Nested with first, second
		             rock Fourth Nested with second, first
		             if Third Nested ain't Fourth Nested
		             say "nested arrays with different contents are not equal"

		             """;
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(14);
	}

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