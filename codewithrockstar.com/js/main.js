import { replaceElementWithEditor } from './rockstar-editor.js';

import { dotnet } from '../wasm/wwwroot/_framework/dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);
await dotnet.run();

function RunRockstarProgram(source) {
	return exports.Rockstar.Wasm.RockstarRunner.Run(source);
}

document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	replaceElementWithEditor(el, RunRockstarProgram);
});
