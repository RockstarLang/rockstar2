import * as tokens from "../grammars/rockstar.terms.js"

const aliases = new Map();

export function tokenizeKeyword(input) {
	var codes = readNextWord(input);
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	aliases.keys().forEach(token => {
		if (aliases.get(token).includes(lexeme)) return input.acceptToken(token);
	});
}

const whitespace = " \t";
const whitespaceCodes = stringToCharCodeArray(whitespace);

function readNextWord(input) {
	var codes = [];
	while(input.next >= 0) {
		if (whitespaceCodes.includes(input.next)) break;
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
		while (whitespaceCodes.includes(input.peek(++i)));
	}
	return tokenTo;
}

const uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĸĹĻĽĿŁŃŅŇŊŌŎŐŒŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŸŹŻŽ";
const lowers = "abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįi̇ĳĵķĸĺļľŀłńņňŋōŏőœŕŗřśŝşšţťŧũūŭůűųŵŷÿźżžŉß";
const upperCodes = stringToCharCodeArray(uppers);
const lowerCodes = stringToCharCodeArray(lowers);
const alphaCodes = upperCodes.concat(lowerCodes);

aliases.set(tokens.Above, ['above', 'over']);
aliases.set(tokens.And, ['and']);
aliases.set(tokens.Around, ['around', 'round']);
aliases.set(tokens.As, ['as']);
aliases.set(tokens.AsGreat, ['great', 'high', 'big', 'strong']);
aliases.set(tokens.AsSmall, ['less', 'low', 'small', 'weak']);
aliases.set(tokens.At, ['at']);
aliases.set(tokens.Back, ['back']);
aliases.set(tokens.Be, ['be']);
aliases.set(tokens.Break, ['break']);
aliases.set(tokens.Build, ['build']);
aliases.set(tokens.Call, ['call']);
aliases.set(tokens.Cast, ['cast', 'burn']);
aliases.set(tokens.Continue, ['continue', 'take']);
aliases.set(tokens.Debug, ['debug']);
aliases.set(tokens.Divided, ['divided', 'between', 'over']);
aliases.set(tokens.Down, ['down']);
aliases.set(tokens.Else, ['else', 'otherwise']);
aliases.set(tokens.Empty, ['empty', 'silent', 'silence']);
aliases.set(tokens.End, ['end', 'yeah', 'baby', 'oh']);
aliases.set(tokens.Exactly, ['exactly', 'totally', 'really']);
aliases.set(tokens.False, ["false", "lies", "no", "wrong"]);
aliases.set(tokens.His, ['his', 'her']);
aliases.set(tokens.If, ['if', 'when']);
aliases.set(tokens.Into, ['into', 'in']);
aliases.set(tokens.Is, ['is', 'was', 'are', 'were', 'am']);
aliases.set(tokens.Isnt, ["isnt", "isn't", 'aint', "ain't", "wasn't", "wasnt", "aren't", "arent", "weren't", "werent"]);
aliases.set(tokens.Join, ['join', 'unite']);
aliases.set(tokens.Knock, ['knock']);
aliases.set(tokens.Less, ['less', 'lower', 'smaller', 'weaker']);
aliases.set(tokens.Let, ['let']);
aliases.set(tokens.Like, ['like', 'so']);
aliases.set(tokens.Listen, ['listen']);
aliases.set(tokens.Minus, ['minus', 'without']);
aliases.set(tokens.More, ['greater', 'higher', 'bigger', 'stronger', 'more']);
aliases.set(tokens.Mysterious, ['mysterious']);
aliases.set(tokens.Non, ['non']);
aliases.set(tokens.Nor, ['nor']);
aliases.set(tokens.Not, ['not']);
aliases.set(tokens.Now, ['now']);
aliases.set(tokens.Null, ['null', 'nothing', 'nowhere', 'nobody', 'gone']);
aliases.set(tokens.Or, ['or']);
aliases.set(tokens.Over, ['over']);
aliases.set(tokens.Plus, ['plus', 'with']);
aliases.set(tokens.Pop, ['roll', 'pop']);
aliases.set(tokens.Print, ["print", "shout", "say", "scream", "whisper"]);
aliases.set(tokens.Pronoun,	['they', 'them', 'she', 'him', 'her', 'hir', 'zie', 'zir', 'xem', 'ver', 'ze', 've', 'xe', 'it', 'he', 'you', 'me', 'i']);
aliases.set(tokens.Push, ['rock', 'push']);
aliases.set(tokens.Put, ['put']);
aliases.set(tokens.Return, ['return', 'giving', 'give', 'send']);
aliases.set(tokens.Says, ['say', 'says', 'said']);
aliases.set(tokens.Split, ['cut', 'split', 'shatter']);
aliases.set(tokens.Takes, ['takes', 'wants']);
aliases.set(tokens.Taking, ['taking']);
aliases.set(tokens.Than, ['than']);
aliases.set(tokens.The, ['an', 'a', 'the', 'my', 'your', 'our', 'their']);
aliases.set(tokens.Then, ['then']);
aliases.set(tokens.Times, ['times', 'of']);
aliases.set(tokens.To, ['to']);
aliases.set(tokens.True, ["true", "yes", "ok", "right"]);
aliases.set(tokens.Turn, ['turn']);
aliases.set(tokens.Under, ['under', 'below']);
aliases.set(tokens.Until, ['until']);
aliases.set(tokens.Up, ['up']);
aliases.set(tokens.Using, ['using', 'with']);
aliases.set(tokens.While, ['while']);
aliases.set(tokens.With, ['with']);
aliases.set(tokens.Write, ['write']);


// export const OutputTokenizer = CreateTokenizer(tokens.output, [ "print", "say", "shout", "scream", "whisper" ])

// export const OutputFinder = new ExternalTokenizer(OutputTokenizer);

// export const ProperVariableFinder = new ExternalTokenizer((input, stack) => {

// })
// function CreateTokenizer(token, lexemes) {
// 	lexemes = (typeof(lexemes) == "string" ? lexemes : Object.values(lexemes).flat());
// 	return (input, stack) => {
// 		var codes = readNextWord(input);
// 		var lexeme = String.fromCodePoint(...codes);
// 		if (lexemes.includes(lexeme.toLowerCase())) {
// 			console.log('"' + lexeme + '" looks like a token! ' + token);
// 			return input.acceptToken(token);
// 		}
// 	};
// }


// export const CommonVariableFinder
// export const Constant = CreateTokenizer(tokens.Constant, Keywords.Constants);
// // export const ArithmeticOperator = CreateTokenizer(tokens.ArithmeticOperator, Keywords.ArithmeticOperators)
// export const Pronoun = CreateTokenizer(tokens.Pronoun, Keywords.Pronouns);

// export const listen = CreateTokenizer(tokens.listen, "listen");
// export const to = CreateTokenizer(tokens.listen, "to");


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
