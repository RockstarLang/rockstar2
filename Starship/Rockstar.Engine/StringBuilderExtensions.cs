using System.Text;

namespace Rockstar;

public static class StringBuilderExtensions {
	public static StringBuilder Indent(this StringBuilder sb, int depth)
		=> sb.Append(String.Empty.PadLeft(depth * 2));
}
