import { EditorView, basicSetup } from "codemirror"
import { RockstarLanguageSupport } from "./codemirror-lang-rockstar.js"
import { KitchenSinkLanguageSupport } from "./codemirror-lang-kitchen-sink.js";

// import { kitchenSink } from "./themes/kitchen-sink.js";
// import { coolGlow, boysAndGirls, cobalt, tomorrow } from 'thememirror';
// import { blackSabbath } from "./themes/black-sabbath.js"

export { EditorView, basicSetup,
	// Languages
	KitchenSinkLanguageSupport,
	RockstarLanguageSupport,
	// themes
	kitchenSink,
	coolGlow, boysAndGirls, cobalt, tomorrow, blackSabbath
};