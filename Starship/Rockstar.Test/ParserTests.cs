namespace Rockstar.Test;

public class ParserTests(ITestOutputHelper output) : FixtureBase(output) {

	[Theory]
	[MemberData(nameof(AllFixtureFiles))]
	[MemberData(nameof(AllExampleFiles))]
	[MemberData(nameof(AllV1FixtureFiles))]
	public void Parse(RockFile file) => TestParser(file);

	//[Theory]
	//[MemberData(nameof(AllV1FixtureFiles))]
	//[MemberData(nameof(AllExampleFiles))]
	//public void ParseV1Fixtures(RockFile file) => TestParser(file);

	//[Theory]
	//[MemberData(nameof(AllExampleFiles))]
	//public void ParseExampleFixtures(RockFile file) => TestParser(file);
}