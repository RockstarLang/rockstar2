using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public enum WhatToDo {
	Unknown,
	Next,
	Skip,
	Break,
	Return
}

public class Result(Value value, WhatToDo whatToDo = WhatToDo.Next) {
	public Value Value => value;
	public WhatToDo WhatToDo => whatToDo;
	public static Result Unknown = new(new Null(), WhatToDo.Unknown);
	public static Result Return(Value value) => new Result(value, WhatToDo.Return);
}
