import * as rockstar from "../src/tokenizers/rockstar.js"

describe("it", () => {
	test("matches keywords", () => {
		var token = rockstar.tokenizeKeyword(new parserInput("SHOUT")).token;
		expect(token).toBe(tokens.Print);
	});
	it("finds proper variables", () => {
		var input = new parserInput("print hello world");
		rockstar.tokenizeKeyword(input);
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
	#token;
	#s;
	#i = 0;
	constructor(s) { this.#s = s; }
	get token() { return this.#token; }
	get next() { return this.#i >= this.#s.length ? -1 : this.#s.charCodeAt(this.#i) }
	advance = () => {
		this.#i++;
		return this.next;
	}
	acceptToken = (token) => this.#token = token;
}
