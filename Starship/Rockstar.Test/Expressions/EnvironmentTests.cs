using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockstar.Engine.Expressions;
using Rockstar.Engine.Values;

namespace Rockstar.Test.Expressions {
	public class EnvironmentTests {

		[Fact]
		public void GlobalScopeWorks() {
			var io = new StringBuilderIO();
			var e1 = new RockstarEnvironment(io);
			var foo = new SimpleVariable("foo");
			var bar = new SimpleVariable("bar");
			var value = new Number(123);
			e1.SetVariable(foo, value);
			var e2 = e1.Extend();
			e2.SetVariable(bar, value);
			var e3 = e2.Extend();
			var e4 = e2.Extend();
			e4.GetScope(foo).ShouldBe(e4);
			e4.GetScope(bar).ShouldBe(e4);

			var n = new Number(456);
			e4.SetVariable(foo, n);
			e4.Lookup(foo).ShouldBe(n);
			e3.Lookup(foo).ShouldBe(value);
			e2.Lookup(foo).ShouldBe(value);
			e1.Lookup(foo).ShouldBe(value);

			e4.SetVariable(foo, n, true);
			e4.Lookup(foo).ShouldBe(n);
			e3.Lookup(foo).ShouldBe(value);
			e2.Lookup(foo).ShouldBe(value);
			e1.Lookup(foo).ShouldBe(value);
		}

		[Theory]
		[InlineData("MY VARIABLE", "my   variable")]
		[InlineData("thE SKY", "the\tsky")]
		[InlineData("your dreams", "Your Dreams")]
		[InlineData("a GIRL", "a   girl")]
		[InlineData("Our price", "OUR    PRICE")]
		public void CommonVariableNamesAreNormalized(string name1, string name2) {
			var io = new StringBuilderIO();
			var e = new RockstarEnvironment(io);
			var v1 = new CommonVariable(name1);
			var v2 = new CommonVariable(name2);
			v1.Key.ShouldBe(v2.Key);
			e.Assign(v1, new Number(123));
			e.Lookup(v2).ShouldBe(new Number(123));
		}

		[Theory]
		[InlineData("Doctor Feelgood", "DOCTOR FEELGOOD")]
		[InlineData("Billie Jean", "BILLIE    JEAN")]
		[InlineData("Billy Ray Cyrus", "BILLY  RAY     \tCyrus")]
		[InlineData("Income Tax", "INCome   TaX")]
		public void ProperVariableNamesAreNormalized(string name1, string name2) {
			var io = new StringBuilderIO();
			var e = new RockstarEnvironment(io);
			var v1 = new ProperVariable(name1);
			var v2 = new ProperVariable(name2);
			v1.Key.ShouldBe(v2.Key);
			e.Assign(v1, new Number(123));
			e.Lookup(v2).ShouldBe(new Number(123));
		}
	}
}
