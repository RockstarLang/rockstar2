using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public abstract class RockstarEnvironment {
	public abstract string? ReadInput();
	public abstract void WriteLine(string? output);
	public abstract void Write(string s);

	private readonly Dictionary<string, Value> variables = new();
	public void SetVariable(string name, Value value) => variables[name.ToLowerInvariant()] = value;
	public Value GetVariable(string name) => variables[name.ToLowerInvariant()];
}