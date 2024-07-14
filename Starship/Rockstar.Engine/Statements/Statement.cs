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

public class ExpressionStatement(Expression expr, Source source) : Statement(source) {
	public Expression Expression => expr;
	public override void Print(StringBuilder sb, string prefix) => expr.Print(sb, prefix);
}
