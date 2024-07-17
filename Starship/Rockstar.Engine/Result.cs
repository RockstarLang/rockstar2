namespace Rockstar.Engine; 

public partial class Parser {
	private Dictionary<string, string[]> keywords = new() {
		{ "is", ["is", "was", "were", "are"] }
	};

	public bool Alias(string key, string alias)
		=> keywords.TryGetValue(key, out var aliases) && aliases.Contains(alias);

}
//using Rockstar.Engine.Values;

//namespace Rockstar.Engine;

//public enum WhatToDo {
//	Unknown,
//	Next,
//	Skip,
//	Break,
//	Return
//}

//public class Result(Value value, WhatToDo whatToDo = WhatToDo.Next) {
//	public Value Value => value;
//	public WhatToDo WhatToDo => whatToDo;
//	public static Result Continue => new(new Null(), WhatToDo.Skip);
//	public static Result Break => new(new Null(), WhatToDo.Break);
//	public static Result Unknown = new(new Null(), WhatToDo.Unknown);
//	public static Result Return(Value value) => new(value, WhatToDo.Return);
//}
