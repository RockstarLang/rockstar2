namespace Rockstar.Test;

public class ExampleTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllExampleFiles))]
	public void Example(string filePath) => RunFile(filePath, EXAMPLES_DIRECTORY);
}
