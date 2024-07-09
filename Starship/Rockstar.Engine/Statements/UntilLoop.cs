using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class UntilLoop(Expression condition, Block body, Source source)
	: Loop(condition, false, body, source) {
	protected override string LoopType => "until";
}