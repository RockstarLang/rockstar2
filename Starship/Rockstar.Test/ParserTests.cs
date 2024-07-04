using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace Rockstar.Test {
	public class ParserTests {
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		[InlineData("\n")]
		[InlineData("\n\n\n\n\n\n\n\n")]
		[InlineData("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n")]
		[InlineData("   \n")]
		[InlineData("()   \n")]
		[InlineData("()   \r\n")]
		[InlineData("   ()   \r\n")]
		public void ParserParsesEmptyPrograms(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(0);
		}

		[Theory]
		[InlineData("say 1")]
		[InlineData("\nsay 1")]
		[InlineData("\r\nsay 1")]
		[InlineData("     \r\nsay 1")]
		[InlineData("   say 1\n")]
		[InlineData("()   say 1\n")]
		[InlineData("say () 1  \r\n")]
		[InlineData("   () say 1  \r\n")]
		[InlineData("(start with a comment line)\nsay 1  \r\n")]
		[InlineData("(start with a comment line)\r\n\r\nsay 1  \r\n")]
		public void ParserParsesWeirdPrograms(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(1);
		}

		[Theory]
		[InlineData("say 1\nsay 2")]
		[InlineData("\nsay 1\r\nsay 2")]
		[InlineData("\r\nsay 1\nsay 2")]
		[InlineData("     \r\nsay 1\nsay 2")]
		[InlineData("   say 1\nsay 2")]
		[InlineData("()   say 1\nsay 2")]
		[InlineData("say () 1  \r\nsay 2")]
		[InlineData("   () say 1  \r\nsay 2")]
		public void ParserParsesWeirdProgramsWithMultipleStatements(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
			result.Statements.Count.ShouldBe(2);
		}

		[Theory]
		[InlineData("x is true")]
		[InlineData("x is false")]
		[InlineData("x is false or 5")]
		[InlineData("say x and y")]
		public void ParserParsesBooleäns(string source) {
			var parser = new Parser();
			var result = parser.Parse(source);
		} }
}
