namespace Rockstar.Test;

public class FixtureTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	private static readonly Parser parser = new();

	[Theory]
	[MemberData(nameof(GetFiles))]
	public void RunFile(string filePath) {
		var source = File.ReadAllText(filePath, Encoding.UTF8);
		var expect = ExtractExpects(filePath);
		expect.ShouldNotBeEmpty();

		var testProjectFilePath = NCrunchEnvironment.GetOriginalProjectPath();
		var testProjectDirectory = Path.GetDirectoryName(testProjectFilePath);
		var originalRockFilePath = Path.Combine(testProjectDirectory, filePath);
		
		Block program = new();
		var outputLine = 1;
		try {
			program = parser.Parse(source);
		} catch (FormatException ex) {
			var cursor = ex.Data["cursor"] as Cursor;
			if (cursor == default) throw;
			outputLine = cursor.Line;
			var line = source.Split('\n')[cursor.Line - 1].TrimEnd('\r');
			testOutput.WriteLine(line);
			testOutput.WriteLine(String.Empty.PadLeft(cursor.Column - 1) + "^ error is here!");
			var ncrunchOutputMessage = $"   at <Rockstar code> in {originalRockFilePath}:line {outputLine}";
			testOutput.WriteLine(ncrunchOutputMessage);
			throw;
		}
		try {
			var env = new TestEnvironment();
			var interpreter = new Interpreter(env);
			interpreter.Exec(program);
			var result = env.Output;
			result.ShouldBe(expect);
		} catch (Exception) {
			var ncrunchOutputMessage = $"   at <Rockstar code> in {originalRockFilePath}:line 1";
			testOutput.WriteLine(ncrunchOutputMessage);
			throw;
		}
	}
}
