namespace Rockstar.Test;

public class StringBuilderIO : RockstarIO {
	private readonly StringBuilder sb = new();
	public string? Read() => null;
	public void Write(string? s) => sb.Append(s);
	public string Output => sb.ToString();
}

public class TestEnvironment() : RockstarEnvironment(new StringBuilderIO()) {
	public string Output => ((StringBuilderIO) IO).Output;
}