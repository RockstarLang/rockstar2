import * as rockstar from "../src/tokenizers/rockstar-lexer.js"
import * as tokens from "../src/grammars/rockstar.terms.js";

import { parser } from "../src/grammars/rockstar.js";

test("parse", () => {
	let result = parser.parse("say 1 say 2");
	console.log(result);
});


