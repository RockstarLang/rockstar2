---
title: Expressions
layout: home
examples: examples/03-expressions/
nav_order: "1003"
---
### Expressions

Rockstar expressions are heavily inspired by JavaScript, in that they will almost always return *something* rather than failing.

### Basic Arithmetic

Rockstar supports the standard infix arithmetic operators `+`, `-`, `*`, `/`, with several aliases for each operator so you can write lyrically pleasing expressions:

| Operation      | Operator | Aliases            |
| -------------- | -------- | ------------------ |
| Addition       | `+`      | `with`, `plus`     |
| Subtraction    | `-`      | `minus`, `without` |
| Multiplication | `*`      | `times`, `of`      |
| Division       | `/`      | `over`, `between`  |




```rockstar
{  % include_relative {{ page.examples }}poetic_strings.rock %  }
```











