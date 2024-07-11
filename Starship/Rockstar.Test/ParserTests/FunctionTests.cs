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
		             say Function taking 1, 2
		             """;
		Run(source).ShouldBe("3");
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
