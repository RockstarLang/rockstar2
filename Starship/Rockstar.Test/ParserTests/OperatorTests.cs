using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

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