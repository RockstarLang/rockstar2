namespace Rockstar.Test;

public abstract class FixtureBase(ITestOutputHelper testOutput) {
	public class TestEnvironment : IAmARockstarEnvironment {

		private readonly Dictionary<string, Value> variables = new();
		public void SetVariable(string name, Value value) => variables[name] = value;
		public Value GetVariable(string name) => variables[name];

		private readonly StringBuilder outputStringBuilder = new();
		public string Output => outputStringBuilder.ToString();
		public string? ReadInput() => null;

		public void WriteLine(string output)
			=> this.outputStringBuilder.Append(output + '\n');

		public void Write(string s)
			=> this.outputStringBuilder.Append(s);
	}

	private static string[] ListRockFiles() =>
		Directory.GetFiles("fixtures", "*.rock", SearchOption.AllDirectories);

	public static IEnumerable<object[]> GetFiles()
		=> ListRockFiles().Select(filePath => new[] { filePath });

	public static IEnumerable<object[]> GetFilesWithExpectations()
		=> ListRockFiles()
			.Where(filePath => !String.IsNullOrWhiteSpace(ExtractExpects(filePath)))
			.Select(filePath => new[] { filePath });

	public static string ExtractExpects(string filePathOrSourceCode) {
		if (File.Exists(filePathOrSourceCode + ".out")) {
			return File.ReadAllText(filePathOrSourceCode + ".out");
		}
		var source = (File.Exists(filePathOrSourceCode)
			? File.ReadAllText(filePathOrSourceCode)
			: filePathOrSourceCode);
		var limit = source.Length;
		var output = new List<string>();
		for (var i = 0; i < limit; i++) {
			switch (source.SafeSubstring(i, 9)) {
				case "(expect: ":
				case "(prints: ":
					i += 9;
					var j = i;
					while (j < limit && source[j] != ')') j++;
					var expected = Regex.Unescape(source.Substring(i, j - i));
					if (!expected.EndsWith('\n')) expected += '\n';
					output.Add(expected);
					i = j;
					break;
			}
		}
		return String.Join("", output);
	}
}