namespace Rockstar.Engine.Values;

public class Null(Source source)
	: Value(source), IHaveANumber {
	public Null() : this(Source.None) { }
	public override bool Truthy => false;
	public decimal NumericValue => 0;
}