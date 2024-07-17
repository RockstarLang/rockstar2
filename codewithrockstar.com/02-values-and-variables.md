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
{% include_relative {{ page.examples }}string_literals.rock %}
```
## Numbers

**Number literals** are written as ordinary digits; decimals and negative numbers are supported:

```rockstar
{% include_relative {{ page.examples }}number_literals.rock %}
```

A Rockstar number is a 128-bit fixed-precision decimal, between -79,228,162,514,264,337,593,543,950,335 and +79,228,162,514,264,337,593,543,950,335.

You get 29 digits, a minus sign if you need it, and a a decimal point you can put anywhere you like:

```rockstar
{% include_relative {{ page.examples }}number_limits.rock %}
```

Numbers with more than 29 digits will be rounded to 29 digits if they have a decimal part:

```rockstar
{% include_relative {{ page.examples }}number_29_digits.rock %}
```
## Booleans
Rockstar supports the Boolean literals `true` (aliases: `yes`, `ok`, `right`) and `false` (aliases: `no`, `wrong`, `lies`).
```rockstar
{% include_relative {{ page.examples }}boolean_literals.rock %}
```
## Null

Rockstar `null` represents an expression which has no meaningful value. Aliases for `null` are `nothing`, `nowhere`, `nobody` and `gone`:

```rockstar
{% include_relative {{ page.examples }}boolean_literals.rock %}
```

## Variables

Rockstar supports three different kinds of variables.

**Simple variables** can be any valid identifier that isn't a reserved keyword. A simple variable name must contain only letters, and cannot contain spaces. Note that Rockstar does not allow numbers or underscores in variable names - remember the golden rule of Rockstar syntax: if you can’t sing it, you can’t have it. Simple variables are case-insensitive.

```rockstar
{% include_relative {{ page.examples }}simple_variables.rock %}
```

**Common variables** consist of one of the keywords `a`, `an`, `the`, `my`, `your` or `our` followed by whitespace and an identifier. The keyword is part of the variable name, so `a boy` is a different variable from `the boy`. Common variables are case-insensitive.

> Common variables can include language keywords, so you can have variables called `your scream`, `my null`, `the silence`. 

```rockstar
{% include_relative {{ page.examples }}common_variables.rock %}
```

**Proper variables** are multi-word proper nouns: words which aren’t language keywords, each starting with an uppercase letter, separated by spaces. (Single-word variables are always simple variables.) Whilst some developers may use this feature to create variables with names like `Customer ID`, `Tax Rate` or `Distance In Kilometres`, we recommend you favour idiomatic variable names such as `Doctor Feelgood`, `Mister Crowley`, `Tom Sawyer`, and `Billie Jean`.
#### A note on case sensitivity in Rockstar

Rockstar keywords and variable names are all case-insensitive, with the exception of proper variables. Proper variables are case-insensitive **apart from the first letter of each word, which must be a capital letter.**

- `TIME`, `time`, `tIMe`, `TIMe` are all equivalent. Simple variables are case-insensitive.
- `MY HEART`, `my heart`, `My Heart` - are all equivalent; the keyword `my` triggers **common variable** behaviour
- `Tom Sawyer`, `TOM SAWYER`, `TOm SAWyer` - are all equivalent; the capital `S` on `Sawyer` triggers **proper variable** behaviour
- `DOCTOR feelgood` is not a valid Rockstar variable; the lowercase `f` on `feelgood` does not match any valid variable naming style and so the variable name is not valid.



