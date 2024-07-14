using Rockstar.Engine.Expressions;

namespace Rockstar.Test.ParserTests;

public class BinaryTests(ITestOutputHelper output) {

	[Fact]
	public void ComplicatedExpressionListsReduceProperly() {
		var list1 = new OpList(Operator.Plus, [new Number(2), new Number(3), new Number(4), new Number(5)]);
		var list2 = new OpList(Operator.Times, [new Number(5), new Number(6), new Number(7), new Number(8)]);
		var list3 = new OpList(Operator.Plus, [new Number(9), new Number(10), new Number(11), new Number(12)]);

		var binary = Binary.Reduce(new Number(1), new List<OpList> { list1, list2, list3 }, Source.None);
		output.WriteLine(binary.ToString());
	}
}