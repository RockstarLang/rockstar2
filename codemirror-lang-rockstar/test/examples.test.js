import * as fs from "fs"
import * as path from "path"
import { fileURLToPath } from 'url';
import { parser } from "../src/grammars/rockstar.js";

let caseDir = path.dirname(fileURLToPath(import.meta.url))
let examples = path.join(caseDir, "../../codewithrockstar.com/examples/");
var allFiles = fs.readdirSync(examples, { recursive: true });
var rockFiles = [];
var TEST_COUNT = 2;
var i = 0;
for (var file of allFiles) {
	if (! /\.rock$/.test(file)) continue;
	rockFiles.push(path.join(examples, file));
	if (i++ > TEST_COUNT) break;
}

test.each(rockFiles)("parser parses %p", (file) => {
	//let source = fs.readFileSync(file, "utf8");
	let result = parser.parse(`A man was lying in the street.
A boy was 2 + 4
A girl says she was there the whole time
A child is without fear`);
	console.log(result.toString());
});

