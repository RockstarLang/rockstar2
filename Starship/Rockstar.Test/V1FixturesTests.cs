namespace Rockstar.Test;
#if DEBUG
public class V1FixturesTests(ITestOutputHelper output) : FixtureBase(output) {
	[Theory]
	[MemberData(nameof(AllV1FixtureFiles))]
	public void RunV1Fixtures(string filePath) => output.WriteLine(RunFile(filePath, V1FixturesDirectory));

	[Theory]
	[MemberData(nameof(AllV1FixtureFiles))]
	public void ParseV1Fixtures(string filePath) {
		if (CheckForExpectedError(filePath, V1FixturesDirectory, out var error)) {
			try {
				ParseFile(filePath, V1FixturesDirectory);
				throw new("Parser should have failed.");
			} catch (Exception ex) {
				ex.Message.ShouldBe(error);
			}
		} else {
			var program = ParseFile(filePath, V1FixturesDirectory);
			output.WriteLine(program);
		}
	}
}
#endif