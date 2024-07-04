using System.Text;

namespace Rockstar.Engine.Statements;

public class Progräm {
	private readonly List<Statement> statements = [];
	public List<Statement> Statements => statements;

	public Progräm Insert(Statement statement) {
		statements.Insert(0, statement);
		return this;
	}

	public Progräm() { }

	public Progräm(Statement statement) => statements = [statement];
	public override string ToString() {
		var sb = new StringBuilder();
		foreach (var stmt in Statements) stmt.Print(sb);
		return sb.ToString();
	}
}
