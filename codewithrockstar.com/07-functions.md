---
title: Functions
layout: home
examples: examples/07-functions/
nav_order: "1007"
---
Functions are declared with a variable name followed by the `takes` keyword (alias `wants`) and a list of arguments separated by one of the following: `and` `,` `&` `, and` `'n'`

```rockstar 
{% include_relative {{ page.examples }}functions.rock %}
```

Functions can be one-liners, usually written with the `giving` keyword:

```rockstar 
{% include_relative {{ page.examples }}one-line-functions.rock %}
```

Function bodies can also be a block. Functions in Rockstar specified by the `return` keyword and its aliases `giving`, `give`, `give back` and `send`. A return statement can be followed by the keyword `back` (which has no effect but can make code more lyrical).

```rockstar
{% include_relative {{ page.examples }}polly-wants-a-cracker.rock %}
```

Functions are called using the ‘taking’ keyword and must have at least one argument. Multiple arguments are separated with one of the following: `,` `&` `, and` `'n'`.

Function arguments can be any kind of expression, including other function calls:

- `Multiply taking 3, 5` is an expression returning (presumably) 15
- `Search taking "hands", "lay your hands on me"`
- `Put Multiply taking 3, 5, and 9 into Large` will set Large to `3 * 5 * 9` **NOT** `(3 * 5) && 9`.

