namespace Rockstar.Engine.Values;

public class Null(Source source)
	: Value(source) {
	public Null() : this(Source.None) { }
	public override bool Truthy => false;
}