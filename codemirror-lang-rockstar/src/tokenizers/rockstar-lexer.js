import * as tokens from "../grammars/rockstar.terms.js"
import { ASCII } from './ascii.js';

const compareOperators = [">=", "<=", ">", "<", "="];
const arithmeticOperators = ["+", "/", "*", "-"];

const noise = " \t,?!./;";
const noiseCodes = stringToCharCodeArray(noise);

const endOfStatementMarkers = ",?!./;";
const endOfStatementCodes = stringToCharCodeArray(endOfStatementMarkers);

export function tokenizeEndOfStatement(input) {
	var found = false;
	while(input.next >= 0) {
		while(spaceCodes.includes(input.next)) input.advance();
		if (input.next == ASCII.LF) {
			found = true;
			break;
		}
		while(spaceCodes.includes(input.next)) input.advance();
		if (endOfStatementCodes.includes(input.next)) found = true;
		input.advance();
	}
	if (found) return input.acceptTokens(tokens.EOS);
}

export function tokenizeEndMarkers(input) {
	// Skip all trailing punctuation
	while(noiseCodes.includes(input.next)) input.advance();
	if (input.next == ASCII.CR) input.advance();
	if (input.next == ASCII.LF) {
		input.advance(); // eat the newline.
		var i = 0;
		while (input.peek(i) >= 0) {
			while (noiseCodes.includes(input.peek(i++)));
			if (input.peek(i) == ASCII.CR) i++;
			if (input.peek(i) == ASCII.LF) return input.acceptToken(tokens.EOB);
			let [codes, index] = peekNextWord(input);
			var lexeme = String.fromCodePoint(...codes).toLowerCase();
			if (aliases.get(tokens.Else).includes(lexeme)) return input.acceptToken(tokens.EOB);
			if (aliases.get(tokens.End).includes(lexeme)) return input.acceptToken(tokens.EOB);
		}
	}
}

export function tokenizePoeticNumber(input) {
	let codes = [];
	while (whitespaceCodes.includes(input.next)) input.advance();
	// poetic numbers always start with a letter
	if (!(alphaCodes.concat([ASCII.Apostrophe, ASCII.Hyphen]).includes(input.next))) return;
	while (input.next >= 0 && !whitespaceCodes.includes(input.next)) {
		codes.push(input.next);
		input.advance();
	}
	let lexeme = String.fromCodePoint(...codes).toLowerCase();
	if (isArithmeticOperator(lexeme)) return;
	while (input.next >= 0 && input.next != ASCII.LF) input.advance();
	return input.acceptToken(tokens.PoeticNumber);
}

export function tokenizePoeticString(input) {
	while (input.next >= 0 && input.next != ASCII.LF) input.advance();
	input.acceptToken(tokens.PoeticString);
}

function isArithmeticOperator(lexeme) {
	if (compareOperators.includes(lexeme)) return true;
	if (arithmeticOperators.includes(lexeme)) return true;
	for (var op of operatorMaps.keys()) {
		for (var token of operatorMaps.get(op)) {
			if (aliases.get(token).includes(lexeme)) return true;
		}
	}
	return false;
}

export function tokenizeOperator(input) {
	var codes = readNextWordIncludingOperators(input);
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	if (compareOperators.includes(lexeme)) return input.acceptToken(tokens.CompareOperator);
	if (arithmeticOperators.includes(lexeme)) return input.acceptToken(tokens.ArithmeticOperator);
	if (aliases.get(tokens.Is).includes(lexeme)) {
		codes = readNextWordIncludingOperators(input);
		lexeme = String.fromCodePoint(...codes).toLowerCase();
		for (var token of [tokens.Above, tokens.Less, tokens.More, tokens.Under]) {
			if (aliases.get(token).includes(lexeme)) {
				var tokenTo = input.pos;
				codes = readNextWordIncludingOperators(input);
				lexeme = String.fromCodePoint(...codes).toLowerCase();
				if (aliases.get(tokens.Than).includes(lexeme)) {
					return input.acceptToken(tokens.CompareOperator);
				} else {
					return input.acceptTokenTo(tokens.CompareOperator, tokenTo);
				}
			}
		}
		if (lexeme == "as") {
			codes = readNextWordIncludingOperators(input);
			lexeme = String.fromCodePoint(...codes).toLowerCase();
			for (var token of [tokens.Great, tokens.Small]) {
				if (aliases.get(token).includes(lexeme)) {
					codes = readNextWordIncludingOperators(input);
					lexeme = String.fromCodePoint(...codes).toLowerCase();
					if (aliases.get(tokens.As).includes(lexeme)) return input.acceptToken(tokens.CompareOperator);
				}
			}
		}
	} else {
		for (var op of operatorMaps.keys()) {
			for (var token of operatorMaps.get(op)) {
				if (aliases.get(token).includes(lexeme)) return input.acceptToken(op);
			}
		}
	}
}

const operatorMaps = new Map();
operatorMaps.set(tokens.CompareOperator, [tokens.Not, tokens.Is, tokens.Isnt]);
operatorMaps.set(tokens.ArithmeticOperator, [tokens.Plus, tokens.Minus, tokens.Divided, tokens.Times]);
operatorMaps.set(tokens.LogicOperator, [tokens.And, tokens.Nor, tokens.Or]);

export function tokenizeKeyword(input) {
	var codes = readNextWord(input);
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	for (const [key, value] of aliases) {
		if (value.includes(lexeme)) return input.acceptToken(key);
	}
}

const MAXIMUM_PARTS_IN_A_PROPER_VARIABLE = 99;

export function tokenizeVariable(input) {
	var codes = readNextWord(input);
	if (!codes.length) return;
	var lexeme = String.fromCodePoint(...codes);

	if (aliases.get(tokens.The).includes(lexeme.toLowerCase())) {
		readNextWord(input);
		return input.acceptToken(tokens.CommonVariable);
	}

	if (aliases.get(tokens.His).includes(lexeme.toLowerCase())) {
		codes = readNextWord(input);
		lexeme += " " + String.fromCodePoint(...codes);
		if (codes.length == 0 || isKeyword(codes)) return input.acceptToken(tokens.Pronoun);
		return input.acceptToken(tokens.CommonVariable);
	}

	if (aliases.get(tokens.Pronoun).includes(lexeme.toLowerCase())) return input.acceptToken(tokens.Pronoun);
	if (isKeyword(codes)) return;

	while (input.next == ASCII.FullStop) {
		codes.push(input.next);
		input.advance();
	}
	var properNouns = [];
	var index = 0;
	var tokenTo = input.pos;

	for(var i = 0; i < MAXIMUM_PARTS_IN_A_PROPER_VARIABLE; i++) {
		if (isKeyword(codes)) break;
		if (upperCodes.includes(codes[0])) {
			properNouns.push(String.fromCodePoint(...codes));
			input.advance(index);
			tokenTo += index;
		}
		[codes, index] = peekNextWord(input);
	}
	if (properNouns.length > 0) return input.acceptTokenTo(tokens.ProperVariable, tokenTo);
	return input.acceptToken(tokens.SimpleVariable);
}

function isKeyword(codes) {
	var lexeme = (typeof (codes) == "string" ? codes.toLowerCase() : (String.fromCodePoint(...codes).toLowerCase()));
	return keywords.includes(lexeme);
}

const whitespace = " \t";
const whitespaceCodes = stringToCharCodeArray(whitespace);

const readNextWordIncludingOperators = (input) => readWhileContains(input, alphaCodes.concat(opCodes));
const readNextWordIncludingApostrophes = (input) => readWhileContains(input, [ASCII.Apostrophe].concat(alphaCodes));

const readNextWord = (input) => readWhileContains(input, alphaCodes);

const peekNextWord = (input) => {
	let codes = [];
	let index = 0;
	while (whitespaceCodes.includes(input.peek(index))) index++;
	if (input.peek(index) <= 0) return [codes, index];
	while (alphaCodes.includes(input.peek(index))) codes.push(input.peek(index++));
	while(input.peek(index) == ASCII.FullStop) codes.push(input.peek(index++));
	return [codes, index];
}

function readWhileContains(input, accept) {
	let codes = [];
	while (whitespaceCodes.includes(input.next)) input.advance();
	while (accept.includes(input.next)) {
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

const operators = "'<>=!+/-*";
const uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞĀĂĄĆĈĊČĎĐĒĔĖĘĚĜĞĠĢĤĦĨĪĬĮİĲĴĶĸĹĻĽĿŁŃŅŇŊŌŎŐŒŔŖŘŚŜŞŠŢŤŦŨŪŬŮŰŲŴŶŸŹŻŽ";
const lowers = "abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþāăąćĉċčďđēĕėęěĝğġģĥħĩīĭįi̇ĳĵķĸĺļľŀłńņňŋōŏőœŕŗřśŝşšţťŧũūŭůűųŵŷÿźżžŉß";
const opCodes = stringToCharCodeArray(operators);
const upperCodes = stringToCharCodeArray(uppers);
const lowerCodes = stringToCharCodeArray(lowers);
const alphaCodes = upperCodes.concat(lowerCodes);

const aliases = new Map();
aliases.set(tokens.Above, ['above', 'over']);
aliases.set(tokens.And, ['and']);
aliases.set(tokens.Around, ['around', 'round']);
aliases.set(tokens.As, ['as']);
aliases.set(tokens.Great, ['great', 'high', 'big', 'strong']);
aliases.set(tokens.Small, ['less', 'low', 'small', 'weak']);
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