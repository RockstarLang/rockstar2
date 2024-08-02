using Rockstar.Engine.Values;

namespace Rockstar.Test.Parsing;

public class AssignmentTests : ParserTestBase {

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
		Should.Throw<Exception>(() => {
			var program = Parse($"{name} is 1");
			return program;
		});
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