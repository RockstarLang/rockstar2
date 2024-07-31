import * as tokens from "../grammars/rockstar.terms.js"


const ASCII = {
	FullStop: 46
};

export function tokenizeKeyword(input) {
	var codes = readNextWord(input);
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	for (const [key, value] of aliases) {
		if (value.includes(lexeme)) return input.acceptToken(key);
	}
}

export function tokenizeVariable(input) {
	var codes = readNextWord(input);
	if (! codes.length) return;
	var lexeme = String.fromCodePoint(...codes);
	if (aliases.get(tokens.The).includes(lexeme.toLowerCase())) {
		readNextWord(input);
		return input.acceptToken(tokens.CommonVariable);
	}
	if (aliases.get(tokens.His).includes(lexeme.toLowerCase())) {
		codes = readNextWord(input);
		if (codes.length == 0 || isKeyword(codes)) return input.acceptToken(tokens.Pronoun);
		return input.acceptToken(tokens.CommonVariable);
	}
	if (aliases.get(tokens.Pronoun).includes(lexeme.toLowerCase())) return input.acceptToken(tokens.Pronoun);
	if (isKeyword(codes)) return;
	var tokenTo = -1;
	while (codes.length) {
		if (upperCodes.includes(codes[0])) {
			if (isKeyword(codes)) break;
			while (input.next == ASCII.FullStop) input.advance();
			while (whitespaceCodes.includes(input.next)) input.advance();
			tokenTo = input.pos;
		}
		codes = readNextWord(input);
	}
	if (tokenTo >= 0) return input.acceptTokenTo(tokens.ProperVariable, tokenTo);
	if (!isKeyword(codes)) return input.acceptToken(tokens.SimpleVariable);
}

// export function tokenizePronoun(input) {
// 	var codes = readNextWord(input);
// 	var lexeme = String.fromCodePoint(...codes).toLowerCase();

// }

// export function tokenizeSimpleVariable(input) {
// 	var codes = readNextWord(input);
// 	if (! isKeyword(codes)) return input.acceptToken(tokens.SimpleVariable);
// }

// export function tokenizeCommonVariable(input) {
// 	var codes = readNextWord(input);
// 	var lexeme = String.fromCodePoint(...codes).toLowerCase();


// }
// export function tokenizeProperVariable(input) {
// 	var tokenTo = -1;
// 	let codes = [];
// 	var i = 0;
// 	while (input.next > 0) {
// 		codes = [];
// 		if (!upperCodes.includes(input.next)) break;
// 		codes.push(input.peek(i));
// 		while (alphaCodes.includes(input.advance())) codes.push(input.next);
// 		if (isKeyword(codes)) break;
// 		tokenTo = i;
// 		console.log(String.fromCodePoint(...codes));
// 		if (ASCII.FullStop == input.peek(i)) i++;
// 		while (whitespaceCodes.includes(input.peek(++i)));
// 	}
// 	if (tokenTo >= 0) input.acceptTokenTo(tokens.ProperVariable, tokenTo);
// }

function isKeyword(codes) {
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	return keywords.includes(lexeme);
}

const whitespace = " \t";
const whitespaceCodes = stringToCharCodeArray(whitespace);

function readNextWord(input) {
	let codes = [];
	while (whitespaceCodes.includes(input.next)) input.advance();
	while(alphaCodes.includes(input.next)) {
		codes.push(input.next);
		input.advance();
	}
	return codes;
}

function stringToCharCodeArray(s) {
	var result = [];
	for (var i = 0; i < s.length; i++) result.push(s.charCodeAt(i));
	return result;
}

const uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĸĹĻĽĿŁŃŅŇŊŌŎŐŒŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŸŹŻŽ";
const lowers = "abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįi̇ĳĵķĸĺļľŀłńņňŋōŏőœŕŗřśŝşšţťŧũūŭůűųŵŷÿźżžŉß";
const upperCodes = stringToCharCodeArray(uppers);
const lowerCodes = stringToCharCodeArray(lowers);
const alphaCodes = upperCodes.concat(lowerCodes);

const aliases = new Map();
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
aliases.set(tokens.Pronoun, ['they', 'them', 'she', 'him', 'her', 'hir', 'zie', 'zir', 'xem', 'ver', 'ze', 've', 'xe', 'it', 'he', 'you', 'me', 'i']);
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

const keywords = Array.from(aliases.values()).flat();