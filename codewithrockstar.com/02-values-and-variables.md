---
title: Values and Variables
layout: home
examples: examples/02-values-and-variables/
nav_order: "1002"
---
### Types

Rockstar uses a similar type system to that defined by the [ECMAScript type system](http://www.ecma-international.org/ecma-262/5.1/#sec-8), except `undefined` doesn’t sound very rock’n’roll so we use `mysterious` instead.

* **String** - Rockstar strings are sequences of 16-bit unsigned integer values representing UTF-16 code units. `empty`, `silent`, and `silence` are aliases for the empty string (`""`)
* **Number** - Numbers in Rockstar are double-precision floating point numbers, stored according to the [IEEE 754](https://en.wikipedia.org/wiki/IEEE_754) standard. _(An earlier version of this spec proposed that Rockstar used the [DEC64](http://www.dec64.com/) numeric type. This is a perfect example of something that seemed like a great idea after a couple of beers but turns out to be prohibitively difficult to implement…)_
* **Boolean** - a logical entity having two values `true` and `false`. 
	- `right`, `yes` and `ok` are valid aliases for `true`
	- `wrong`, `no` and `lies` are valid aliases for `false`
- **Function** - used for functions.
- **Null** - the null type. Evaluates as equal to zero and equal to false. The keywords `nothing`, `nowhere`, `nobody`, and `gone` are defined as aliases for `null`
- **Mysterious** - the value of any variable that hasn’t been assigned a value, denoted by the keyword `mysterious`.

## Literals
String literals in Rockstar use double quotes:
```rockstar
{% include_relative {{ page.example }}string_literals.rock %}
```
Number literals are written as deci% mal numbers:




## Variables

Rockstar supports three different kinds of variables.

A **simple variables** is any valid identifier that isn't a reserved keyword. A simple variable name must contain only letters, and cannot contain spaces. Note that Rockstar does not allow numbers or underscores in variable names - remember the golden rule of Rockstar syntax: if you can’t sing it, you can’t have it. Simple variables are case-insensitive.





