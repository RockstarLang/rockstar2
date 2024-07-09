using Pegasus.Common;
using System.Text.RegularExpressions;

namespace Rockstar.Engine;

public class Source(int line, int column, string lexeme = "") {

	public string Location => 
		$"(line {line}, column {column - lexeme.Length} [{lexeme}])";

	public static readonly Source None = new(0, 0, "");
}

public static class PegasusParserExtensions {
	public static Source Source(this Cursor cursor, string lexeme = "")
		=> new(cursor.Line, cursor.Column, lexeme);

	public static string Error(this Cursor cursor, string unexpected)
		=> "Unexpected '" + Regex.Escape(unexpected) + "' at line " + cursor.Line + ", col " + (cursor.Column - 1);
}
