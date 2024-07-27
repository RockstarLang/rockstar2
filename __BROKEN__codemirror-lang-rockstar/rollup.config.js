import typescript from "rollup-plugin-ts"
import { nodeResolve } from "@rollup/plugin-node-resolve"
import { lezer } from "@lezer/generator/rollup"

export default [
	{
		input: "src/grammars/rockstar.grammar",
		output: {
			file: "src/parsers/rockstar-parser.js", format: "es"
		},
		plugins: [lezer(), typescript(), nodeResolve()]
	}, {
		input: "src/codemirror-lang-rockstar.js",
		NOPE_external: id => id != "tslib" && !/^(\.?\/|\w:)/.test(id),
		output: {
			dir: "./dist", format: "es"
		},
		plugins: [lezer(), nodeResolve()]
	},
	{
		input: "src/codemirror-lang-kitchen-sink.js",
		NOPE_external: id => id != "tslib" && !/^(\.?\/|\w:)/.test(id),
		output: {
			dir: "./dist", format: "es"
		},
		plugins: [lezer(), nodeResolve()]
	}, {
		input: "src/rockstar-editor.mjs",
		output: {
			//			dir: "./dist", format: "iife"
			dir: "../codewithrockstar.com/js", format: "es"
		},
		plugins: [lezer(), nodeResolve()]
	}
]
