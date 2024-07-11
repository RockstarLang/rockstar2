namespace Rockstar.Test.ParserTests;

public class ParserTestBase(ITestOutputHelper output) {
	protected Block Parse(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
		return result;
	}

	protected string Run(string source) {
		var parsed = Parse(source);
		var e = new TestEnvironment();
		e.Exec(parsed);
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