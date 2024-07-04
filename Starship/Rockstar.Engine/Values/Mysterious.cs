using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public class Mysterious : Value {
	private Mysterious() : base(Source.None) { }
	public static Mysterious Instance = new();
	public override bool Truthy => false;
}
