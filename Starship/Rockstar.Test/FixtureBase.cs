namespace Rockstar.Test;

public abstract class FixtureBase(ITestOutputHelper testOutput) {

	private static string[] excludes = []; // ["arrays", "conditionals", "control-flow", "examples"];
	private static IEnumerable<string> ListRockFiles() =>
		Directory.GetFiles("fixtures", "*.rock", SearchOption.AllDirectories)
			.Where(f => !excludes.Any(f.Contains));

	public static IEnumerable<object[]> GetFiles()
		=> ListRockFiles().Select(filePath => new[] { filePath });

	//public static IEnumerable<object[]> GetFilesWithExpectations()
	//	=> ListRockFiles()
	//		.Where(filePath => !String.IsNullOrWhiteSpace(ExtractExpects(filePath)))
	//		.Select(filePath => new[] { filePath });

	public static string ExtractExpects(string filePathOrSourceCode) {
		if (File.Exists(filePathOrSourceCode + ".out")) {
			return File.ReadAllText(filePathOrSourceCode + ".out", Encoding.UTF8).ReplaceLineEndings();
		}
		var source = (File.Exists(filePathOrSourceCode)
			? File.ReadAllText(filePathOrSourceCode, Encoding.UTF8)
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
					if (!expected.EndsWith(Environment.NewLine)) expected += Environment.NewLine;
					output.Add(expected);
					i = j;
					break;
			}
		}
		return String.Join("", output).ReplaceLineEndings();
	}
}