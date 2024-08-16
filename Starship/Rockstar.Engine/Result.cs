using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public class Result(Value value, WhatToDo whatToDo = WhatToDo.Next) {
	public override string ToString() => WhatToDo switch {
		WhatToDo.Return => $"value: {value}",
		WhatToDo.Skip => "skip",
		WhatToDo.Break => "break",
		WhatToDo.Next => "next",
		WhatToDo.Exit => "exit",
		_ => "unknown"
	};

	public Value Value => value;
	public WhatToDo WhatToDo => whatToDo;
	public static Result Skip => new(new N�ll(), WhatToDo.Skip);
	public static Result Break => new(new N�ll(), WhatToDo.Break);
	public static Result Unknown = new(new N�ll(), WhatToDo.Unknown);
	public static Result Return(Value value) => new(value, WhatToDo.Return);
	public static Result Exit => new(new N�ll(), WhatToDo.Exit);
}
