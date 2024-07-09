using System.Diagnostics;
using Pegasus.Common.Tracing;
using Rockstar.Engine.Expressions;

namespace Rockstar.Test;

public class PronounTests(ITestOutputHelper output) {

	private void TestPronoun(Variable variable, Value value) {
		var e = new TestEnvironment();
		var pronoun = new Pronoun();
		e.SetVariable(variable, value);
		var result = e.GetVariable(pronoun) as Number;
		result.ShouldBe(value);
	}

	[Fact]
	public void AssigningProperVariableSetsPronoun()
		=> TestPronoun(new ProperVariable("Doctor Feelgood"), new Number(123));

	[Fact]
	public void AssigningSimpleVariableSetsPronoun()
		=> TestPronoun(new SimpleVariable("Doctor Feelgood"), new Number(123));

	[Fact]
	public void AssigningCommonVariableSetsPronoun()
		=> TestPronoun(new CommonVariable("Doctor Feelgood"), new Number(123));

	[Fact]
	public void LookupPronounWithoutAssigningVariableThrowsException() {
		var e = new TestEnvironment();
		Should.Throw<Exception>(() => e.GetVariable(new Pronoun("him")));
	}

	[Fact]
	public void AssignPronounWithoutAssigningVariableThrowsException() {
		var e = new TestEnvironment();
		Should.Throw<Exception>(() => e.SetVariable(new Pronoun("him"), new Number(123)));
	}

	private void AssignPronounAfterAssigningVariableUpdatesVariable(Variable variable, Value value) {
		var e = new TestEnvironment();
		e.SetVariable(variable, new Null());
		e.SetVariable(new Pronoun(), value);
		e.GetVariable(variable).ShouldBe(value);
	}

	[Fact]
	public void AssignPronounAfterAssigningProperVariableUpdatesVariable()
		=> AssignPronounAfterAssigningVariableUpdatesVariable(new ProperVariable("Mr Crowley"), new Strïng("hey"));

	[Fact]
	public void AssignPronounAfterAssigningSimpleVariableUpdatesVariable()
		=> AssignPronounAfterAssigningVariableUpdatesVariable(new SimpleVariable("crowley"), new Strïng("hey"));

	[Fact]
	public void AssignPronounAfterAssigningCommonVariableUpdatesVariable()
		=> AssignPronounAfterAssigningVariableUpdatesVariable(new CommonVariable("my humps"), new Strïng("my lady humps"));

	[Fact]
	public void PronounAssignmentParsesCorrectly() {
		var source = "it is false";
		var result = new Parser().Parse(source);
		var value = ((Assign) result.Statements[0]).Expr as Booleän;
		value.Truthy.ShouldBe(false);
	}

	[Fact]
	public void PronounAssignmentWorks() {
		var source = """
		             my heart is true
		             say my heart
		             it is false
		             
		             say my heart
		             say it
		             """;
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
		result.Statements.Count.ShouldBe(5);
		var e = new TestEnvironment();
		var i = new Interpreter(e);
		i.Run(result);
		e.Output.ShouldBe("""
		                  true
		                  false
		                  false
		                  
		                  """);
	}

	[Fact]
	public void Whatever() {
		var source = "put 1 into Epic Music";
		var parser = new Parser() { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		result.Statements.Count.ShouldBe(1);
		Console.WriteLine(result.ToString());
	}
}
