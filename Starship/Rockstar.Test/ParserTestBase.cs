using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

namespace Rockstar.Test;

public class ParserTestBase(ITestOutputHelper output) {
	protected Block Parse(string source) {
		var parser = new Parser(); //  { Tracer = DiagnosticsTracer.Instance };
		var result = parser.Parse(source);
		output.WriteLine(result.ToString());
		return result;
	}
}