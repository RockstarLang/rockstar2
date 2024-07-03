import {EditorView, basicSetup} from "codemirror"
import {Rockstar} from "./codemirror-lang-rockstar.ts"

let editor = new EditorView({
  extensions: [basicSetup, Rockstar()],
  parent: document.body
})