import { ExternalTokenizer } from "@lezer/lr"
import * as tokens from "./rockstar.terms.js"

// import { Keywords } from "./keywords.js"
// const FULL_STOP = 46;

function CreateTokenizer(token, lexemes) {
	lexemes = (typeof(lexemes) == "string" ? lexemes : Object.values(lexemes).flat());
	return (input, stack) => {
		var codes = readNextWord(input);
		var lexeme = String.fromCodePoint(...codes);
		if (lexemes.includes(lexeme.toLowerCase())) {
			console.log('"' + lexeme + '" looks like a token! ' + token);
			return input.acceptToken(token);
		}
	};
}

export const KeywordTokenizer = new ExternalTokenizer((input, stack) => {
	var codes = readNextWord(input);
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	switch(lexeme) {
		case "listen": input.acceptToken(tokens.Listen);
		case "burn": input.acceptToken(tokens.Burn);
		case "to": input.acceptToken(tokens.To);
		case "the": input.acceptToken(tokens.The);
	}
});

export const OutputTokenizer = CreateTokenizer(tokens.OutputToken, [ "print", "say", "shout", "scream", "whisper" ])

export const OutputFinder = new ExternalTokenizer(OutputTokenizer);

// export const ProperVariableFinder = new ExternalTokenizer((input, stack) => {

// })

// export const CommonVariableFinder
// export const Constant = CreateTokenizer(tokens.Constant, Keywords.Constants);
// // export const ArithmeticOperator = CreateTokenizer(tokens.ArithmeticOperator, Keywords.ArithmeticOperators)
// export const Pronoun = CreateTokenizer(tokens.Pronoun, Keywords.Pronouns);

// export const listen = CreateTokenizer(tokens.listen, "listen");
// export const to = CreateTokenizer(tokens.listen, "to");

const whitespace = " \t";
const spaceCodes = stringToCharCodeArray(whitespace);

function readNextWord(input) {
	var codes = [];
	while(input.next >= 0) {
		if (input.next < 0 || spaceCodes.includes(input.next)) break;
		codes.push(input.next);
		input.advance()
	}
	return codes;
}

function stringToCharCodeArray(s) {
	var result = [];
	for (var i = 0; i < s.length; i++) result.push(s.charCodeAt(i));
	return result;
}

function properVariable(input) {
	var tokenTo = -1;
	let codes = [];
	var i = 0;
	while (input.next > 0) {
		codes = [];
		if (!upperCodes.includes(input.next)) break;
		codes.push(input.peek(i));
		while (alphaCodes.includes(input.advance())) codes.push(input.next);
		if (isKeyword(codes)) break;
		tokenTo = i;
		if (FULL_STOP == input.peek(i)) i++;
		while (spaceCodes.includes(input.peek(++i)));
	}
	return tokenTo;
}

const uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĸĹĻĽĿŁŃŅŇŊŌŎŐŒŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŸŹŻŽ";
const lowers = "abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįi̇ĳĵķĸĺļľŀłńņňŋōŏőœŕŗřśŝşšţťŧũūŭůűųŵŷÿźżžŉß";
const upperCodes = stringToCharCodeArray(uppers);
const lowerCodes = stringToCharCodeArray(lowers);
const alphaCodes = upperCodes.concat(lowerCodes);

// // export const variable = new ExternalTokenizer((input, stack) => {
// // 	var tokenTo = properVariable(input);
// // 	if (tokenTo > 1) return input.acceptTokenTo(tokens.properVariable, tokenTo);
// // 	//tokenTo = commonVariable(input);
// // 	//if (tokenTo > 1) return input.acceptTokenTo(tokens.commonVariable, tokenTo);
// // });

// // function commonVariable(input) {
// // 	return -1;
// // 	// var nextWord = peekNextWord(input);
// // 	// if (prefixCodes.includes(nextWord)) return 5;
// // 	// {
// // 	// 	input.advance(nextWord.length);
// // 	// 	input.advance(peekNextWord(input).length);
// // 	// 	return input.pos;
// // 	// }
// // 	// return -1;
// // }


// // const prefixes = [ "a", "an", "the", "my", "your", "his", "her", "their" ];
// // const prefixCodes =  prefixes.map(s => stringToCharCodeArray(s));

// // function peekNextWord(input) {
// // 	var i = 0;
// // 	var codes = [];
// // 	var code = -1;
// // 	while(true) {
// // 		code = input.peek(i);
// // 		console.log(code);
// // 		if (code < 0 || spaceCodes.includes(code)) {
// // 			console.log(String.fromCodePoint(...codes));
// // 			return codes;
// // 		}
// // 		codes.push(code);
// // 		i++;
// // 	}
// // }



// // // var codes = [];
// // // while (input.advance() >= 0) codes.push(input.next);
// // // console.log(String.fromCodePoint(...codes));
// // // input.acceptToken(properVariable);

// // 	// while (true) {
// // 	// var word = readNextWord(input);
// // 	// if (word != "") console.log(word);
// // 	//
// // //};

// // // while (true) {
// // // var codes = [];
// // // if (upperCodes.includes(input.next)) {
// // // codes.push(input.next);
// // // while (alphaCodes.includes(input.advance())) codes.push(input.next);
// // // if (isKeyword(codes)) return;
// // // while (spaceCodes.includes(input.advance())) codes.push(input.next);
// // //
// // 	//}
// // // input.acceptToken(properVariable);
// // //
// // //});

// // // function readNextWord(input) {
// // // var codes = [];
// // // while (alphaCodes.includes(input.advance())) codes.push(input.next);
// // // return String.fromCodePoint(...codes);
// // // }



// function isKeyword(codes) {
// 	var token = String.fromCodePoint(...codes);
// 	return keywords.includes(token.toLowerCase());
// }

// const aliases = {
// 	above: ['above', 'over'],
// 	and: ['and'],
// 	around: ['around', 'round'],
// 	as: ['as'],
// 	at: ['at'],
// 	back: ['back'],
// 	be: ['be'],
// 	break: ['break'],
// 	build: ['build'],
// 	call: ['call'],
// 	cast: ['cast', 'burn'],
// 	continue: ['continue', 'take'],
// 	debug: ['debug'],
// 	down: ['down'],
// 	else: ['else', 'otherwise'],
// 	end: ['end', 'yeah', 'baby', 'oh'],
// 	if: ['if', 'when'],
// 	into: ['into', 'in'],
// 	join: ['join', 'unite'],
// 	knock: ['knock'],
// 	let: ['let'],
// 	like: ['like', 'so'],
// 	listen: ['listen'],
// 	mysterious: ['mysterious'],
// 	non: ['non'],
// 	nor: ['nor'],
// 	not: ['not'],
// 	now: ['now'],
// 	null: ['null', 'nothing', 'nowhere', 'nobody', 'gone'],
// 	or: ['or'],
// 	over: ['over'],
// 	pop: ['roll', 'pop'],
// 	print: ["print", "shout", "say", "scream", "whisper"],
// 	push: ['rock', 'push'],
// 	put: ['put'],
// 	return: ['return', 'giving', 'give', 'send'],
// 	says: ['say', 'says', 'said'],
// 	split: ['cut', 'split', 'shatter'],
// 	takes: ['takes', 'wants'],
// 	taking: ['taking'],
// 	than: ['than'],
// 	the: ['an', 'a', 'the', 'my', 'your', 'our'],
// 	then: ['then'],
// 	to: ['to'],
// 	turn: ['turn'],
// 	under: ['under', 'below'],
// 	until: ['until'],
// 	up: ['up'],
// 	using: ['using', 'with'],
// 	while: ['while'],
// 	with: ['with'],
// 	write: ['write']
// };

// const keywords = Object.values(aliases).flat();
