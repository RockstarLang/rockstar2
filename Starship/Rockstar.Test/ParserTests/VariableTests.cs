using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test.ParserTests;

public class VariableTests {
	[Theory]
	[InlineData("a variable", "a  variable")]
	[InlineData("My variable", "MY VARIABLE")]
	[InlineData("My variable", "MY VARIable")]
	[InlineData("My variable", "MY         VARIable")]
	[InlineData("My variable", "MY\t\tVARIable")]
	[InlineData("My variable", "MY         VARIable")]
	public void CommonVariablesAreCaseInsensitiveAndIgnoreWhitespace(string name1, string name2) {
		var a = new CommonVariable(name1);
		var b = new CommonVariable(name2);
		a.Key.ShouldBe(b.Key);
	}

	[Theory]
	[InlineData("Alpha", "ALPHA")]
	[InlineData("beta", "Beta")]
	[InlineData("gamma", "GaMmA")]
	[InlineData("deLTA", "DElta")]
	public void SimpleVariablesAreCaseInsensitiveAndIgnoreWhitespace(string name1, string name2) {
		var a = new SimpleVariable(name1);
		var b = new SimpleVariable(name2);
		a.Key.ShouldBe(b.Key);
	}

	[Theory]
	[InlineData("Mister Crowley", "MISTER CROWLEY")]
	[InlineData("Dr Feelgood", "DR Feelgood")]
	[InlineData("Black Betty", "BLaCK BeTTY")]
	public void ProperVariablesAreCaseInsensitiveAndIgnoreWhitespace(string name1, string name2) {
		var a = new ProperVariable(name1);
		var b = new ProperVariable(name2);
		a.Key.ShouldBe(b.Key);
	}

	[Theory]
	[InlineData("Variable Is 5")]
	[InlineData("""
	            variable is 1
	            say variable
	            """)]
	[InlineData("""
	            variable is 1
	            say variable
	            variable iS 3
	            say variable
	            VARIABLE is 4
	            say variable
	            """)]
	[InlineData("""
	            variable is 1
	            say variable
	            variable iS 3
	            say variable
	            VARIABLE IS 4
	            say variable
	            """)]
	[InlineData("""
	            variable is 1
	            say variable
	            Variable is 2
	            say variable
	            variable iS 3
	            say variable
	            VARIABLE IS 4
	            say variable
	            """)]
	[InlineData("Variable is 2")]
	public void LiteralAssignmentsAreCaseInsensitive(string source) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
	}

	[Theory]
	[InlineData("Albert Einstein says he's got a new theory to share")]
	[InlineData("Albert says he's got a new theory to share")]
	[InlineData("my dog says he's got a new theory to share")]
	[InlineData("the cat says he's got a new theory to share")]
	public void ParserParsesPoeticStrings(string source) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
	}

	[Theory]
	[InlineData("Let themes be 10")]
	//(variable name beginning with pronoun 'the')
	//            Let italics be true (variable name beginning with pronoun 'it')
	//            Shout themes (prints: 10)
	//            Shout italics (prints: true)
	//            """)]
	public void ParserParsesVariableStartingWithKeyword(string source) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
	}
}
