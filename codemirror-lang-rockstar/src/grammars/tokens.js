import { ExternalTokenizer } from "@lezer/lr"
import * as tokens from "./rockstar.terms.js"

const FULL_STOP = 46;

export const variable = new ExternalTokenizer((input, stack) => {
	var tokenTo = properVariable(input);
	if (tokenTo > 1) return input.acceptTokenTo(tokens.properVariable, tokenTo);
	//tokenTo = commonVariable(input);
	//if (tokenTo > 1) return input.acceptTokenTo(tokens.commonVariable, tokenTo);
});

function commonVariable(input) {
	return -1;
	// var nextWord = peekNextWord(input);
	// if (prefixCodes.includes(nextWord)) return 5;
	// {
	// 	input.advance(nextWord.length);
	// 	input.advance(peekNextWord(input).length);
	// 	return input.pos;
	// }
	// return -1;
}

function properVariable(input) {
	var tokenTo = -1;
	let codes = [];
	var i = 0;
	while (input.peek(i) > 0) {
		codes = [];
		if (!upperCodes.includes(input.peek(i))) break;
		codes.push(input.peek(i));
		while (alphaCodes.includes(input.peek(++i))) codes.push(input.peek(i));
		if (isKeyword(codes)) break;
		console.log(String.fromCodePoint(...codes));
		tokenTo = i;
		if (FULL_STOP == input.peek(i)) i++;
		while (spaceCodes.includes(input.peek(++i)));
	}
	return tokenTo;
}

const prefixes = [ "a", "an", "the", "my", "your", "his", "her", "their" ];
const prefixCodes =  prefixes.map(s => stringToCharCodeArray(s));

function peekNextWord(input) {
	var i = 0;
	var codes = [];
	var code = -1;
	while(true) {
		code = input.peek(i);
		console.log(code);
		if (code < 0 || spaceCodes.includes(code)) {
			console.log(String.fromCodePoint(...codes));
			return codes;
		}
		codes.push(code);
		i++;
	}
}



// var codes = [];
// while (input.advance() >= 0) codes.push(input.next);
// console.log(String.fromCodePoint(...codes));
// input.acceptToken(properVariable);

	// while (true) {
	// var word = readNextWord(input);
	// if (word != "") console.log(word);
	//
//};

// while (true) {
// var codes = [];
// if (upperCodes.includes(input.next)) {
// codes.push(input.next);
// while (alphaCodes.includes(input.advance())) codes.push(input.next);
// if (isKeyword(codes)) return;
// while (spaceCodes.includes(input.advance())) codes.push(input.next);
//
	//}
// input.acceptToken(properVariable);
//
//});

// function readNextWord(input) {
// var codes = [];
// while (alphaCodes.includes(input.advance())) codes.push(input.next);
// return String.fromCodePoint(...codes);
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

const aliases = {
	above: ['above', 'over'],
	and: ['and'],
	around: ['around', 'round'],
	as_great: ['great', 'high', 'big', 'strong'],
	as_small: ['less', 'low', 'small', 'weak'],
	as: ['as'],
	at: ['at'],
	back: ['back'],
	be: ['be'],
	break: ['break'],
	build: ['build'],
	call: ['call'],
	cast: ['cast', 'burn'],
	continue: ['continue', 'take'],
	debug: ['debug'],
	divided_by: ['divided', 'between', 'over'],
	down: ['down'],
	else: ['else', 'otherwise'],
	empty: ['empty', 'silent', 'silence'],
	end: ['end', 'yeah', 'baby', 'oh'],
	exactly: ['exactly', 'totally', 'really'],
	false: ["false", "lies", "no", "wrong"],
	if: ['if', 'when'],
	into: ['into', 'in'],
	is: ['is', 'was', 'are', 'were', 'am'],
	isnt: ["isnt", "isn't", 'aint', "ain't", "wasn't", "wasnt", "aren't", "arent", "weren't", "werent"],
	join: ['join', 'unite'],
	knock: ['knock'],
	less: ['less', 'lower', 'smaller', 'weaker'],
	let: ['let'],
	like: ['like', 'so'],
	listen: ['listen'],
	minus: ['minus', 'without'],
	more: ['greater', 'higher', 'bigger', 'stronger', 'more'],
	mysterious: ['mysterious'],
	non: ['non'],
	nor: ['nor'],
	not: ['not'],
	now: ['now'],
	null: ['null', 'nothing', 'nowhere', 'nobody', 'gone'],
	or: ['or'],
	over: ['over'],
	plus: ['plus', 'with'],
	pop: ['roll', 'pop'],
	print: ["print", "shout", "say", "scream", "whisper"],
	pronoun: ['they', 'them', 'she', 'him', 'her', 'hir', 'zie', 'zir', 'xem', 'ver', 'ze', 've', 'xe', 'it', 'he', 'you', 'me', 'i'],
	push: ['rock', 'push'],
	put: ['put'],
	return: ['return', 'giving', 'give', 'send'],
	says: ['say', 'says', 'said'],
	split: ['cut', 'split', 'shatter'],
	takes: ['takes', 'wants'],
	taking: ['taking'],
	than: ['than'],
	the: ['an', 'a', 'the', 'my', 'your', 'our'],
	then: ['then'],
	times: ['times', 'of'],
	to: ['to'],
	true: ["true", "yes", "ok", "right"],
	turn: ['turn'],
	under: ['under', 'below'],
	until: ['until'],
	up: ['up'],
	using: ['using', 'with'],
	while: ['while'],
	with: ['with'],
	write: ['write']
};

const keywords = Object.values(aliases).flat();
