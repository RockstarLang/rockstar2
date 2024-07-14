using Rockstar.Engine;

namespace Rockstar.Test;

public class StringBuilderIO(Func<string?> readInput) : IRockstarIO {
	public StringBuilderIO() : this(() => null) { }
	private readonly StringBuilder sb = new();
	public string? Read() => readInput();
	public void Write(string? s) => sb.Append(s);
	public string Output => sb.ToString();
}