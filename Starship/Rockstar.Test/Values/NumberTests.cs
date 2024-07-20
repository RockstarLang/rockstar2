using System.Security.Cryptography.X509Certificates;
using Rockstar.Engine.Values;

namespace Rockstar.Test.Values;

public class TruthinessTests(ITestOutputHelper output) {
	[Fact]
	public void Booleäns() {
		Booleän.True.Truthy.ShouldBe(true);
		Booleän.False.Truthy.ShouldBe(false);
	}

	[Fact]
	public void Strïngs() {
		new Strïng(" ").Truthy.ShouldBe(true);
	}
}
public class NumberTests(ITestOutputHelper output) {
	[Fact]
	public void NumberEqualityWorks() {
		var a = new Number(2);
		var b = new Number(2);
		(a == b).ShouldBe(true);
		a.Equals(b).ShouldBe(true);
	}
}