import { EditorState } from "@codemirror/state"
import { EditorView, basicSetup } from "codemirror"
// import { ViewUpdate } from "@codemirror/view"
import { Rockstar } from "./codemirror-lang-rockstar.ts"

export function editorFromTextArea(textarea) {
	let view = new EditorView({
		state: EditorState.create({
			extensions: [
				basicSetup,
				Rockstar(),
				EditorView.updateListener.of((update) => {
					if (update.docChanged) textarea.value = update.state.doc.toString();
				})
			],
		})
	});
	textarea.parentNode.insertBefore(view.dom, textarea);
	textarea.style.display = "none";
	if (textarea.form) textarea.form.addEventListener("submit", () => {
		textarea.value = view.state.doc.toString()
	})
	return view;
}
let editor = new EditorView({
	extensions: [basicSetup, Rockstar()],
	parent: document.body
})