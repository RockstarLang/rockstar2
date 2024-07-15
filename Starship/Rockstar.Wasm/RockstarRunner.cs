using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Rockstar.Engine;

namespace Rockstar.Wasm;

public partial class RockstarRunner {
	private static readonly Parser parser = new();
	[JSExport]
	internal static string Run(string source) {
		var program = parser.Parse(source);
		var io = new WasmIO();
		new RockstarEnvironment(io).Execute(program);
		return io.Output;
	}
}

public class WasmIO : IRockstarIO {
	private readonly StringBuilder sb = new();
	public string? Read() => null;
	public void Write(string? s) => sb.Append(s);
	public string Output => sb.ToString();
}