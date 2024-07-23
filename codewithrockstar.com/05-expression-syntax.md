---
title: Expression Lists
layout: home
examples: examples/05-expression-syntax/
nav_order: "1005"
---
### Expression lists

The right-hand side of a binary operation in Rockstar can be an list of primary expressions.

> A **primary expression** in Rockstar is anything which yields a value but does not involve any operators. Primaries are literal strings and numbers, variables, function calls, and language constants. The reason for the restriction is that if you could put arithmetic in expression lists, it becomes impossible to tell where one list ends and the next one begins:
>
> ```rockstar
> Shout 1 + 2, 3 / 4, 5, and 6
> ```
> ...is that `1 + 2 + (3/4) + 5 + 6`? Or is it `1 + 2 + (3 / 4 / 5 / 6)` ?

```rockstar
{% include_relative {{ page.examples }}expression-lists.rock %}
```

### Compound Expressions

Languages like C support shorthand expressions like `x++`, `x += 2`, and so on.

The equivalent in Rockstar looks like this:

```rockstar
{% include_relative {{ page.examples }}compound-expressions.rock %}
```





