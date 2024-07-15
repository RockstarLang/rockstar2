using Rockstar.Engine.Expressions;
using Rockstar.Engine.Statements;
using Rockstar.Engine.Values;

namespace Rockstar.Engine;

public class RockstarEnvironment(IRockstarIO io) {
	public RockstarEnvironment(IRockstarIO io, RockstarEnvironment parent) : this(io) {
		Parent = parent;
	}

	public RockstarEnvironment? Parent { get; init; }
	public RockstarEnvironment Extend() => new(IO, this);
	protected IRockstarIO IO = io;

	public string? ReadInput() => IO.Read();
	public void WriteLine(string? output) => IO.Write((output ?? String.Empty) + Environment.NewLine);
	public void Write(string output) => IO.Write(output);

	public Result Execute(Program program)
		=> program.Blocks.Aggregate(Result.Unknown, (_, block) => Execute(block));

	private Result Execute(Block block)
		=> block.Statements.Aggregate(Result.Unknown, (_, statement) => Execute(statement));
	
	private Result Execute(Statement statement) => statement switch {
		Output output => Output(output),
		_ => throw new($"I don't know how to execute {statement.GetType().Name} statements")
	};

	private Result Output(Output output) {
		var value = Eval(output.Expression);
		WriteLine("YEAH" + value.ToStrïng().Value);
		return new(value);
	}

	private Value Eval(Expression expr) => expr switch {
		Value value => value,
		_ => throw new NotImplementedException($"Eval not implemented for {expr.GetType()}")
	};
}

public class Result(Value value) {
	public Value Value => value;
	public static Result Unknown = new Result(Mysterious.Instance);
}