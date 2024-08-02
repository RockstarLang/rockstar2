---
title: Values and Variables
layout: home
examples: examples/02-values-and-variables/
nav_order: "1002"
---
### Types

Rockstar uses a similar type system to that defined by the [ECMAScript type system](http://www.ecma-international.org/ecma-262/5.1/#sec-8), except `undefined` doesn’t sound very rock’n’roll so we use `mysterious` instead.

* **Number** - Numbers in Rockstar are fixed-precision , stored according to the [IEEE 754](https://en.wikipedia.org/wiki/IEEE_754) standard. _(An earlier version of this spec proposed that Rockstar used the [DEC64](http://www.dec64.com/) numeric type. This is a perfect example of something that seemed like a great idea after a couple of beers but turns out to be prohibitively difficult to implement…)_
* **Boolean** - a logical entity having two values `true` and `false`.
	- `right`, `yes` and `ok` are valid aliases for `true`
	- `wrong`, `no` and `lies` are valid aliases for `false`
- **Function** - used for functions.
- **Null** - the null type. Evaluates as equal to zero and equal to false. The keywords `nothing`, `nowhere`, `nobody`, and `gone` are defined as aliases for `null`
- **Mysterious** - the value of any variable that hasn’t been assigned a value, denoted by the keyword `mysterious`.

## Strings

Rockstar strings are surrounded by double quotes. A string literal includes everything up to the closing quote, including newlines. To include a double quote in a string, use a pair of double quotes. Rockstar strings are stored internally as UTF-16, and support the full Unicode character set.

The keywords `empty`, `silent`, and `silence` are aliases for the empty string (`""`)

```rockstar
{% include_relative {{ page.examples }}string-literals.rock %}
```
## Numbers

**Number literals** are written as ordinary digits; decimals and negative numbers are supported:

```rockstar
{% include_relative {{ page.examples }}number-literals.rock %}
```

A Rockstar number is a 128-bit fixed-precision decimal, between -79,228,162,514,264,337,593,543,950,335 and +79,228,162,514,264,337,593,543,950,335.

You get 29 digits, a minus sign if you need it, and a a decimal point you can put anywhere you like:

```rockstar
{% include_relative {{ page.examples }}number-limits.rock %}
```

Numbers with more than 29 digits will be rounded to 29 digits if they have a decimal part:

```rockstar
{% include_relative {{ page.examples }}number-29-digits.rock %}
```
## Booleans
Rockstar supports the Boolean literals `true` (aliases: `yes`, `ok`, `right`) and `false` (aliases: `no`, `wrong`, `lies`).
```rockstar
{% include_relative {{ page.examples }}boolean-literals.rock %}
```
## Null

Rockstar `null` represents an expression which has no meaningful value. Aliases for `null` are `nothing`, `nowhere`, `nobody` and `gone`:

```rockstar
{% include_relative {{ page.examples }}boolean-literals.rock %}
```

## Variables and Assignment

Rockstar variables are dynamically typed. There are three different ways to assign a variable in Rockstar.

1. `<variable> is <expression>`.  Valid aliases for `is` are `are`, `am`, `was`, `were`, and the contractions `'s` and `'re`
2. `put <expression> into <variable>`
3. `let <variable> be <expression>`

```rockstar
{% include_relative {{ page.examples }}assignment.rock %}
```

Rockstar variables are function scoped - see variable scope in the section on functions for more about how this work. 
### Variable names

Rockstar supports three different kinds of variable names.

**Simple variables** can be any valid identifier that isn't a reserved keyword. A simple variable name must contain only letters, and cannot contain spaces. Note that Rockstar does not allow numbers or underscores in variable names - remember the golden rule of Rockstar syntax: if you can’t sing it, you can’t have it. Simple variables are case-insensitive.

```rockstar
{% include_relative {{ page.examples }}simple-variables.rock %}
```

**Common variables** consist of one of the keywords `a`, `an`, `the`, `my`, `your` or `our` followed by whitespace and an identifier. The keyword is part of the variable name, so `a boy` is a different variable from `the boy`. Common variables are case-insensitive.

> Common variables can include language keywords, so you can have variables called `your scream`, `my null`, `the silence`.

```rockstar
{% include_relative {{ page.examples }}common-variables.rock %}
```

**Proper variables** are multi-word proper nouns: words which aren’t language keywords, each starting with an uppercase letter, separated by spaces. (Single-word variables are always simple variables.) Whilst some developers may use this feature to create variables with names like `Customer ID`, `Tax Rate` or `Distance In Kilometres`, we recommend you favour idiomatic variable names such as `Doctor Feelgood`, `Mister Crowley`, `Tom Sawyer`, and `Billie Jean`.
#### A note on case sensitivity in Rockstar

Rockstar keywords and variable names are all case-insensitive, with the exception of proper variables. Proper variables are case-insensitive **apart from the first letter of each word, which must be a capital letter.**

- `TIME`, `time`, `tIMe`, `TIMe` are all equivalent. Simple variables are case-insensitive.
- `MY HEART`, `my heart`, `My Heart` - are all equivalent; the keyword `my` triggers **common variable** behaviour
- `Tom Sawyer`, `TOM SAWYER`, `TOm SAWyer` - are all equivalent; the capital `S` on `Sawyer` triggers **proper variable** behaviour
- `DOCTOR feelgood` is not a valid Rockstar variable; the lowercase `f` on `feelgood` does not match any valid variable naming style and so the variable name is not valid.

## Pronouns

As well as referring to variables by name, you can refer to them using pronouns. The keywords `it`, `he`, `she`, `him`, `her`, `they`, `them`, `ze`, `hir`, `zie`, `zir`, `xe`, `xem`, `ve`, and `ver` refer to the current **pronoun subject**.

The pronoun subject is updated when:

* A variable is declared or assigned:
  
   `My heart is true. Say it` - `it` here refers to `my heart`
* A variable is the left-hand side of a comparison used as the condition in an `if`, `while` or `until` statement
  
   `If my heart is true, give it back, yeah` - `it` refers to `my heart`

```rockstar
{% include_relative {{ page.examples }}pronouns.rock %}
```

> (Please don’t file issues pointing out that 80s rockers were a bunch of misogynists and gender-inclusive pronouns aren’t really idiomatic. You’re right, we know, and we’ve all learned a lot since then. Besides, [_Look What The Cat Dragged In_](https://en.wikipedia.org/wiki/Look_What_the_Cat_Dragged_In) was recorded by four cishet guys who spent more money on lipgloss and hairspray than they did on studio time, and it’s an absolute classic.)

### The Thing About "Her"

`her` is where Rockstar runs smack into one of the English language's most delightful idiosyncrasies, because the feminine third person pronoun and the feminine possessive are the **same word.**

> Give him his guitar.
> Give them their horns.
> Give her her bass

There is therefore a very specific restriction in the Rockstar grammar: you can't use `her` as a common variable prefix if the second part of the variable is a keyword.

You can have variables called `the times`, `your lies`, `my right`, even though `times`, `lies` and `right` are language keywords, but you can't have `her times` or `her lies` because they'd create ambiguous expressions:

```
A girl is 123
Her times are trying
Say her
Say her times 456

```



## Poetic Literals

One of Rockstar's unique features is the ability to initialise variables using song lyrics.
### Poetic Numbers

A poetic number begins with the `like` or `so` keyword, followed by a series of words. The Rockstar parser takes the length of each word and interprets it as a decimal digit:

```rockstar
{% include_relative {{ page.examples }}poetic-numbers.rock %}
```

Words of 10 or more letters are counted modulo 10, so you can use 10-letter words for `0`, 11 letters for `1` and 12 letters for `2`. Hyphens `-` are counted as letters, so `demon-haunted` is treated as a 12-letter word. Apostrophes are **not** counted, so `nothing` counts as 7 but `nothin'` counts as 6. A poetic number counts every word until the end of the current statement (indicated by a newline or punctuation `.!?;`) If you need a poetic number with a decimal point, use an ellipsis `...`  or the Unicode equivalent U+2026 `…` as the decimal.

```rockstar
{% include_relative {{ page.examples }}poetic-numbers-2.rock %}
```
### Poetic strings
You can initialise string variables without quotes by using the `says` or `said` keyword. This will skip exactly one space character and then capture the rest of the line as a literal string. If the character immediately following the `says` keyword is not a space, it will be included in the string literal.

```rockstar
{% include_relative {{ page.examples }}poetic-strings.rock %}
```











