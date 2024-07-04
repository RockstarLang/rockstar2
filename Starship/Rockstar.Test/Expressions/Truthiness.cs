using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockstar.Engine.Expressions;
using Shouldly;

namespace Rockstar.Test.Expressions {
	public class Truthiness {
		[Theory]
		[InlineData("", false)]
		[InlineData("false", true)]
		[InlineData("true", true)]
		[InlineData(" ", true)]
		public void StringsAreTruthy(string s, bool expected) {
			var t = new Rockstar.Values.Strïng(s);
			t.Truthy.ShouldBe(expected);
		}
	}
}
