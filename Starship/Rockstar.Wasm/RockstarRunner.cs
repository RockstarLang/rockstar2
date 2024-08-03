using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Rockstar.Engine;

namespace Rockstar.Wasm;

public class WasmIO(Action<string> output) : IRockstarIO {
	public string? Read() => null;
	public void Write(string s) => output(s);
}

public partial class RockstarRunner {
	private static readonly Parser parser = new();

	[JSExport]
	public static Task<string> Run(string source,
		[JSMarshalAs<JSType.Function<JSType.String>>] Action<string> output) {
		return Task.Run(() => {
			var program = parser.Parse(source);
			var e = new WasmIO(output);
			var env = new RockstarEnvironment(e);
			var result = env.Execute(program);
			return result?.Value?.ToString() ?? "";
		});
	}

	[JSExport]
	public static Task<string> Parse(string source) {
		return Task.Run(() => parser.Parse(source).ToString());
	}
}