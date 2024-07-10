using System.Text;

namespace Rockstar.Engine.Statements;

public abstract class Statement(Source source) {
	public const string INDENT = "  ";
	public abstract void Print(StringBuilder sb, string prefix = "");
	protected string Location => source.Location;
}