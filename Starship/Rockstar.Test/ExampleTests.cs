namespace Rockstar.Test;

public class ExampleTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void RunExamples(string filePath) => output.WriteLine(RunFile(filePath, ExamplesDirectory));

	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void ParseExamples(string filePath) {
		if (CheckForExpectedError(filePath, ExamplesDirectory, out var error)) {
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