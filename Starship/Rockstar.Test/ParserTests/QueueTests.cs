namespace Rockstar.Test.ParserTests;

public class ListOperatorTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParseWithoutList() {
		var source = "say 10 without 1, 2, and 3";
		var parsed = Parse(source);
		var result = Run(parsed);
		result.ShouldBe("4\n".ReplaceLineEndings());
	}

	[Fact]
	public void ParseWolf() {
		var source = """
		             The wolf is hungry, out on the street
		             Fear is the mind killer
		             Fury is the demon child
		             Hate is the only truth
		             Let the wolf be without fear, fury, and hate
		             Shout the wolf

		             """;
		var parsed = Parse(source);
		var result = Run(parsed);
		result.ShouldBe("62190\n".ReplaceLineEndings());
	}

	[Fact]
	public void ParseTommy() {
		var source = "Let Tommy be with \" to\", \" what\", \" we've\", \" got\"";
		var parsed = Parse(source);
		parsed.Statements.Count.ShouldBe(1);
		output.WriteLine(parsed.ToString());
	}

	[Fact]
	public void WolfNoot() {
		var source = "say 99999 without 8888, 77, 6";
		var parsed = Parse(source);
		output.WriteLine(parsed.ToString());
		Run(parsed).ShouldBe("91028\n".ReplaceLineEndings());
	}
}

public class QueueTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Fact]
	public void ParseQueueProgram() {
		var source = """
		             rock the list with "first"
		             while the list ain't nothing
		             shout roll the list

		             """;
		var parsed = Parse(source);
		output.WriteLine(parsed.ToString());
		var result = Run(parsed);
		result.ShouldBe("first\n".ReplaceLineEndings());
	}
}