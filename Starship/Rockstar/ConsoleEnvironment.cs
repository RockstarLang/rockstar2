using Rockstar.Engine;
using Rockstar.Engine.Values;

namespace Rockstar;

public class ConsoleEnvironment : IAmARockstarEnvironment {
	public string? ReadInput() => Console.ReadLine();
	public void WriteLine(string? output) => Console.WriteLine(output);
	public void Write(string output) => Console.Write(output);
	private readonly Dictionary<string, Value> variables = new();
	public void SetVariable(string name, Value value) => variables[name] = value;
	public Value GetVariable(string name)
		=> variables[name] ?? Mysterious.Instance;
}