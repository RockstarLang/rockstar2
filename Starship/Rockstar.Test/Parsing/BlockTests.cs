using Rockstar.Engine.Values;

namespace Rockstar.Test.Parsing;

public class AssignmentTests(ITestOutputHelper output) : ParserTestBase(output) {

	[Theory]
	[InlineData("variable")]
	[InlineData("MyVariable")]
	[InlineData("var_1")]
	public void SimpleVariablesMustStartWithALetter(string name) {
		var assign = Parse($"{name} is 1")
			.Blocks.Single().Statements.Single() as Assign;
		assign!.Expression.ShouldBe(new Number(1));
	}

	[Theory]
	[InlineData("5ive")]
	[InlineData("_foo")]
	public void ParserRejectsInvalidVariableNames(string name) {
		Should.Throw<Exception>(() => Parse($"{name} is 1"));
	}

	[Theory]
	[InlineData("sayonara")]
	[InlineData("island")]
	[InlineData("wasteland")]
	public void SimpleVariablesCanStartWithKeywords(string name) {
		Parse($"{name} is 2").Blocks.Single().Statements.Single().ShouldBeAssignableTo<Assign>();
	}

	[Theory]
	[InlineData("My variable")]
	[InlineData("Your variable")]
	[InlineData("The variable")]
	[InlineData("A variable")]
	[InlineData("Our variable")]
	public void ParserParsesCommonVariables(string name) {
		Parse($"{name} is 2").Blocks.Single().Statements.Single().ShouldBeAssignableTo<Assign>();
	}
}

public class BlockTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
	[InlineData("Say 1\n\nSay 2\n\nSay 3\n\n", 1, 1, 1)]
	[InlineData("Say 1\nSay 2\nSay 3", 3)]
	[InlineData("Say 1\n\n\n\nSay 2\nSay 3", 1, 2)]
	[InlineData("\n\n\n\nSay 1\n\n\n\nSay 2\nSay 3\n\n\n\n", 1, 2)]
	[InlineData("  (comment) Say 1; say 2; say 3   (comment)\n   (empty line)  \n say(comment)4", 3, 1)]
	public void ParserParsesBlocks(string source, params int[] counts) {
		var program = Parse(source);
		program.Blocks.Count.ShouldBe(counts.Length);
		program.Blocks.Select(b => b.Statements.Count).ShouldBe(counts);
	}
}