using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public abstract class RockstarEnvironment {
	public abstract string? ReadInput();
	public abstract void WriteLine(string? output);
	public abstract void Write(string s);
	private Variable? pronounTarget;

	private readonly Dictionary<string, Value> variables = new();

	private Variable AssertTarget(Pronoun pronoun)
		=> pronounTarget ?? throw new($"You must assign a variable before using a pronoun ('{pronoun.Name}')");

	public void SetVariable(Variable variable, Value value) {
		pronounTarget = variable;
		variables[variable.Key] = value;
	}

	public void SetVariable(Pronoun pronoun, Value value)
		=> SetVariable(AssertTarget(pronoun), value);

	public Value GetVariable(Pronoun pronoun)
		=> GetVariable(AssertTarget(pronoun));

	public Value GetVariable(Variable variable) =>
		variables.TryGetValue(variable.Key, out var value) ? value : throw new($"Unknown variable '{variable.Name}'");
		
}