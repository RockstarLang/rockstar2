using System.ComponentModel.Design;
using System.Diagnostics;
using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test {

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
	}
	public class ParserTests(ITestOutputHelper helper) {
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		[InlineData("\n")]
		[InlineData("\n\n\n\n\n\n\n\n")]
		[InlineData("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n")]
		[InlineData("   \n")]
		[InlineData("()   \n")]
		[InlineData("()   \r\n")]
		[InlineData("   ()   \r\n")]
		public void ParserParsesEmptyPrograms(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(0);
		}

		[Theory]
		[InlineData("say 1")]
		[InlineData("\nsay 1")]
		[InlineData("\r\nsay 1")]
		[InlineData("     \r\nsay 1")]
		[InlineData("   say 1\n")]
		[InlineData("()   say 1\n")]
		[InlineData("say () 1  \r\n")]
		[InlineData("   () say 1  \r\n")]
		[InlineData("(start with a comment line)\nsay 1  \r\n")]
		[InlineData("(start with a comment line)\r\n\r\nsay 1  \r\n")]
		[InlineData("""
		            say "pass" 
		            """)]
		public void ParserParsesWeirdPrograms(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(1);
		}

		[Theory]
		[InlineData("say 1\nsay 2")]
		[InlineData("\nsay 1\r\nsay 2")]
		[InlineData("\r\nsay 1\nsay 2")]
		[InlineData("     \r\nsay 1\nsay 2")]
		[InlineData("   say 1\nsay 2")]
		[InlineData("()   say 1\nsay 2")]
		[InlineData("say () 1  \r\nsay 2")]
		[InlineData("   () say 1  \r\nsay 2")]
		public void ParserParsesWeirdProgramsWithMultipleStatements(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(2);
		}

		[Theory]
		[InlineData("x is true")]
		[InlineData("x is false")]
		//TODO: [InlineData("x is false or 5")]
		[InlineData("say x and y")]
		public void ParserParsesBooleäns(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
		}

		[Theory]
		[InlineData("Alpha says a", 1)]
		[InlineData("Alpha says a   ", 1)]
		[InlineData("""
		            Alpha says a
		            Beta says b
		            Gamma says hey now, gonna "make" you "groove", &(!"£$\\!
		            Delta says \\r\\n
		            """, 4)]
		public void ParserParsesSaysLiterals(string source, int count) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(count);
		}

		[Theory]
		[InlineData("an variable is 1", 1)]
		[InlineData("""
		            an    variable is 1
		            """, 1)]
		[InlineData("""
		            a     variable is 1
		            a    variable is 2
		            """, 2)]
		public void ParserParsesCommonVariables(string source, int count) {
			var parser = new Parser {
				Tracer = DiagnosticsTracer.Instance
			};
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(count);
		}
	}
}


