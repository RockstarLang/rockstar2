using Rockstar.Engine;
using Rockstar.Engine.Statements;

namespace Rockstar.Test;

public abstract class FixtureBase(ITestOutputHelper testOutput) {

	protected static readonly string ExamplesDirectory = Path.Combine("programs", "examples");
	protected static readonly string FixturesDirectory = Path.Combine("programs", "fixtures");

	private static string QualifyRelativePath(string path) {
#if NCRUNCH
		var testProjectFilePath = NCrunchEnvironment.GetOriginalProjectPath();
		var testProjectDirectory = Path.GetDirectoryName(testProjectFilePath);
		var fileInfo = new FileInfo(Path.Combine(testProjectDirectory, path));
		return fileInfo.FullName;
#else
		return Path.GetFullPath(path);
#endif
	}

	private static IEnumerable<string> ListRockFiles(string relativePath) {
		var absolutePath = QualifyRelativePath(relativePath);
		var allFiles = Directory.GetFiles(absolutePath, "*.rock", SearchOption.AllDirectories);
		var trimmedFiles = allFiles.Select(f => f.Replace(absolutePath, "").Trim(Path.DirectorySeparatorChar));
		return trimmedFiles;
	}

	public static IEnumerable<object[]> AllExampleFiles()
		=> ListRockFiles(ExamplesDirectory).Select(filePath => new[] { filePath });

	public static IEnumerable<object[]> AllFixtureFiles()
		=> ListRockFiles(FixturesDirectory).Select(filePath => new[] { filePath });

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

	protected static readonly Parser Parser = new();

	private void PrettyPrint(string source, string filePath, Exception ex) {
		var cursor = ex.Data["cursor"] as Cursor;
		if (cursor == default) return;
		var outputLine = cursor.Line;
		var line = source.Split('\n')[cursor.Line - 1].TrimEnd('\r');
		testOutput.WriteLine(line);
		testOutput.WriteLine(String.Empty.PadLeft(cursor.Column - 1) + "^ error is here!");
		var ncrunchOutputMessage = $"   at <Rockstar code> in {filePath}:line {outputLine}";
		testOutput.WriteLine(ncrunchOutputMessage);
	}

	private string RunProgram(Program program, Queue<string>? inputs = null) {
		string? ReadInput() => inputs != null && inputs.TryDequeue(out var result) ? result : null;
		var env = new TestEnvironment(ReadInput);
		env.Execute(program);
		return env.Output;
	}

	public Program ParseFile(string filePath, string directory) {
		var relativePath = Path.Combine(directory, filePath);
		filePath = QualifyRelativePath(relativePath);
		var source = File.ReadAllText(filePath, Encoding.UTF8);
		try {
			return Parser.Parse(source);
		} catch (FormatException ex) {
			PrettyPrint(source, filePath, ex);
			throw;
		}
	}

	public string RunFile(string filePath, string directory) {
		var relativePath = Path.Combine(directory, filePath);
		filePath = QualifyRelativePath(relativePath);
		var source = File.ReadAllText(filePath, Encoding.UTF8);
		Program program;
		string result;
		try {
			program = Parser.Parse(source);
		} catch (FormatException ex) {
			PrettyPrint(source, filePath, ex);
			throw;
		}
		try {
			var inputs = ReadInputs(filePath);
			result = RunProgram(program, inputs);
			var expect = ExtractExpects(filePath);
			if (String.IsNullOrEmpty(expect)) return result;
			result.ShouldBe(expect);
		} catch (Exception) {
			testOutput.WriteNCrunchFilePath(filePath);
			throw;
		}

		return result;
	}

	private Queue<string>? ReadInputs(string filePath)
		=> File.Exists(filePath + ".in") ? new(File.ReadAllLines(filePath + ".in")) : null;
}