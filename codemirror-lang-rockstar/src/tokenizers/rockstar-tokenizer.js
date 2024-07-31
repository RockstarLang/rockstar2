import { ExternalTokenizer } from "@lezer/lr"
import * as rockstar from "./rockstar-lexer.js"

export const Keywords = new ExternalTokenizer(rockstar.tokenizeKeyword);
export const Variables = new ExternalTokenizer(rockstar.tokenizeVariable);
export const Operators = new ExternalTokenizer(rockstar.tokenizeOperator);
export const PoeticString = new ExternalTokenizer(rockstar.tokenizePoeticString);
export const PoeticNumber = new ExternalTokenizer(rockstar.tokenizePoeticNumber);