namespace Rockstar.Engine.Expressions;

public class CommonVariable(string name, Source source) : Variable(name, source) {
	public CommonVariable(string name) : this(name, Source.None) { }
	public override string Key => NormalizedName.ToLowerInvariant();
}