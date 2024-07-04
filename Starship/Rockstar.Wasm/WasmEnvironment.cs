using System;
using System.Collections.Generic;
using System.Text;
using Rockstar.Engine;
using Rockstar.Engine.Values;

namespace Rockstar.Wasm;

public class WasmEnvironment : IAmARockstarEnvironment {
	private readonly StringBuilder output = new();
	public string Output => output.ToString();

	private readonly Dictionary<string, Value> variables = new();
	public Value GetVariable(string name)
		=> variables.ContainsKey(name) ? variables[name] : new Null(Source.None);

	public string ReadInput() {
		throw new NotImplementedException();
	}

	public void SetVariable(string name, Value value)
		=> variables[name] = value;

	public void Write(string s) => this.output.Append(s);

	public void WriteLine(string s) => this.output.AppendLine(s);
}