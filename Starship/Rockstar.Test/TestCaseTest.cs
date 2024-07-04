namespace Rockstar.Test;

public class TestCaseTest {
	[Fact]
	public void ExpectationExtractorExtractsExpectation() {
		var expect = FixtureBase.ExtractExpects("""
		                                     shout "hello" (expect: hello\n)
		                                     shout "world" (expect: world\n)
		                                     """);
		expect.ShouldBe("hello\nworld\n");
	}

	[Fact]
	public void ExpectationExtractorExtractsPrint() {
		var expect = FixtureBase.ExtractExpects("""
		                                        shout "hello" (prints: hello)
		                                        shout "world" (prints: 12345)
		                                        """);
		expect.ShouldBe("hello\n12345\n");
	}

   [Fact]
	public void ExpectationExtractorExtractsExpectAndPrints() {
		var expect = FixtureBase.ExtractExpects("""
		                                        shout "hello" (expect: hello\n)
		                                        shout "world" (expect: world\n)
		                                        shout "12345" (prints: 12345)
		                                        shout "67890" (prints: 67890)
		                                        """);
		expect.ShouldBe("hello\nworld\n12345\n67890\n");
	}

}
