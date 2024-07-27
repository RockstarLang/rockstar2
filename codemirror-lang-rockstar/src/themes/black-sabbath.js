import { tags as t } from '@lezer/highlight';
import createTheme from '../create-theme.js';
export const blackSabbath = createTheme({
	variant: 'dark',
	settings: {
		background: '#000000',
		foreground: '#ffffff',
		caret: '#ff00ff',
		selection: '#0000ff',
		gutterBackground: '#006666',
		gutterForeground: 'rgb(50, 90, 150)',
		lineHighlight: '#ffff00',
	},
	styles: makeStyles()
	// [
	// 	{ tag: t.comment, color: '#000000' }, { tag: t.lineComment, color: '#200000' }, { tag: t.blockComment, color: '#400000' }, { tag: t.docComment, color: '#600000' }, { tag: t.name, color: '#800000' }, { tag: t.variableName, color: '#a00000' }, { tag: t.typeName, color: '#c00000' }, { tag: t.tagName, color: '#e00000' }, { tag: t.propertyName, color: '#000020' }, { tag: t.attributeName, color: '#200020' }, { tag: t.className, color: '#400020' }, { tag: t.labelName, color: '#600020' }, { tag: t.namespace, color: '#800020' }, { tag: t.macroName, color: '#a00020' }, { tag: t.literal, color: '#c00020' }, { tag: t.string, color: '#e00020' }, { tag: t.docString, color: '#000040' }, { tag: t.character, color: '#200040' }, { tag: t.attributeValue, color: '#400040' }, { tag: t.number, color: '#600040' }, { tag: t.integer, color: '#800040' }, { tag: t.float, color: '#a00040' }, { tag: t.bool, color: '#c00040' }, { tag: t.regexp, color: '#e00040' }, { tag: t.escape, color: '#000060' }, { tag: t.color, color: '#200060' }, { tag: t.url, color: '#400060' }, { tag: t.keyword, color: '#600060' }, { tag: t.self, color: '#800060' }, { tag: t.null, color: '#a00060' }, { tag: t.atom, color: '#c00060' }, { tag: t.unit, color: '#e00060' }, { tag: t.modifier, color: '#000080' }, { tag: t.operatorKeyword, color: '#200080' }, { tag: t.controlKeyword, color: '#400080' }, { tag: t.definitionKeyword, color: '#600080' }, { tag: t.moduleKeyword, color: '#800080' }, { tag: t.operator, color: '#a00080' }, { tag: t.derefOperator, color: '#c00080' }, { tag: t.arithmeticOperator, color: '#e00080' }, { tag: t.logicOperator, color: '#0000a0' }, { tag: t.bitwiseOperator, color: '#2000a0' }, { tag: t.compareOperator, color: '#4000a0' }, { tag: t.updateOperator, color: '#6000a0' }, { tag: t.definitionOperator, color: '#8000a0' }, { tag: t.typeOperator, color: '#a000a0' }, { tag: t.controlOperator, color: '#c000a0' }, { tag: t.punctuation, color: '#e000a0' }, { tag: t.separator, color: '#0000c0' }, { tag: t.bracket, color: '#2000c0' }, { tag: t.angleBracket, color: '#4000c0' }, { tag: t.squareBracket, color: '#6000c0' }, { tag: t.paren, color: '#8000c0' }, { tag: t.brace, color: '#a000c0' }, { tag: t.content, color: '#c000c0' }, { tag: t.heading, color: '#e000c0' }, { tag: t.heading1, color: '#0000e0' }, { tag: t.heading2, color: '#2000e0' }, { tag: t.heading3, color: '#4000e0' }, { tag: t.heading4, color: '#6000e0' }, { tag: t.heading5, color: '#8000e0' }, { tag: t.heading6, color: '#a000e0' }, { tag: t.contentSeparator, color: '#c000e0' }, { tag: t.list, color: '#e000e0' }, { tag: t.quote, color: '#002000' }, { tag: t.emphasis, color: '#202000' }, { tag: t.strong, color: '#402000' }, { tag: t.link, color: '#602000' }, { tag: t.monospace, color: '#802000' }, { tag: t.strikethrough, color: '#a02000' }, { tag: t.inserted, color: '#c02000' }, { tag: t.deleted, color: '#e02000' }, { tag: t.changed, color: '#002020' }, { tag: t.invalid, color: '#202020' }, { tag: t.meta, color: '#402020' }, { tag: t.documentMeta, color: '#602020' }, { tag: t.annotation, color: '#802020' }, { tag: t.processingInstruction, color: '#a02020' },
	// ],
});

function makeStyles() {

	var tags = [t.comment, t.lineComment, t.blockComment, t.docComment, t.name, t.variableName, t.typeName, t.tagName, t.propertyName,
	t.attributeName, t.className, t.labelName, t.namespace, t.macroName, t.literal, t.string, t.docString, t.character, t.attributeValue,
	t.number, t.integer, t.float, t.bool, t.regexp, t.escape, t.color, t.url, t.keyword, t.self, t.null, t.atom, t.unit, t.modifier,
	t.operatorKeyword, t.controlKeyword, t.definitionKeyword, t.moduleKeyword, t.operator, t.derefOperator, t.arithmeticOperator,
	t.logicOperator, t.bitwiseOperator, t.compareOperator, t.updateOperator, t.definitionOperator, t.typeOperator, t.controlOperator,
	t.punctuation, t.separator, t.bracket, t.angleBracket, t.squareBracket, t.paren, t.brace, t.content, t.heading,
	t.heading1, t.heading2, t.heading3, t.heading4, t.heading5, t.heading6, t.contentSeparator, t.list, t.quote, t.emphasis, t.strong,
	t.link, t.monospace, t.strikethrough, t.inserted, t.deleted, t.changed, t.invalid, t.meta, t.documentMeta, t.annotation, t.processingInstruction];

	const ZERO = 121;
	const BUMP = 17;
	var red = ZERO;
	var green = ZERO;
	var blue = ZERO;
	var result = [];
	for (var i = 0; i < tags.length; i++) {
		var random = (red <= blue && green <= blue ? 0 : green <= red && green <= blue ? 1 : 2); // Math.floor(Math.random() * 3);
		var rgb =
			(random == 0 ? "00" : (red + 256).toString(16).substring(1, 3))
			+
			(random == 1 ? "00" : (green + 256).toString(16).substring(1, 3))
			+
			(random == 2 ? "00" : (blue + 256).toString(16).substring(1, 3));
		result.push({ tag: tags[i], color: `#${rgb}` })
		red += BUMP;
		if (red > 255) {
			red = ZERO;
			green += BUMP;
			if (green > 255) {
				green = ZERO;
				blue += BUMP;
				if (blue > 255) blue = ZERO;
			}
		}
	}
	return result;
}
