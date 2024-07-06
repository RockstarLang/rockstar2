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
		if (variable is Pronoun pronoun) {
			variables[AssertTarget(pronoun).Key] = value;
		} else {
			pronounTarget = variable;
			variables[variable.Key] = value;
		}
	}

	public Value GetVariable(Variable variable) {
		var key = (variable is Pronoun pronoun ? AssertTarget(pronoun).Key : variable.Key);
		return variables.TryGetValue(key, out var value) ? value : throw new($"Unknown variable '{variable.Name}'");
	}
}