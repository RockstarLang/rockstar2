namespace Rockstar.Engine.Expressions;

public class ProperVariable(string name, Source source) : Variable(name, source) {
	public ProperVariable(string name) : this(name, Source.None) { }
	public override string Key => NormalizedName.ToUpperInvariant();
}