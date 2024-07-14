using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

public class OperatorTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("say 1 + 2 + 3")]
	[InlineData("say 1 plus 2 plus 3")]
	[InlineData("say \"hello\" plus \" \" plus .11")]
	public void AdditionOperatorWorks(string source) {
		Parse(source);
	}

	[Theory]
	[InlineData("say 1 + 2, 3, and 4 + 3, 4, and 5 + 6")]
	public void AdditionOperatorWorksWithReallyComplicatedArguments(string source) {
		Parse(source);
	}

	[Fact]
	public void NotWorks() {
		Parse("say not true");
	}

	[Fact]
	public void PrecedenceParses() {
		var source = """
		             say 1 + 1 * 2 (prints: 3)
		             say 1 * 2 + 1 (prints: 3)
		             say 1 / 2 * 3 + 4 (prints: 5.5)
		             say 5-2*3 (prints: -1)
		             say 2*3 - 5 (prints: 1)

		             """;
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(5);
	}

	[Fact]
	public void AintWorks() {
		var parsed = Parse("say 5 aint 4");
		parsed.Statements.Count.ShouldBe(1);
		var result = Run(parsed);
		result.ShouldBe("true\n".ReplaceLineEndings());
	}

	[Fact]
	public void NotTest() {
		var source = """
		             say not True
		             say not 5
		             say not "hello"
		             say not 0
		             say not mysterious
		             say not null
		             (verify 5 is not 4 isn't the same as 5 is [not 4])
		             say 5 ain't 4
		             say false is not 4
		             say not not not true

		             """;
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(9);
		output.WriteLine(parsed.ToString());
		var result = Run(parsed);

	}
}