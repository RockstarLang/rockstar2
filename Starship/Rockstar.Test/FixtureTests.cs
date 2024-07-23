namespace Rockstar.Test;

public class FixtureTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	public void RunExamples(string filePath) => output.WriteLine(RunFile(filePath, FixturesDirectory));

	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	public void ParseExamples(string filePath) {
		if (CheckForExpectedError(filePath, ExamplesDirectory, "error", out var error)) {
			try {
				ParseFile(filePath, ExamplesDirectory);
				throw new("Parser should have failed.");
			} catch (Exception ex) {
				ex.Message.ShouldBe(error);
			}
		} else {
			var program = ParseFile(filePath, FixturesDirectory);
			output.WriteLine(program);
		}
	}
}
