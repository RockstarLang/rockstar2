import { replaceElementWithEditor } from './rockstar-editor.js';


import { dotnet } from '../wasm/_framework/dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);
await dotnet.run();

function RunRockstarProgram(source) {
	return exports.RockstarRunner.Run(source);
}

document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	replaceElementWithEditor(el, RunRockstarProgram);
});
