import { dotnet } from '../wasm/wwwroot/_framework/dotnet.js'

const { getAssemblyExports, getConfig } = await dotnet.withDiagnosticTracing(false).create();

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);

function report(editorId) {
	return function (output) {
		self.postMessage({ output: output, editorId: editorId })
	}
}

async function RunRockstarProgram(source, editorId) {
	try {
		return await exports.Rockstar.Wasm.RockstarRunner.Run(source, report(editorId));
	} catch (error) {
		self.postMessage({ error: error, editorId: editorId })
	}
}

self.addEventListener('message', async function (message) {
	var data = message.data;
	if (data.program) await RunRockstarProgram(data.program, data.editorId);
});