using Pegasus.Common.Tracing;

namespace Rockstar.Test.ParserTests;

public class TestOutputTracer(ITestOutputHelper Output) : ITracer {
	public void TraceCacheHit<T>(string ruleName, Cursor cursor, CacheKey cacheKey, IParseResult<T> parseResult) {
		
	}

	public void TraceCacheMiss(string ruleName, Cursor cursor, CacheKey cacheKey) {
		
	}

	public void TraceInfo(string ruleName, Cursor cursor, string info) {
		
	}

	public void TraceRuleEnter(string ruleName, Cursor cursor) {
		
	}

	public void TraceRuleExit<T>(string ruleName, Cursor cursor, IParseResult<T> parseResult) {
		Output.WriteLine(ruleName);
	}
}