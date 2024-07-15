using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Rockstar.Engine;

namespace Rockstar.Wasm;

public partial class RockstarRunner {
	private static readonly Parser parser = new();
	[JSExport]
	internal static string Run(string source) {
		var program = parser.Parse(source);
		var io = new StringBuilderIO();
		new RockstarEnvironment(io).Execute(program);
		return io.Output;
	}
}