import * as tokens from "../grammars/rockstar.terms.js"
import { ASCII } from './ascii.js';

const compareOperators = [">=", "<=", ">", "<", "="];
const arithmeticOperators = ["+", "/", "*", "-"];

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
					console.log(lexeme);
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

function isKeyword(codes) {
	var lexeme = String.fromCodePoint(...codes).toLowerCase();
	return keywords.includes(lexeme);
}

const whitespace = " \t";
const whitespaceCodes = stringToCharCodeArray(whitespace);

const readNextWordIncludingOperators = (input) => readWhileContains(input, alphaCodes.concat(opCodes));
const readNextWordIncludingApostrophes = (input) => readWhileContains(input, [ASCII.Apostrophe].concat(alphaCodes));

const readNextWord = (input) => readWhileContains(input, alphaCodes);

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