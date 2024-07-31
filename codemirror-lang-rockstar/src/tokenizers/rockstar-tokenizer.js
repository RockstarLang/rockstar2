import { ExternalTokenizer } from "@lezer/lr"
import * as rockstar from "./rockstar.js"

export const Keywords = new ExternalTokenizer(rockstar.tokenizeKeyword);
export const PoeticNumber = new ExternalTokenizer(rockstar.tokenizePoeticNumber);
export const PoeticString = new ExternalTokenizer(rockstar.tokenizePoeticString);
export const CommonVariable = new ExternalTokenizer(rockstar.tokenizeCommonVariable);
export const SimpleVariable = new ExternalTokenizer(rockstar.tokenizeSimpleVariable);
export const ProperVariable = new ExternalTokenizer(rockstar.tokenizeProperVariable);
export const Pronoun = new ExternalTokenizer(rockstar.tokenizePronoun);
