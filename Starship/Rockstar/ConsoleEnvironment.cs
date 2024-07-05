using Rockstar.Engine;

namespace Rockstar;

public class ConsoleEnvironment : RockstarEnvironment {
	public override string? ReadInput() => Console.ReadLine();
	public override void WriteLine(string? output) => Console.WriteLine(output);
	public override void Write(string s) => Console.Write(s);
}