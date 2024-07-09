using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class WhileLoop(Expression condition, Block body, Source source)
	: Loop(condition, true, body, source) {
	protected override string LoopType => "while";
}