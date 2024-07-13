using Rockstar.Test.ParserTests;

namespace Rockstar.Test;

public class FixtureTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	public void Fixture(string filePath) => RunFile(filePath, FIXTURES_DIRECTORY);
}