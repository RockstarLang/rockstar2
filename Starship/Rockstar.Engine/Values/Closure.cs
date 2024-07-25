using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public class Closure(Function function, RockstarEnvironment env) : Value {
	public Function Function => function;
	private readonly RockstarEnvironment scope = env.Extend();
	public override int GetHashCode() => function.GetHashCode() ^ env.GetHashCode();
	public override Strïng ToStrïng() => new("[closure]");
	
	public Result Apply(Dictionary<Variable, Value> args) {
		var local = this.scope.Extend();
		foreach (var arg in args) local.SetLocal(arg.Key, arg.Value);
		if (args.Any()) local.UpdatePronounTarget(args.Last().Key);
		return local.Execute(function.Body);
	}
}
