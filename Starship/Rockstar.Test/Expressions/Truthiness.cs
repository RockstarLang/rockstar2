namespace Rockstar.Test.Expressions {
	public class Truthiness {
		[Theory]
		[InlineData("", false)]
		[InlineData("false", true)]
		[InlineData("true", true)]
		[InlineData(" ", true)]
		public void StringsAreTruthy(string s, bool expected) {
			var t = new Strïng(s);
			t.Truthy.ShouldBe(expected);
		}
	}
}
