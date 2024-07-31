import { styleTags, tags } from "@lezer/highlight"
import { Ampersand, OxfordComma } from "./rockstar.terms";

export const highlighting = styleTags({
	ProperVariable: tags.variableName,
	CommonVariable: tags.variableName,
	SimpleVariable: tags.variableName,
	Pronoun: tags.variableName,
	Number: tags.number,
	PoeticString: tags.string,
	PoeticNumber: tags.number,
	String: tags.string,
	Comment: tags.comment,
	LineComment: tags.lineComment,
	BlockComment: tags.blockComment,
	ArithmeticOperator: tags.arithmeticOperator,
	LogicOperator: tags.logicOperator,
	CompareOperator: tags.compareOperator,
	VLS: tags.separator,
	XLS: tags.separator,
	ALS: tags.separator,
	Comma: tags.separator,
	Nacton: tags.separator,
	Ampersand: tags.separator,
	OxfordComma: tags.separator,

	Above: tags.compareOperator,
	And: tags.logicOperator,
	Around: tags.keyword,
	As: tags.keyword,
	AsGreat: tags.compareOperator,
	AsSmall: tags.compareOperator,
	At: tags.keyword,
	Back: tags.keyword,
	Be: tags.keyword,
	Break: tags.controlKeyword,
	Build: tags.keyword,
	Call: tags.keyword,
	Cast: tags.keyword,
	Continue: tags.controlKeyword,
	Debug: tags.keyword,
	Divided: tags.arithmeticOperator,
	Down: tags.keyword,
	Else: tags.controlKeyword,
	Empty: tags.literal,
	End: tags.keyword,
	Exactly: tags.compareOperator,
	False: tags.bool,
	//His: tags.keyword,
	If: tags.controlKeyword,
	Into: tags.keyword,
	Is: tags.compareOperator,
	Isnt: tags.compareOperator,
	Join: tags.keyword,
	Knock: tags.keyword,
	Less: tags.compareOperator,
	Let: tags.keyword,
	Like: tags.keyword,
	Listen: tags.keyword,
	Minus: tags.arithmeticOperator,
	More: tags.compareOperator,
	Mysterious: tags.null,
	Non: tags.logicOperator,
	Nor: tags.logicOperator,
	Not: tags.logicOperator,
	Now: tags.keyword,
	Null: tags.null,
	Or: tags.logicOperator,
	Over: tags.arithmeticOperator,
	Plus: tags.arithmeticOperator,
	Pop: tags.keyword,
	Print: tags.keyword,
	// Pronoun: tags.keyword,
	Push: tags.keyword,
	Put: tags.keyword,
	Return: tags.controlKeyword,
	Says: tags.keyword,
	Split: tags.keyword,
	Takes: tags.keyword,
	Taking: tags.keyword,
	Than: tags.compareOperator,
	// The: tags.keyword,
	Then: tags.controlKeyword,
	Times: tags.arithmeticOperator,
	To: tags.keyword,
	True: tags.bool,
	Turn: tags.keyword,
	Under: tags.keyword,
	Until: tags.controlKeyword,
	Up: tags.keyword,
	Using: tags.keyword,
	While: tags.controlKeyword,
	With: tags.keyword,
	Write: tags.keyword

	// DocComment: tags.docComment,
	// Name: tags.name,
	// VariableName: tags.variableName,
	// TypeName: tags.typeName,
	// TagName: tags.tagName,
	// PropertyName: tags.propertyName,
	// AttributeName: tags.attributeName,
	// ClassName: tags.className,
	// LabelName: tags.labelName,
	// Namespace: tags.namespace,
	// MacroName: tags.macroName,
	// Literal: tags.literal,
	// String: tags.string,
	// DocString: tags.docString,
	// Character: tags.character,
	// AttributeValue: tags.attributeValue,
	// Number: tags.number,
	// Integer: tags.integer,
	// Float: tags.float,
	// Bool: tags.bool,
	// Regexp: tags.regexp,
	// Escape: tags.escape,
	// Color: tags.color,
	// Url: tags.url,
	// Keyword: tags.keyword,
	// Self: tags.self,
	// Null: tags.null,
	// Atom: tags.atom,
	// Unit: tags.unit,
	// Modifier: tags.modifier,
	// OperatorKeyword: tags.operatorKeyword,
	// ControlKeyword: tags.controlKeyword,
	// DefinitionKeyword: tags.definitionKeyword,
	// ModuleKeyword: tags.moduleKeyword,
	// Operator: tags.operator,
	// DerefOperator: tags.derefOperator,
	// ArithmeticOperator: tags.arithmeticOperator,
	// LogicOperator: tags.logicOperator,
	// BitwiseOperator: tags.bitwiseOperator,
	// CompareOperator: tags.compareOperator,
	// UpdateOperator: tags.updateOperator,
	// DefinitionOperator: tags.definitionOperator,
	// TypeOperator: tags.typeOperator,
	// ControlOperator: tags.controlOperator,
	// Punctuation: tags.punctuation,
	// Separator: tags.separator,
	// Bracket: tags.bracket,
	// AngleBracket: tags.angleBracket,
	// SquareBracket: tags.squareBracket,
	// Paren: tags.paren,
	// Brace: tags.brace,
	// Content: tags.content,
	// Heading: tags.heading,
	// Heading1: tags.heading1,
	// Heading2: tags.heading2,
	// Heading3: tags.heading3,
	// Heading4: tags.heading4,
	// Heading5: tags.heading5,
	// Heading6: tags.heading6,
	// ContentSeparator: tags.contentSeparator,
	// List: tags.list,
	// Quote: tags.quote,
	// Emphasis: tags.emphasis,
	// Strong: tags.strong,
	// Link: tags.link,
	// Monospace: tags.monospace,
	// Strikethrough: tags.strikethrough,
	// Inserted: tags.inserted,
	// Deleted: tags.deleted,
	// Changed: tags.changed,
	// Invalid: tags.invalid,
	// Meta: tags.meta,
	// DocumentMeta: tags.documentMeta,
	// Annotation: tags.annotation,
	// ProcessingInstruction: tags.processingInstruction,

	// // Tags from here on down are modifiers and must wrap another tag
	// // https://lezer.codemirror.net/docs/ref/#highlight.tags.definition
	// Definition: tags.definition(tags.name),
	// Constant: tags.constant(tags.name),
	// Function: tags.function(tags.name),
	// Standard: tags.standard(tags.name),
	// Local: tags.local(tags.name),
	// Special: tags.special(tags.name)
});