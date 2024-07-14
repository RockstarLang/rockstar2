namespace Rockstar.Test.ParserTests;

public class FunctionTests(ITestOutputHelper output) : ParserTestBase(output) {

	[Theory]
	[InlineData("""
	            OuterFunction takes X
	            InnerFunction takes X
	            say X with X
	            (end InnerFunction)
	            say X
	            (end OuterFunction)
	            """)]
	[InlineData("""
	            Function takes x and y
	            say x with y
	            (end function)
	            """)]
	[InlineData("""
	            Echo takes X
	            say X
	            (end function)
	            """)]
	[InlineData("sum taking 2, 3")]
	[InlineData("""
	            Echo takes X
	            say X
	            (end function)
	            Echo taking true
	            Echo taking "hello world"
	            put 5 into Temp
	            Echo taking Temp
	            """)]
	public void ParserParsesFunctions(string source) {
		var result = Parse(source);
	}

	[Fact]
	public void ParserParsesArrayCopy() {
		var source = """
		             ArrayCopy takes source
		             rock dest
		             let i be 0
		             let len be source + 0
		             while i is less than len
		             let dest at i be source at i
		             build i up

		             return dest

		             """;
		Parse(source).Statements.Count.ShouldBe(1);

	}
	[Fact]
	public void ParserParsesFunctionWithReturnStatement() {
		var source = """
		             Function takes x
		             give back x with x
		             (end function)
		             """;
		Parse(source);
	}

	[Fact]
	public void FunctionWithReturnStatementWorks() {
		var source = """
		             Function takes x
		             give back x with x
		             (end function)
		             say Function taking 2
		             """;
		Run(source).ShouldBe("4\n".ReplaceLineEndings());
	}

	[Fact]
	public void ParseHelloWorldWorks() {
		var source = """
		             Eternity takes the pain.
		             The prize is silence
		             Until the pain is nothing,
		             Roll the pain into violence,
		             Cast violence into your lies,
		             Let the prize be with your lies. 

		             Give back the prize


		             """;
		Parse(source).Statements.Count.ShouldBe(1);
	}

	[Fact]
	public void ParseFunctionWorks() {
		var source = """
		             AddAndPrint takes X, and Y
		             put X plus Y into Value
		             say Value
		             Give back Value
		             (end function)
		             say AddAndPrint taking 3, 4
		             """;
		Run(source).ShouldBe("7\n7\n".ReplaceLineEndings());
	}

	[Fact]
	public void ParsePolly() {
		var source = """
		             Polly wants a cracker
		             Cheese is delicious
		             Put a cracker with cheese in your mouth
		             Give it back
		             
		             Shout Polly taking 100
		             """;
		Run(source).ShouldBe("109\n".ReplaceLineEndings());
	}

	[Fact]
	public void RecursionWorks() {
		var source = """
		             Decrement takes X
		             If X is nothing
		             Give back X
		             Else
		             Put X minus 1 into NewX
		             Give back Decrement taking NewX
		             
		             Say Decrement taking 5
		             
		             """;
		var program = Parse(source);
		output.WriteLine(program.ToString());
		//var result = Run(program);
		//Run(source).ShouldBe("0\n".ReplaceLineEndings());

	}

	[Fact]
	public void ParseFunctionWithExpressionListWorks() {
		var source = """
		             AddOrSub takes X, and B
		             if B
		             Give Back X plus 1
		             (end if)
		             say "else"
		             Give Back X minus 1
		             (end function)
		             say AddOrSub taking 4, true
		             say AddOrSub taking 4, false

		             """;
		var program = Parse(source);
		output.WriteLine(program.ToString());
		var result = Run(program);
		Run(source).ShouldBe("5\nelse\n3\n".ReplaceLineEndings());
	}

	[Fact]
	public void FunctionWithMultipleParametersAndReturnStatementWorks() {
		var source = """
		             Function takes x and y and z
		             give back x with " " with y with " " with z
		             (end function)
		             say Function taking "eddie", "van", and "halen"
		             """;
		Run(source).ShouldBe("eddie van halen\n".ReplaceLineEndings());
	}

	[Theory]
	[InlineData("""
	            Foo takes bar
	            say bar
	            bar = 2
	            say bar
	            (end function)

	            let bar be 1
	            say bar
	            Foo taking bar
	            say bar
	            """)]
	public void FunctionScopeIsClosed(string source) {
		Run(source).ShouldBe("""
		                     1
		                     1
		                     2
		                     1

		                     """);
	}

	[Theory]
	[InlineData("""
	            Foo takes bar
	            say bar
	            (end function)

	            Foo taking 1
	            Foo taking 2
	            Foo taking 3
	            """)]
	public void InterpreterRunsFunctions(string source) {
		Run(source).ShouldBe("""
		                     1
		                     2
		                     3
		                     
		                     """);
	}

	[Fact]
	public void FunctionCallFailsIfVariableIsNotAFunction() {
		ShouldThrow("""
		            Let NotAFunction be 1
		            NotAFunction taking 12
		            """, ex => ex.Message.ShouldBe("'NotAFunction' is not a function"));
	}

	[Fact]
	public void FunctionFailsIfCalledWithWrongNumberOfArguments() {
		ShouldThrow("""
			    MyFunction takes x
			    say x

			    MyFunction taking 1, 2
			    """, ex => ex.Message.ShouldBe("Wrong number of arguments supplied to function MyFunction - expected 1 (x), got 2"));
	}
}
