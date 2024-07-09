using Pegasus.Common.Tracing;

namespace Rockstar.Test;

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