using Rockstar.Engine.Values;

namespace Rockstar.Test.Values;

public class TruthinessTests {
	[Fact]
	public void Booleäns() {
		Booleän.True.Truthy.ShouldBe(true);
		Booleän.False.Truthy.ShouldBe(false);
	}

	[Fact]
	public void Strïngs() {
		new Strïng("false").Truthy.ShouldBe(true);
		new Strïng(" ").Truthy.ShouldBe(true);
		new Strïng("").Truthy.ShouldBe(false);
	}


	[Fact]
	public void Number() {
		new Number(-1).Truthy.ShouldBe(true);
		new Number(1).Truthy.ShouldBe(true);
		new Number(0).Truthy.ShouldBe(false);
	}
}
