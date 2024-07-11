using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Statements;

public class Return(Expression expr, Source source)
	: Statement(source) {
}
