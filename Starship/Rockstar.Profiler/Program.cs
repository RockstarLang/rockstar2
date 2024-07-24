using System.Diagnostics;
using Rockstar.Engine;

const string DIRECTORY = "../Rockstar.Test/programs";
var fullPath = Path.GetFullPath(DIRECTORY);
Console.WriteLine(fullPath);
var files = Directory.GetFiles(fullPath, "*.rock", SearchOption.AllDirectories);
var parser = new Parser();
var stopwatch = new Stopwatch();

const int FACTOR = 2;
foreach (var file in files) {
	stopwatch.Restart();
	var parseTime = 0;
	var runTime = 0;
	bool error;
	Exception exception = null;
	try {
		var program = parser.Parse(File.ReadAllText(file));
		parseTime = (int)(stopwatch.ElapsedMilliseconds / FACTOR);
		var e = new RockstarEnvironment(new StringBuilderIO());
		stopwatch.Restart();
		var timeout = Task.Delay(TimeSpan.FromMilliseconds(1000));
		error = await Task.WhenAny(Task.Run(() => e.Execute(program)), timeout) == timeout;
		runTime = (int)stopwatch.ElapsedMilliseconds / FACTOR;
	} catch (Exception ex) {
		exception = ex;
		error = true;
	}
	stopwatch.Stop();
	var reportPath = file.Replace(fullPath, "").TrimStart(Path.DirectorySeparatorChar);
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(String.Empty.PadRight(parseTime, '#'));
	if (error) {
		Console.ForegroundColor = ConsoleColor.Red;
		var boom = exception == default ? "TIMEOUT" : exception.Message;
		Console.Write(boom);
		var pad = Math.Max(0, 60 - parseTime - boom.Length);
		Console.Write(String.Empty.PadRight(pad));
	} else {
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(String.Empty.PadRight(runTime, '#'));
		var pad = Math.Max(0, 60 - parseTime - runTime);
		Console.Write(String.Empty.PadRight(pad));
	}

	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(parseTime.ToString().PadLeft(5));
	Console.Write("ms ");
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.Write(runTime.ToString().PadLeft(5));
	Console.Write("ms ");
	Console.WriteLine(reportPath);
}