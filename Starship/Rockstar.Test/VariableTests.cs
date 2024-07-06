using System.Runtime.InteropServices;
using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test;

public class LiteralTests {

	[Theory]
	[InlineData("the sky is crying", 6)]
	[InlineData("Tommy was a lovestruck ladykiller", 100)]
	public void PoeticLiteralAssignsCorrectValue(string source, decimal value) {
		var parser = new Parser() {
			Tracer = DiagnosticsTracer.Instance
		};
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
}
