---
title: Expressions
layout: home
examples: examples/03-expressions/
nav_order: "1003"
---
### Expressions

Rockstar expressions are heavily inspired by JavaScript, in that they will almost always return *something* rather than failing.

```rockstar
{% include_relative {{ page.examples }}lyrical_expressions.rock %}
``` 

### Basic Arithmetic

Rockstar supports the standard infix arithmetic operators `+`, `-`, `*`, `/`, with several aliases for each operator so you can write lyrically pleasing expressions:

| Operation      | Operator | Aliases            |
| -------------- | -------- | ------------------ |
| Addition       | `+`      | `with`, `plus`     |
| Subtraction    | `-`      | `minus`, `without` |
| Multiplication | `*`      | `times`, `of`      |
| Division       | `/`      | `over`, `between`  |

Operator precedence obeys the convention of multiplication, division, addition, subtraction.

> Rockstar doesn't support parentheses in expressions. If the default operator precedence doesn't do what you need, you'll have to decompose your expression into multiple evaluations and assignments.

```rockstar
{% include_relative {{ page.examples }}basic_arithmetic.rock %}
```

Here's how Rockstar operators are defined for various combinations of types.

As a rough rule, operations involving numbers will return numbers. For arithmetic purposes, `true` is `1`, `null` and `false` are `0`. Operations involving strings will generally return strings.

> Any operation involving `mysterious` will always return `mysterious`.

#### Addition
```rockstar
{% include_relative {{ page.examples }}addition_types.rock %}
```

### Subtraction

If both operands  have a numeric value, subtraction is numeric. If either or both operands is a string, Rockstar will "subtract" strings by removing the the bit you're taking away (the *subtrahend*, if you want to get technical) from the bit you're taking it away from (the *minuend*). Most of the time, this has no effect and subtraction just returns the string representation of the left-hand argument, but hey - you're the one trying to subtract strings here; let's not make out like I'm the one who has a problem.

```rockstar
{% include_relative {{ page.examples }}addition_types.rock %}
```
### Multiplication

Rockstar lets you multiply strings. Multiplying by positive integers will repeat the string. Multiply by `-1` to reverse the string, multiplying by decimal fractions will extract substrings. You'll figure it out.

```rockstar
{% include_relative {{ page.examples }}multiplication_types.rock %}
```

### Division

...you know where this is going now. Yes, you can divide strings. A string divided by 2 gives you the first half. A string divided by half will be repeated. A string divided by -1 will reverse.

Dividing anything by a string will tell you how many times the second string occurs in the string representation of the first operand. Dividing `haystack / needle` will be zero if it didn't find `needle` in `haystack`, and positive if it found it.

Or, y'know, you can just stick to dividing numbers.

```rockstar
{% include_relative {{ page.examples }}division_types.rock %}
```

