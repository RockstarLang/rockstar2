namespace Rockstar.Engine.Expressions;

public class SimpleVariable(string name, Source source) : Variable(name, source) {
	public SimpleVariable(string name) : this(name, Source.None) { }
	public override string Key => Name.ToLowerInvariant();
}