namespace Rockstar.Test;

public class StringBuilderIO(Func<string?> readInput) : RockstarIO {
	public StringBuilderIO() : this(() => null) { }
	private readonly StringBuilder sb = new();
	public string? Read() => readInput();
	public void Write(string? s) => sb.Append(s);
	public string Output => sb.ToString();
}

public class TestEnvironment(Func<string?> readInput) : RockstarEnvironment(new StringBuilderIO(readInput)) {
	public TestEnvironment() : this(() => null) {
	}
	public string Output => ((StringBuilderIO) IO).Output;
}