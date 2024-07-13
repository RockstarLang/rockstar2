using System.Diagnostics;
using System.Text;
using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public abstract class Statement(Source source) : Expression(source) {
	public Block Concat(IList<Statement> list)
		=> new Block(new List<Statement> { this }.Concat(list));

	public Block Concat(Statement tail)
		=> new Block(new List<Statement> { this }.Concat([ tail ]));

};

public enum Round {
	Up,
	Down,
	Nearest
}

public class Rounding(Variable variable, Round round, Source source) : Statement(source) {
	public Variable Variable => variable;
	public Round Round => round;

}
public class Mutation(Operator op, Expression expr, Source source, Variable? target = default, Expression? modifier = default)
	: Statement(source) {
	public Operator Operator => op;
	public Expression Expression => expr;
	public Variable? Target => target;
	public Expression? Modifier => modifier;
}

public class Noop() : Statement(Source.None) {
	public static Noop Instance => new Noop();
}

public class Break(Source source) : Statement(source) {

}

public class Continue(Source source) : Statement(source) {

}