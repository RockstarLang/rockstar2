namespace Rockstar.Test;

public class FunctionTests(ITestOutputHelper output) : ParserTestBase(output) {
	[Theory]
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
}