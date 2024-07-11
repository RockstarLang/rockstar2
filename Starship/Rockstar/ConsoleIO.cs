using Rockstar.Engine;

namespace Rockstar;

public class ConsoleIO : RockstarIO {
	public string? Read() => Console.ReadLine();
	public void Write(string? s) => Console.Write(s);
}
