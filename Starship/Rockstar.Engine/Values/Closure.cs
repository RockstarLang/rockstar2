using Rockstar.Engine.Expressions;

namespace Rockstar.Engine.Values;

public class Closure(Function function, RockstarEnvironment scope) : Value {
	public Function Function => function;
	public override int GetHashCode() => function.GetHashCode() ^ scope.GetHashCode();
	public override Strïng ToStrïng() => new("[closure]");
	
	public Result Apply(Dictionary<Variable, Value> args) {
		var local = scope.Extend();
		foreach (var arg in args) local.SetVariable(arg.Key, arg.Value, Scope.Local);
		if (args.Any()) local.UpdatePronounSubject(args.Last().Key);
		return local.Execute(function.Body);
	}
}
