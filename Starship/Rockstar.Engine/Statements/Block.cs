using System.Text;

namespace Rockstar.Engine.Statements;

public class Block(IEnumerable<Statement> statements) {

	public Block(IEnumerable<Block> blocks) : this(blocks.SelectMany(b => b.Statements)) { }
	public List<Statement> Statements { get; } = statements.ToList();
	public static Block Empty => new();
	public bool IsEmpty => !statements.Any();

	public Block Concat(IEnumerable<Statement> tail) {
		Statements.AddRange(tail);
		return this;
	}
	public Block Concat(IEnumerable<Block> tail) {
		Statements.AddRange(tail.SelectMany(t => t.Statements));
		return this;
	}

	public Block Concat(Block tail) {
		Statements.AddRange(tail.Statements);
		return this;
	}

	public Block Insert(Statement statement) {
		Statements.Insert(0, statement);
		return this;
	}

	public Block() : this(new List<Block>()) { }

	public Block(Statement statement) : this([statement]) { }

	public override string ToString() {
		var sb = new StringBuilder();
		Print(sb);
		return sb.ToString();
	}

	public void Print(StringBuilder sb, string prefix = "") {
		foreach (var stmt in Statements) stmt.Print(sb, prefix);
	}
}
