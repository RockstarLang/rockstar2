using Rockstar.Engine;
namespace Rockstar;

public static class Program {
	private static readonly Parser parser = new();
	public static void Main(string[] args) {
		string? rockstarProgramFile = null;
		List<string> programArguments = [];
		foreach (var arg in args) {
			if (rockstarProgramFile == null) {
				if (arg == "--version") DisplayVersionAndExit();
				if (arg.EndsWith(".rock", StringComparison.InvariantCultureIgnoreCase)) rockstarProgramFile = arg;
			} else {
				programArguments.Add(arg);
			}
		}

		if (rockstarProgramFile != null) {
			RunFile(rockstarProgramFile, programArguments.ToArray());
		} else {
			Console.WriteLine($"Rockstar {RockstarEnvironment.VERSION} on {System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier}.");
			Console.WriteLine("Type 'exit' to exit.");
			RunPrompt();
		}
	}

	private static void DisplayVersionAndExit() {
		Console.WriteLine(RockstarEnvironment.VERSION);
		Environment.Exit(0);
	}

	private static void RunFile(string path, string[] args) {
		var env = new RockstarEnvironment(new ConsoleIO(), args);
		Run(File.ReadAllText(path), env);
	}

	private static void RunPrompt() {
		var env = new RockstarEnvironment(new ConsoleIO());
		while (true) {
			env.Write("» ");
			var line = env.ReadInput();
			if (line == null) break;
			Run(line, env);
		}
	}

	private static void Run(string source, RockstarEnvironment env) {
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