import { Rockstar } from "./parser/editor.js"
import { OutputTokenizer } from "../src/grammars/rockstar-tokenizer.js";

//import { fileTests } from "@lezer/generator/dist/test"

//import * as fs from "fs"
//import * as path from "path"
//import { fileURLToPath } from 'url';
//let caseDir = path.dirname(fileURLToPath(import.meta.url))

//for (let file of fs.readdirSync(caseDir)) {
//if (!/\.txt$/.test(file)) continue

//let name = /^[^\.]*/.exec(file)[0]
//describe(name, () => {
//for (let { name, run } of fileTests(fs.readFileSync(path.join(caseDir, file), "utf8"), file))
//it(name, () => run(Rockstar.parser))
//})
//}

describe("it", () => {
	it("finds proper variables", () => {
		var input = new parserInput("print hello world");
		OutputTokenizer(input);
	});
	it("works", () => {
		var input = new parserInput("hello");
		var codes = [];
		while (input.next > 0) {
			codes.push(input.next);
			input.advance();
		}
		console.log(String.fromCodePoint(...codes));
	});
});

class parserInput {
	#s;
	#i = 0;
	constructor(s) {
		this.#s = s;
	}
	get next() { return this.#i >= this.#s.length ? -1 : this.#s.charCodeAt(this.#i) }
	advance = () => {
		this.#i++;
		return this.next;
	}
	acceptToken = (token) => console.log(token);
}
