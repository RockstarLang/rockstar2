using System.Text;

namespace Rockstar.Engine.Statements;

public class Program {
	public List<Block> Blocks { get; } = [];
	public Program() { }
	public Program(params Block[] blocks) => this.Blocks.AddRange(blocks);
	public Program Concat(Program tail) {
		this.Blocks.AddRange(tail.Blocks);
		return this;
	}

	public StringBuilder Print(StringBuilder sb, string prefix = "") {
		foreach (var block in Blocks) block.Print(sb, prefix);
		return sb;
	}

	public override string ToString()
		=> Print(new()).ToString();
}