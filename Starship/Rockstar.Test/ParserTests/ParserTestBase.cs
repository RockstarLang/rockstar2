using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

public class ParserTestBase(ITestOutputHelper output) {
	protected Block Parse(string source) {
		var parser = new Parser(); //  { Tracer = new TestOutputTracer(output) };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
		return result;
	}

	protected string Run(string source) => Run(Parse(source));

	protected string Run(Block block) {
		var e = new TestEnvironment();
		e.Exec(block);
		return e.Output;
	}

	protected void ShouldThrow(string source, Action<Exception> assert) {
		try {
			new TestEnvironment().Exec(Parse(source));
			throw new("Exec() should have thrown an exception but didn't");
		} catch (Exception ex) {
			assert(ex);
		}
	}
}