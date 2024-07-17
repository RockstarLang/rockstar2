import { EditorView, basicSetup, coolGlow, Rockstar } from './rockstar-editor.js';

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


function replaceElementWithEditor(element, RunRockstarProgram) {
	let view = new EditorView({
		doc: element.innerText,
		extensions: [basicSetup, coolGlow, Rockstar()],
	});
	element.parentNode.insertBefore(view.dom, element);
	let button = document.createElement("button");
	button.innerText = "ROCK";
	let output = document.createElement("pre");
	output.className = "rockstar-output";
	element.parentNode.insertBefore(button, element);
	element.parentNode.insertBefore(output, element);
	button.onclick = () => {
		let source = view.state.doc.toString();
		try {
			let result = RunRockstarProgram(source);
			console.log(result);
			output.innerText = result;
		} catch (e) {
			output.innerText = e;
		}
	};
	element.style.display = "none";
	return view;
}

document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	replaceElementWithEditor(el, RunRockstarProgram);
});
