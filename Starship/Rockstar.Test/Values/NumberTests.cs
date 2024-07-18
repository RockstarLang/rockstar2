using Rockstar.Engine.Values;

namespace Rockstar.Test.Values;

public class NumberTests(ITestOutputHelper output) {
	[Fact]
	public void NumberEqualityWorks() {
		var a = new Number(2);
		var b = new Number(2);
		(a == b).ShouldBe(true);
		a.Equals(b).ShouldBe(true);
	}
}