namespace Rockstar.Test;

public static class TestOutputHelperExtensions {
	public static void WriteNCrunchFilePath(this ITestOutputHelper output, string path)
		=> output.WriteLine($"   at <Rockstar code> in {path}:line 1");
}
