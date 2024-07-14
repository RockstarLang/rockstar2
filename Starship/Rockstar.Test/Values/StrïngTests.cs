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
}