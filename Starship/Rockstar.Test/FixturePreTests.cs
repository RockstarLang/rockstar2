using Shouldly;
using Xunit.Abstractions;

namespace Rockstar.Test;

public class FixturePreTests(ITestOutputHelper testOutput) : FixtureBase(testOutput) {
	[Theory]
	[MemberData(nameof(GetFiles))]
	public void FileHasExpectations(string filePath) {
		var expect = ExtractExpects(filePath);
		expect.ShouldNotBeEmpty();
	}
}
