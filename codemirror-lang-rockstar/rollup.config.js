import typescript from "rollup-plugin-ts"
import { nodeResolve } from "@rollup/plugin-node-resolve"
import { lezer } from "@lezer/generator/rollup"

export default [
	{
		input: "src/editor.mjs",
		//external: id => id != "tslib" && !/^(\.?\/|\w:)/.test(id),
		output: [
			// { file: "dist/editor.cjs", format: "cjs" },
			{ dir: "../codewithrockstar.com/js/codemirror", format: "es" },
			{ dir: "./test/parser", format: "es" }
		],
		plugins: [lezer(), nodeResolve(), typescript()]
	}
]
