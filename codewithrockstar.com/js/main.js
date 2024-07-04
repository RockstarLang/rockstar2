import { replaceElementWithEditor } from './rockstar-editor.js';

import { RunRockstarProgram } from '../wasm/wwwroot/main.js';

document.querySelectorAll(('code.language-rockstar')).forEach((el) => {
	replaceElementWithEditor(el, RunRockstarProgram);
});
