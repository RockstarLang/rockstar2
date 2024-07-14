namespace Rockstar.Test;

public class RunFixtureTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	public void Fixture(string filePath) => RunFile(filePath, FIXTURES_DIRECTORY);
}

public class ParseFixtureTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	public void ParseFixture(string filePath) => ParseFile(filePath, FIXTURES_DIRECTORY);
}