import {
	EditorView, basicSetup,
	// Language definitions
	Rockstar, KitchenSink,
	// themes
	kitchenSink, blackSabbath, espresso, cobalt, dracula, solarizedLight
} from './codemirror/editor.js';

function handleMessageFromWorker(message) {
	if (message.data.editorId) {
		var output = document.getElementById(`rockstar-output-${message.data.editorId}`);
		var button = document.getElementById(`rockstar-button-${message.data.editorId}`);
		button.classList.remove("running");
		button.innerText = "Rock";
		if (message.data.error) {
			output.innerText = message.data.error;
		} else {
			output.innerText += message.data.output;
		}
	} else {
		console.log(message);
	}
}

var worker = new Worker("/js/worker.js", { type: 'module' });
worker.addEventListener("message", handleMessageFromWorker);

function executeProgram(program, editorId) {
	worker.postMessage({ program: program, editorId: editorId });
}

function replaceElementWithEditor(element, languageSupport, theme) {
	var language = languageSupport();
	let view = new EditorView({ doc: element.innerText, extensions: [basicSetup, language, theme] });
	console.log(language.language.parser.parse(element.innerText).toString());
	element.parentNode.insertBefore(view.dom, element);
	element.style.display = "none";
	return view;
}

var editorId = 1;
document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	editorId++;
	let output = document.createElement("pre");
	let button = document.createElement("button");
	button.className = "rockstar-button";
	output.className = "rockstar-output";
	button.id = `rockstar-button-${editorId}`;
	output.id = `rockstar-output-${editorId}`;
	button.innerText = "Rock";
	var editor = replaceElementWithEditor(el, Rockstar, kitchenSink);
	el.parentNode.insertBefore(button, el);
	el.parentNode.insertBefore(output, el);
	button.onclick = () => {
		button.innerText = "Stop";
		button.classList.add("running");
		output.innerText = "";
		let source = editor.state.doc.toString();
		try {
			executeProgram(source, editorId);
		} catch (e) {
			console.log(e);
		}
	};

});

document.querySelectorAll(('code.language-kitchen-sink')).forEach((el) => {
	replaceElementWithEditor(el, KitchenSink, kitchenSink);
});


