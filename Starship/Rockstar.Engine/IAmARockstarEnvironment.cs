using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public interface IAmARockstarEnvironment {
	string? ReadInput();
	void WriteLine(string? output);
	void Write(string output);
	void SetVariable(string name, Value value);
	Value GetVariable(string name);
}