using System;
using System.Runtime.InteropServices.JavaScript;
using Rockstar.Engine;
using Rockstar.Wasm;

Console.WriteLine("Hello, Browser!");
public partial class RockstarRunner {
	private static readonly Parser parser = new();
	[JSExport]
	internal static string Run(string source) {
		var environment = new WasmEnvironment();
		var program = parser.Parse(source);
		var interpreter = new Interpreter(environment);
		interpreter.Run(program);
		return environment.Output;
	}
}
