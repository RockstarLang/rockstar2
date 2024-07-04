import {parser} from "./rockstar.grammar"
import {LRLanguage, LanguageSupport, indentNodeProp, foldNodeProp, foldInside, delimitedIndent} from "@codemirror/language"
// import {styleTags, tags as t} from "@lezer/highlight"
import { completeFromList } from "@codemirror/autocomplete"

export const RockstarLanguage = LRLanguage.define({
  parser: parser.configure({
    props: [
      indentNodeProp.add({
        Application: delimitedIndent({closing: ")", align: false})
      }),
      foldNodeProp.add({
        Application: foldInside
      }),
    //   styleTags({
    //     Identifier: t.variableName,
    //     Boolean: t.bool,
    //     String: t.string,
    //     Comment: t.comment,
    //     "( )": t.paren
    //   })
    ]
  }),
  languageData: {
    commentTokens: {line: ";"}
  }
})

const RockstarAutocomplete = RockstarLanguage.data.of({
	autocomplete: completeFromList([
		{ label: "defun", type: "keyword" },
		{ label: "defvar", type: "keyword" },
		{ label: "let", type: "keyword" },
		{ label: "cons", type: "function" },
		{ label: "car", type: "function" },
		{ label: "cdr", type: "function" }
	])
});
export function Rockstar() {
  return new LanguageSupport(RockstarLanguage, [ RockstarAutocomplete ])
}
