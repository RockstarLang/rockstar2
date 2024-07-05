using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public abstract class RockstarEnvironment {
	public abstract string? ReadInput();
	public abstract void WriteLine(string? output);
	public abstract void Write(string s);

	private readonly Dictionary<string, Value> variables = new();
	public void SetVariable(Variable variable, Value value) => variables[variable.Key] = value;

	public Value GetVariable(Variable variable) =>
		variables.TryGetValue(variable.Key, out var value) ? value : throw new Exception($"Unknown variable '{variable.Name}'");
		
}