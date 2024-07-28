import { ExternalTokenizer } from "@lezer/lr"
import {
	properVariable
} from "./rockstar.terms.js"

const FULL_STOP = 46;

export const matchProperVariable = new ExternalTokenizer((input, stack) => {
	var tokenTo = -1;
	let codes = [];
	while (input.next > 0) {
		codes = [];
		if (!upperCodes.includes(input.next)) break;
		codes.push(input.next);
		while (alphaCodes.includes(input.advance())) codes.push(input.next);
		if (isKeyword(codes)) break;
		tokenTo = input.pos;
		if (FULL_STOP == input.next) input.advance();
		while(spaceCodes.includes(input.advance()));
	}
	if (tokenTo >= 0) return input.acceptTokenTo(properVariable, tokenTo);
});

// var codes = [];
// 	while(input.advance() >= 0) codes.push(input.next);
// 	console.log(String.fromCodePoint(...codes));
// 	input.acceptToken(properVariable);

	// while(true) {
	// 	var word = readNextWord(input);
	// 	if (word != "") console.log(word);
	// };

// while (true) {
// 	var codes = [];
// 	if (upperCodes.includes(input.next)) {
// 		codes.push(input.next);
// 		while (alphaCodes.includes(input.advance())) codes.push(input.next);
// 		if (isKeyword(codes)) return;
// 		while (spaceCodes.includes(input.advance())) codes.push(input.next);
// 	}
// 	input.acceptToken(properVariable);
// });

// function readNextWord(input) {
// 	var codes = [];
// 	while (alphaCodes.includes(input.advance())) codes.push(input.next);
// 	return String.fromCodePoint(...codes);
// }

const whitespace = " \t";
const spaceCodes = stringToCharCodeArray(whitespace);

const uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĸĹĻĽĿŁŃŅŇŊŌŎŐŒŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŸŹŻŽ";
const lowers = "abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįi̇ĳĵķĸĺļľŀłńņňŋōŏőœŕŗřśŝşšţťŧũūŭůűųŵŷÿźżžŉß";
const upperCodes = stringToCharCodeArray(uppers);
const lowerCodes = stringToCharCodeArray(lowers);
const alphaCodes = upperCodes.concat(lowerCodes);

function stringToCharCodeArray(s) {
	var result = [];
	for (var i = 0; i < s.length; i++) result.push(s.charCodeAt(i));
	return result;
}

function isKeyword(codes) {
	var token = String.fromCodePoint(...codes);
	return keywords.includes(token.toLowerCase());
}

const keywords = [
	"a",
	"above",
	"ain't",
	"aint",
	"am",
	"an",
	"and",
	"are",
	"aren't",
	"arent",
	"around",
	"as",
	"at",
	"baby",
	"back",
	"be",
	"below",
	"between",
	"big",
	"bigger",
	"break",
	"build",
	"burn",
	"by",
	"call",
	"cast",
	"continue",
	"cut",
	"debug",
	"divided",
	"down",
	"else",
	"empty",
	"end",
	"exactly",
	"false",
	"give",
	"giving",
	"gone",
	"great",
	"greater",
	"he",
	"her",
	"high",
	"higher",
	"him",
	"hir",
	"i",
	"if",
	"in",
	"into",
	"is",
	"isn't",
	"isnt",
	"it",
	"join",
	"knock",
	"less",
	"less",
	"let",
	"lies",
	"like",
	"listen",
	"low",
	"lower",
	"me",
	"minus",
	"more",
	"my",
	"mysterious",
	"no",
	"nobody",
	"non",
	"nor",
	"not",
	"nothing",
	"now",
	"nowhere",
	"null",
	"of",
	"oh",
	"ok",
	"or",
	"otherwise",
	"our",
	"over",
	"plus",
	"pop",
	"print",
	"push",
	"put",
	"really",
	"return",
	"right",
	"rock",
	"roll",
	"round",
	"said",
	"say",
	"say",
	"says",
	"scream",
	"send",
	"shatter",
	"she",
	"shout",
	"silence",
	"silent",
	"small",
	"smaller",
	"so",
	"split",
	"strong",
	"stronger",
	"take",
	"takes",
	"taking",
	"than",
	"the",
	"them",
	"then",
	"they",
	"times",
	"to",
	"totally",
	"true",
	"turn",
	"under",
	"unite",
	"until",
	"up",
	"using",
	"ve",
	"ver",
	"wants",
	"was",
	"wasn't",
	"wasnt",
	"weak",
	"weaker",
	"were",
	"weren't",
	"werent",
	"when",
	"while",
	"whisper",
	"with",
	"with",
	"with",
	"without",
	"write",
	"wrong",
	"xe",
	"xem",
	"yeah",
	"yes",
	"you",
	"your",
	"ze",
	"zie",
	"zir"
];
