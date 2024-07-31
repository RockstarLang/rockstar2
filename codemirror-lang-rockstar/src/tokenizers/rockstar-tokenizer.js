import { ExternalTokenizer } from "@lezer/lr"
import * as lexer from "./rockstar-lexer.js"

export const Keywords = new ExternalTokenizer(lexer.tokenizeKeyword);
export const Variables = new ExternalTokenizer(lexer.tokenizeVariable);
export const Operators = new ExternalTokenizer(lexer.tokenizeOperator);
export const PoeticString = new ExternalTokenizer(lexer.tokenizePoeticString);
export const PoeticNumber = new ExternalTokenizer(lexer.tokenizePoeticNumber);
export const EndMarkers = new ExternalTokenizer(lexer.tokenizeEndMarkers);