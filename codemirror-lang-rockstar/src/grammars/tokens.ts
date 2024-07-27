import { ExternalTokenizer } from "@lezer/lr"
import {
	properVariable
} from "./rockstar.grammar"

export const matchProperVariable = new ExternalTokenizer((input, stack) => {
	console.log(input);
	console.log(input.next);
});
