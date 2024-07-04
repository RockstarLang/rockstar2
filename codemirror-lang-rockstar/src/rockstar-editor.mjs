import { EditorView, basicSetup } from "codemirror"
import { Rockstar } from "./codemirror-lang-rockstar.ts"

export function replaceElementWithEditor(element, RunRockstarProgram) {
	let view = new EditorView({
		doc: element.innerText,
		extensions: [basicSetup, Rockstar()],
	});
	element.parentNode.insertBefore(view.dom, element);
	let button = document.createElement("button");
	button.innerText = "Run";
	let output = document.createElement("pre");
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