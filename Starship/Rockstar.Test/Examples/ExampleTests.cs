namespace Rockstar.Test.Examples;

public class ExampleTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void RunExamples(string filePath) {
		if (CheckForExpectedError(filePath, ExamplesDirectory, "runtime error", out var error)) {
			try {
				output.WriteLine(RunFile(filePath, ExamplesDirectory));
				throw new("Interpreter should have failed.");
			} catch (Exception ex) {
				ex.Message.ShouldBe(error);
			}
		} else {
			output.WriteLine(RunFile(filePath, ExamplesDirectory));
		}
	}

	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void ParseExamples(string filePath) {
		if (CheckForExpectedError(filePath, ExamplesDirectory, "error", out var error)) {
			try {
				ParseFile(filePath, ExamplesDirectory);
				throw new("Parser should have failed.");
			} catch (Exception ex) {
				ex.Message.ShouldBe(error);
			}
		} else {
			var program = ParseFile(filePath, ExamplesDirectory);
			output.WriteLine(program);
		}
	}
}