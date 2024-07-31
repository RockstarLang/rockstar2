import { ExternalTokenizer } from "@lezer/lr"
import * as rockstar from "./rockstar.js"

export const Keywords = new ExternalTokenizer(rockstar.tokenizeKeyword);
export const PoeticLiterals = new ExternalTokenizer(rockstar.poeticLiterals);
export const Variables = new ExternalTokenizer(rockstar.variables);
