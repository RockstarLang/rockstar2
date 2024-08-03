import {
	EditorView, basicSetup, keymap, Prec,
	// Language definitions
	Rockstar, KitchenSink,
	// themes
	kitchenSink, blackSabbath, espresso, cobalt, dracula, solarizedLight, coolGlow, amy
} from './codemirror/editor.js';

const ROCK_BUTTON_HTML = "Rock <i class='fa-solid fa-play'></i>";

const STOP_BUTTON_HTML = "Stop <i class='fa-solid  fa-sync fa-spin'></i>"

var rockCount = 0;

function handleMessageFromWorker(message) {
	if (message.data.editorId) {
		var output = document.getElementById(`rockstar-output-${message.data.editorId}`);
		if (message.data.type == "output") return output.innerText += message.data.output;
		if (message.data.type == "error") {
			output.innerHTML += `<span class="error">error: ${message.data.error}</span>`;
		} else if (message.data.type == "result") {
			output.innerHTML += `<span class="result">&raquo; ${message.data.result}</span>`;
		} else if (message.data.type == "parse") {
			return output.innerText = message.data.result;
		} else {
			console.log(message);
		}
		var button = document.getElementById(`rockstar-button-${message.data.editorId}`);
		button.innerHTML = ROCK_BUTTON_HTML;
		rockCount--;
	} else {
		console.log(message);
	}
}

var worker = new Worker("/js/worker.js", { type: 'module' });
worker.addEventListener("message", handleMessageFromWorker);

function executeProgram(program, editorId) {
	rockCount++;
	worker.postMessage({ command: "run", program: program, editorId: editorId });
}

function parseProgram(program, editorId) {
	worker.postMessage({ command: "parse", program: program, editorId: editorId });
}

function stopTheRock() {
	console.log("STOPPING THE ROCK...");
	worker.terminate();
	worker = new Worker("/js/worker.js", { type: 'module' });
	worker.addEventListener("message", handleMessageFromWorker);
	document.querySelectorAll("button.rock-button").forEach((button) => {
		button.innerHTML = ROCK_BUTTON_HTML;
	})
	// document.querySelectorAll("div.rockstar-controls div.output").forEach(div => div.innerHTML = "");
	rockCount = 0;
}

function makeParseTreeLogger(parser) {
	var textarea = document.getElementById('parseTreeTextarea');
	return function logParseTree(viewUpdate) {
		if (viewUpdate.docChanged) {
			var source = viewUpdate.state.doc.toString();
			logTree(parser.parse(source), textarea);
		}
	};
}

function logTree(tree, targetElement) {
	if (!targetElement) return;
	tree = tree.toString();
	var output = [];
	var indent = "";
	for (var i = 0; i < tree.length; i++) {
		if (tree[i] == ',') {
			output.push(' ');
		} else if (tree[i] == '(') {
			indent += "  ";
			output.push('\n' + indent);
		} else if (tree[i] == ')') {
			indent = indent.substring(2);
			output.push('\n');
			while (tree[i + 1] == ')') {
				indent = indent.substring(2);
				i++;
			}
			output.push(indent);
		} else {
			output.push(tree[i]);
		}
	}
	targetElement.value = output.join('');
}

function makeRockstarRunner(editorId) {
	return function handleCtrlEnter({ state, dispatch }) {
		document.getElementById(`rockstar-button-${editorId}`).click();
		console.log(state);
		console.log(dispatch);
		return true;
	}
}

function replaceElementWithEditor({ editorElement, languageSupport, theme, editorId, parseTreeElement }) {
	let language = (languageSupport ? languageSupport() : null);
	let rockstarKeymap = Prec.highest(
		keymap.of([
			{ key: "Ctrl-Enter", mac: "Cmd-Enter", run: makeRockstarRunner(editorId) }
		])
	);
	let extensions = [basicSetup, rockstarKeymap];
	if (language) extensions.push(language);
	if (theme) extensions.push(theme);
	if (language && parseTreeElement) {
		let logger = makeParseTreeLogger(language.language.parser);
		extensions.push(EditorView.updateListener.of(logger.bind(this)));
		logTree(language.language.parser.parse(editorElement.innerText), parseTreeElement);
	}

	const fixedHeightEditor = EditorView.theme({
		".cm-content, .cm-gutter": { minHeight: "120px" }
		//		"&": {height: "300px"},
		//		".cm-scroller": {overflow: "auto"}
	});
	extensions.push(fixedHeightEditor);
	let view = new EditorView({ doc: editorElement.innerText, extensions: extensions });
	editorElement.parentNode.insertBefore(view.dom, editorElement);
	editorElement.style.display = "none";
	return view;
}

function createControls(editorId, editorView, originalSource) {
	let div = document.createElement("div");
	div.className = "rockstar-controls";
	let output = document.createElement("div");
	output.className = "output";
	let rockButton = document.createElement("button");
	rockButton.className = "rock-button";
	let parseButton = document.createElement("button");
	let resetButton = document.createElement("button");
	let buttonContainer = document.createElement("div");
	buttonContainer.className = "buttons";

	rockButton.id = `rockstar-button-${editorId}`;
	output.id = `rockstar-output-${editorId}`;
	rockButton.innerHTML = ROCK_BUTTON_HTML;
	resetButton.innerHTML = "Reset <i class='fa-solid fa-rotate-right'></i>";
	parseButton.innerHTML = "Parse <i class='fa-solid fa-list-tree'></i>";
	parseButton.onclick = () => {
		let source = editorView.state.doc.toString();
		try {
			parseProgram(source, editorId);
		} catch (e) {
			console.log(e);
		}
	}
	rockButton.onclick = () => {
		console.log(rockCount);
		if (rockCount > 0) {
			stopTheRock();
		} else {
			rockButton.innerHTML = STOP_BUTTON_HTML;
			output.innerText = "";
			let source = editorView.state.doc.toString();
			try {
				executeProgram(source, editorId);
			} catch (e) {
				console.log(e);
			}
		}
	}
	resetButton.onclick = evt => {
		output.innerText = "";
		editorView.dispatch({
			changes: {
				from: 0,
				to: editorView.state.doc.length,
				insert: originalSource
			}
		});
	}
	buttonContainer.appendChild(rockButton);
	buttonContainer.appendChild(parseButton);
	buttonContainer.appendChild(resetButton);
	div.appendChild(buttonContainer);
	div.appendChild(output);
	return div;
}

var editorId = 1;
document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	editorId++;
	var originalSource = el.innerText;
	var settings = {
		editorElement: el,
		parseTreeTextarea: document.getElementById('parseTreeTextarea'),
		language: null, // Rockstar
		theme: coolGlow, // kitchenSink
		editorId: editorId
	};
	var editorView = replaceElementWithEditor(settings);
	var controls = createControls(editorId, editorView, originalSource);
	el.parentNode.insertBefore(controls, el);
});

document.querySelectorAll(('code.language-kitchen-sink')).forEach((el) => {
	replaceElementWithEditor(el, KitchenSink, kitchenSink);
});


