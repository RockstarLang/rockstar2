import { EditorView, basicSetup } from "codemirror"
import { keymap } from "@codemirror/view"
import { Rockstar } from "./rockstar"
import { KitchenSink } from "./kitchen-sink"
import { Prec } from "@codemirror/state";

import { kitchenSink, blackSabbath, cobalt, dracula, espresso, solarizedLight } from "./themes";

export {
	EditorView, basicSetup,
	keymap, Prec,
	// Languages
	KitchenSink,
	Rockstar,
	// themes
	kitchenSink,
	blackSabbath,
	espresso,
	cobalt,
	dracula,
	solarizedLight
};
