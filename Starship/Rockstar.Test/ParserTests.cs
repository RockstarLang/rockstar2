namespace Rockstar.Test;

public class ParserTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	[MemberData(nameof(AllExampleFiles))]
	[MemberData(nameof(AllV1FixtureFiles))]
	public void Parse(RockFile file) => TestParser(file);
}