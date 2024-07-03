import typescript from "rollup-plugin-ts"
import { nodeResolve } from "@rollup/plugin-node-resolve"
import { lezer } from "@lezer/generator/rollup"

export default [
	{
		input: "src/codemirror-lang-rockstar.ts",
		external: id => id != "tslib" && !/^(\.?\/|\w:)/.test(id),
		output: {
			dir: "./dist", format: "es"
		},
		plugins: [lezer(), typescript(), nodeResolve()]
	},
	{
		input: "src/rockstar-editor.mjs",
		output: {
			dir: "./dist", format: "iife"
		},
		plugins: [lezer(), nodeResolve()]
	}
]
