using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockstar.Engine.Values;

namespace Rockstar.Test.Values;

public class StrïngTests(ITestOutputHelper output) {

	[Fact]
	public void StringEqualityWorks() {
		var s1 = new Strïng(String.Empty);
		var s2 = new Strïng(String.Empty);
		(s1 == s2).ShouldBe(true);
		s1.Equals(s2).ShouldBe(true);
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

	[Theory]
	[InlineData("a", 1, "a")]
	[InlineData("a", 0, "")]
	[InlineData("a", -1, "a")]
	[InlineData("a", 2, "aa")]
	[InlineData("abc", 0, "")]
	[InlineData("abc", -1, "cba")]
	[InlineData("abc", -2, "cbacba")]
	[InlineData("abc", 0.1, "a")]
	[InlineData("abc", 0.5, "ab")]
	[InlineData("abcdefghij", 0.1, "a")]
	[InlineData("abcdefghij", 0.2, "ab")]
	[InlineData("abcdefghij", 0.3, "abc")]
	[InlineData("abcdefghij", 0.4, "abcd")]
	[InlineData("abcdefghij", 0.5, "abcde")]
	[InlineData("abcdefghij", 0.6, "abcdef")]
	[InlineData("abcdefghij", 0.7, "abcdefg")]
	[InlineData("abcdefghij", 0.8, "abcdefgh")]
	[InlineData("abcdefghij", 0.9, "abcdefghi")]
	[InlineData("abcdefghij", 1.1, "abcdefghija")]
	[InlineData("abcdefghij", -0.1, "j")]
	[InlineData("abcdefghij", -0.2, "ji")]
	[InlineData("abcdefghij", -0.3, "jih")]
	[InlineData("abcdefghij", -0.4, "jihg")]
	[InlineData("abcdefghij", -0.5, "jihgf")]
	[InlineData("abcdefghij", -0.6, "jihgfe")]
	[InlineData("abcdefghij", -0.7, "jihgfed")]
	[InlineData("abcdefghij", -0.8, "jihgfedc")]
	[InlineData("abcdefghij", -0.9, "jihgfedcb")]
	[InlineData("abcdefghij", -1.1, "jihgfedcbaj")]
	[InlineData("r", 0.999999999, "r")]
	[InlineData("hello world", 0.0000000001, "h")]
	public void StringMultiplicationWorks(string input, decimal factor, string expected)
		=> new Strïng(input).Times(factor).ShouldBe(new Strïng(expected));

	[Theory]
	[InlineData("a", "a", "")]
	[InlineData("aa", "a", "a")]
	[InlineData("a", "b", "a")]
	[InlineData("hello world", "world", "hello ")]
	public void StringSubtractionWorks(string minuend, string subtrahend, string difference)
		=> new Strïng(minuend).Minus(new Strïng(subtrahend)).ShouldBe(new Strïng(difference));

	[Theory]
	[InlineData("a", "a", 1)]
	[InlineData("aaa", "a", 3)]
	[InlineData("abcde", "f", 0)]
	[InlineData("", "", 0)]
	[InlineData("banana", "na", 2)]
	[InlineData("one potato two potato three potato four", "potato", 3)]
	public void StringDivisionWorks(string numerator, string denominator, string quotient)
		=> new Strïng(numerator).DividedBy(new Strïng(denominator)).ShouldBe(new Number(quotient));

	[Theory]
	[InlineData("a", 1, "a")]
	[InlineData("aaa", 2, "aa")]
	[InlineData("abcde", 0.5, "abcdeabcde")]
	[InlineData("abcdef", 2, "abc")]
	[InlineData("abcde", -1, "edcba")]
	public void StringDivisionWorks(string numerator, decimal denominator, string quotient)
		=> new Strïng(numerator).DividedBy(denominator).ShouldBe(new Strïng(quotient));
}