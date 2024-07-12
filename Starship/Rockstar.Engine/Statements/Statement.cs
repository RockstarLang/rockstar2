using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public abstract class Statement(Source source) : Expression(source) {
	public Block Concat(IList<Statement> list)
		=> new Block(new List<Statement> { this }.Concat(list));

	public Block Concat(Statement tail)
		=> new Block(new List<Statement> { this }.Concat([ tail ]));

};

public class Noop() : Statement(Source.None) {
	public static Noop Instance => new Noop();
}

public class Break(Source source) : Statement(source) {

}

public class Continue(Source source) : Statement(source) {

}