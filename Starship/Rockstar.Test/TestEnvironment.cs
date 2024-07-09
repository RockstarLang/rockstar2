namespace Rockstar.Test;

public class TestEnvironment : RockstarEnvironment {

	private readonly StringBuilder outputStringBuilder = new();
	public string Output => outputStringBuilder.ToString();
	public override string? ReadInput() => null;

	public override void WriteLine(string? output)
		=> this.outputStringBuilder.Append(output + Environment.NewLine);

	public override void Write(string s)
		=> this.outputStringBuilder.Append(s);
}