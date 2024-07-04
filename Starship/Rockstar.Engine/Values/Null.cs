using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public class Null(Source source)
	: Value(source) {
	public override bool Truthy => false;
}