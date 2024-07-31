import * as rockstar from "../src/tokenizers/rockstar.js"
import * as tokens from "../src/grammars/rockstar.terms";

const cases = [
	[["shout", "Print", "ScreAM"], tokens.Print]
];

describe("tokenizer", () => {
	describe.each(cases)("%p all match", (lexemes, token) => {
		test.each(lexemes)("%p", (lexeme) => {
			var input = new parserInput(lexeme);
			rockstar.tokenizeKeyword(input);
			expect(input.token).toBe(token);
		});
	});
});

const notAnyKindOfVariables = ["true", "false", "1", "+2", "-5", "\n", " ", "\t", "12345", "!"]
test.each(notAnyKindOfVariables)("%p is NOT any kind of variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).toBe(undefined);
});


const notCommonVariables = ["his right", "her times"];
test.each(notCommonVariables)("%p is NOT a common variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).not.toBe(tokens.CommonVariable);
});

const commonVariables = ["the night", "a girl", "A BOY", "My Lies", "your love", "his word", "her hair", "an orange", "AN HONOUR",
	"Their guitars", "OUR FLAG" ];
test.each(commonVariables)("%p is a proper variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).toBe(tokens.CommonVariable);
});

const simpleVariables = ["x", "y", "foo", "bar", "myVariable" ];
test.each(simpleVariables)("%p is a simple variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).toBe(tokens.SimpleVariable);
});

const notSimpleVariables = ["say", "times", "Big Daddy", "my leg" ];
test.each(notSimpleVariables)("%p is NOT a simple variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).not.toBe(tokens.SimpleVariable);
});

const notProperVariables = ["My Dad", "Your Lies", "Scream II" ];
test.each(notProperVariables)("%p is NOT a proper variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).not.toBe(tokens.ProperVariable);
});

const properVariables = ["Johnny B. Goode", "Dr. Feelgood", "Black Betty", "Billie Jean", "JRR Tolkien" ];
test.each(properVariables)("%p is a proper variable", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).toBe(tokens.ProperVariable);
});

const pronouns = ['they', 'them', 'she', 'him', 'her', 'hir', 'zie', 'zir', 'xem', 'ver', 'ze', 've', 'xe', 'it', 'he', 'you', 'me', 'i'];
test.each(pronouns)("%p is a pronoun", (lexeme) => {
	var input = new parserInput(lexeme);
	rockstar.tokenizeVariable(input);
	expect(input.token).toBe(tokens.Pronoun);
});

class parserInput {
	#token;
	#s;
	#i = 0;
	constructor(s) { this.#s = s; }
	get token() { return this.#token; }
	get next() { return this.#i >= this.#s.length ? -1 : this.#s.charCodeAt(this.#i); }
	get pos() { return this.#i; }
	peek = (i) => this.#s.charCodeAt(i);
	advance = () => {
		this.#i++;
		return this.next;
	}
	acceptToken = (token) => this.#token = token;
	acceptTokenTo = (token, tokenTo) => this.#token = token;
}
