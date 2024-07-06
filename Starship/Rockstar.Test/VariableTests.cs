using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test;

public class OperatorTests {
	[Theory]
	[InlineData("say 1 + 2 + 3")]
	[InlineData("say 1 plus 2 plus 3")]
	[InlineData("say \"hello\" plus \" \" plus .11")]
	public void AdditionOperatorWorks(string source) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
	}
}

public class LiteralTests {
	[Theory]
	[InlineData("say 1")]
	[InlineData("say 1.1")]
	[InlineData("say .1")]
	public void ParserParsesNumberLiterals(string source) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		result.Statements.Count.ShouldBe(1);
	}

	[Theory]
	[InlineData("the sky is crying", 6)]
	[InlineData("Tommy was a lovestruck ladykiller", 100)]
	[InlineData("Tommy was a", 1)]
	[InlineData("Tommy was a aa", 12)]
	[InlineData("Tommy was a aa aaa", 123)]
	[InlineData("Tommy was a aa aaa aaaa aaaaa", 12345)]
	[InlineData("Tommy was a. aa aaa aaaa", 1.234)]
	[InlineData("Tommy was a aa. aaa aaaa", 12.34)]
	public void PoeticLiteralAssignsCorrectValue(string source, decimal value) {
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var assign = parser.Parse(source).Statements[0] as Assign;
		((Number) assign.Expr).Value.ShouldBe(value);
	}
}

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
	            Variable Is 2
	            say variable
	            variable iS 3
	            say variable
	            VARIABLE IS 4
	            say variable
	            """)]
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
