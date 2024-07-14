namespace Rockstar.Test;

public class ExampleTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void RunExamples(string filePath) => output.WriteLine(RunFile(filePath, ExamplesDirectory));

	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void ParseExamples(string filePath) {
		var program = ParseFile(filePath, ExamplesDirectory);
		output.WriteLine(program);
	}
}