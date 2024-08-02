using Rockstar.Engine.Statements;
using System.Globalization;
using System.IO;

namespace Rockstar.Test;

public abstract class FixtureBase(ITestOutputHelper testOutput) {

	protected static readonly string ExamplesDirectory = Path.Combine("programs", "examples");
	protected static readonly string FixturesDirectory = Path.Combine("programs", "fixtures");
	protected static readonly string V1FixturesDirectory = Path.Combine("programs", "v1-fixtures");

	//private static string QualifyRelativePath(string path) => Path.GetFullPath(path);

	private static IEnumerable<RockFile> ListRockFiles(string relativePath) {
		//var absolutePath = QualifyRelativePath(relativePath);
		var allFiles = Directory.GetFiles(relativePath, "*.rock", SearchOption.AllDirectories);
		return allFiles.Select(filePath => new RockFile(filePath)).ToList();
	}

	private static IEnumerable<object[]> MemberData(string directory)
		=> ListRockFiles(directory).Select(file => (object[]) [file]).ToList();

	public static IEnumerable<object[]> AllExampleFiles() => MemberData(ExamplesDirectory);
	public static IEnumerable<object[]> AllV1FixtureFiles() => MemberData(V1FixturesDirectory);
	public static IEnumerable<object[]> AllFixtureFiles() => MemberData(FixturesDirectory);

	protected readonly Parser Parser = new();

	private void PrettyPrint(RockFile file, Exception ex) {
		var cursor = ex.Data["cursor"] as Cursor;
		if (cursor == default) return;
		var outputLine = cursor.Line;
		var line = file.Contents.Split('\n')[cursor.Line - 1].TrimEnd('\r');
		testOutput.WriteLine(line);
		testOutput.WriteLine(String.Empty.PadLeft(cursor.Column - 1) + "^ error is here!");
		var ncrunchOutputMessage = $"   at <Rockstar code> in {file.UncrunchedFilePath}:line {outputLine}";
		testOutput.WriteLine(ncrunchOutputMessage);
	}

	protected string RunProgram(Program program, Queue<string>? inputs = null) {
		string? ReadInput() => inputs != null && inputs.TryDequeue(out var result) ? result : null;
		var env = new TestEnvironment(ReadInput);
		env.Execute(program);
		return env.Output;
	}

	//public bool CheckForExpectedError(string filePath, string directory, string label, out string? error) {
	//	var relativePath = Path.Combine(directory, filePath);
	//	filePath = QualifyRelativePath(relativePath);
	//	return ExtractedExpectedError(filePath, label, out error);
	//}

	public Program ParseFile(RockFile rockFile) {
		var source = rockFile.Contents;
		try {
			testOutput.WriteLine($"   at <Rockstar code> in {rockFile.UncrunchedFilePath}:line 1");
			return Parser.Parse(source);
		} catch (FormatException ex) {
			PrettyPrint(rockFile, ex);
			throw;
		}
	}

	protected void TestParser(RockFile rockFile) {
		if (rockFile.ExtractedExpectedError("error", out var error)) {
			try {
				ParseFile(rockFile);
				throw new("Parser should have failed.");
			} catch (Exception ex) {
				ex.Message.ShouldBe(error);
			}
		} else {
			var program = ParseFile(rockFile);
			testOutput.WriteLine(program);
		}
	}

	public void RunFile(RockFile rockFile) {
		if (rockFile.HasExpectedErrors("error")) {
			testOutput.WriteLine($"Skipping {rockFile.UncrunchedFilePath} since it has expected errors.");
			return;
		}
		var source = rockFile.Contents;
		Program program;
		try {
			program = Parser.Parse(source);
		} catch (FormatException ex) {
			PrettyPrint(rockFile, ex);
			throw;
		}
		var result = String.Empty;
		try {
			var inputs = rockFile.SimulateInputs();
			result = RunProgram(program, inputs);
			var expect = rockFile.ExpectedOutput;
			if (String.IsNullOrEmpty(expect)) return;
			var actualOutputPath = $@"D:\rockshit\actual\{rockFile.NameThing}.txt";
			var expectOutputPath = $@"D:\rockshit\expect\{rockFile.NameThing}.txt";
			testOutput.WriteLine(actualOutputPath);
			try {
				File.WriteAllText(actualOutputPath, result.WithDebugInformationRemoved());
				File.WriteAllText(expectOutputPath, expect);
			} catch (Exception ex) {
				testOutput.WriteLine("Exception trying to write test output:");
				testOutput.WriteLine(ex.Message);
			}
			result.WithDebugInformationRemoved().ShouldBe(expect);
		} catch(Exception ex) {
			if (rockFile.ExtractedExpectedError("runtime error", out var error) && ex.Message == error) return;
			testOutput.WriteLine(program.ToString());
			throw;
		} finally {
			testOutput.WriteNCrunchFilePath(rockFile);
			testOutput.WriteLine(result);
		}
	}
}
