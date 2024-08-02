using Rockstar.Engine;
namespace Rockstar;

public static class Program {
	const string VERSION = "0.0.0-prerelease";
	private static readonly RockstarEnvironment env = new(new ConsoleIO());
	private static readonly Parser parser = new();
	public static void Main(string[] args) {
		switch (args.Length) {
			case > 1:
				Console.WriteLine("Usage: rockstar <program.rock>");
				Environment.Exit(64);
				break;
			case 1:
				if (args[0] == "--version") {
					Console.WriteLine(VERSION);
					Environment.Exit(0);
				}
				RunFile(args[0]);
				break;
			default:
				Console.WriteLine($"Rockstar {VERSION}. Type 'exit' to exit.");
				RunPrompt();
				break;
		}
	}
	

	private static void RunFile(string path) => Run(File.ReadAllText(path));

	private static void RunPrompt() {
		while (true) {
			env.Write("» ");
			var line = env.ReadInput();
			if (line == null) break;
			Run(line);
		}
	}

	private static void Run(string source) {
		try {
			var program = parser.Parse(source);
			var result = env.Execute(program);
			if (result.WhatToDo == WhatToDo.Exit) Environment.Exit(0);
			Console.WriteLine("« " + result.Value);
		} catch (FormatException ex) {
			Console.Error.WriteLine(ex);
		}
	}
}