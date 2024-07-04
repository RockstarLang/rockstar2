using System;
using System.Collections.Generic;
using System.Text;
using Rockstar;
using Rockstar.Engine;
using Rockstar.Engine.Values;

public class WasmEnvironment : IAmARockstarEnvironment {

	private StringBuilder output = new StringBuilder();
	public string Output => output.ToString();

	private Dictionary<string, Value> variables = new Dictionary<string, Value>();
	public Value GetVariable(string name)
		=> variables.ContainsKey(name) ? variables[name] : new Null(Source.None);

	public string ReadInput() {
		throw new NotImplementedException();
	}

	public void SetVariable(string name, Value value)
		=> variables[name] = value;

	public void Write(string output)
		=> this.output.Append(output);

	public void WriteLine(string output)
		=> this.output.AppendLine(output);
}
