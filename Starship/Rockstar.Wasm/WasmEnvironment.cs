using System;
using System.Text;
using Rockstar.Engine;

namespace Rockstar.Wasm;

public class WasmEnvironment : RockstarEnvironment {
	private readonly StringBuilder output = new();
	public string Output => output.ToString();
	public override string ReadInput() => throw new NotImplementedException();
	public override void Write(string s) => this.output.Append(s);
	public override void WriteLine(string s) => this.output.AppendLine(s);
}