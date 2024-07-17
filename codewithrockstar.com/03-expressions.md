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

Operator precedence obeys the convention of multiplication, division, addition, subtraction.

> Rockstar doesn't support parentheses in expressions. If the default operator precedence doesn't do what you need, you'll have to decompose your expression into multiple evaluations and assignments.

```rockstar
{% include_relative {{ page.examples }}basic_arithmetic.rock %}
```

Here's how Rockstar operators are defined for various combinations of types.

> Any operation involving `mysterious` will always return `mysterious`.

#### Addition

| LHS     | RHS     | Result | Example                                                                              |
| ------- | ------- | ------ | ------------------------------------------------------------------------------------ |
| Number  | Number  | Number | `1 + 2 = 3`                                                                          |
| Number  | String  | String | `1 + "2" = "12"`                                                                     |
| Number  | Boolean | Number | `1 + true = 2`<br>`1 + false = 1`                                                    |
| Number  | Null    | Number | `1 + null = 1`                                                                       |
| String  | Number  | String | `"2" + 1 = "21"`                                                                     |
| String  | String  | String | `"hello" + "world" = "helloworld"`                                                   |
| String  | Boolean | String | `"hello" + true = "hellotrue"`                                                       |
| String  | Null    | String | `"hello" + null = "hellonull"`                                                       |
| Boolean | Boolean | Number | `true + true = 2`<br>`true + false = 1`<br>`false + true = 2`<br>`false + false = 0` |
| Boolean | Number  | Number | `true + 1 = 2`<br>`false + 1 = 1`                                                    |
| Boolean | String  | String | `true + "hello" = "truehello"`<br>`false + "world" = "falseworld"`                   |
| Boolean | Null    | Number | `true + null = 1`<br>`false + null = 0`                                              |
| Null    | Null    | Number | `null + null = 0`                                                                    |
| Null    | Boolean | Number | `null + true = 1`<br>`null + false = 0`                                              |
| Null    | String  | String | `null + "hello" = "nullhello"`                                                       |
| Null    | Number  | Number | `null + 1 = 1`                                                                       |
#### Multiplication

Multiplying numbers does normal math stuff: `true` is treated as 1; `false` and `null` are treated as zero. Rockstar can multiply strings. 

| LHS     | RHS     | Result | Example                                                                                                                      |
| ------- | ------- | ------ | ---------------------------------------------------------------------------------------------------------------------------- |
| Number  | Number  | Number | `2 * 3 = 6`                                                                                                                  |
| Number  | String  | String | `3 * "foo" = "foofoofoo"`                                                                                                    |
| Number  | Boolean | Number | `5 * true = 1`<br>`5 * false = 0`                                                                                            |
| Number  | Null    | Number | `5 * null = 0`                                                                                                               |
| String  | Number  | String | `"foo" * 3 = "foofoofoo"`<br>`"foo" * 0 = ""`<br>`"foo" * -1 = "oof"`<br>`"foo" * -2 = "oofoof"`<br>`"foo" * 2.5 = "foofoo"` |
| String  | String  | String | `"hello" * "world" = NaN`                                                                                                    |
| String  | Boolean | String | `"hello" * true = "hello"`<br>`"hello" * false = ""`                                                                         |
| String  | Null    | String | `"hello" + null = ""`                                                                                                        |
| Boolean | Boolean | Number | `true * true = 1`<br>`true * false = 0`<br>`false * true = 0`<br>`false * false = 0`                                         |
| Boolean | Number  | Number | `true * 1 = 1`<br>`false * 1 = 0`                                                                                            |
| Boolean | String  | String | `true * "hello" = "hello"`<br>`false * "world" = ""`                                                                         |
| Boolean | Null    | Number | `true * null = 0`<br>`false * null = 0`                                                                                      |
| Null    | Null    | Number | `null * null = 0`                                                                                                            |
| Null    | Boolean | Number | `null * true = 0`<br>`null * false = 0`                                                                                      |
| Null    | String  | String | `null * "hello" = ""`                                                                                                        |
| Null    | Number  | Number | `null * 1 = 0`                                                                                                               |

Division








