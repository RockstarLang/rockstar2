using System;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Rockstar.Engine;

namespace Rockstar.Wasm;

public partial class RockstarRunner {
	private static readonly Parser parser = new();
	private static readonly StringBuilderIO io = new StringBuilderIO();
	private static readonly RockstarEnvironment env = new(io);

	[JSExport]
	public static string Run(string source) {
		var program = parser.Parse(source);
		io.Reset();
		env.Execute(program);
		return io.Output;
	}
}