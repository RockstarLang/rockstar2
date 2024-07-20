---
title: Binary Logic
layout: home
examples: examples/04-boolean-logic/
nav_order: "1003"
---
Rockstar supports binary logic expressions using the keywords `and`, `or`, `nor`, and `not`
## Truthiness

Every possible value in Rockstar evaluates to either true or false -- this is known as **truthiness**

The only things in Rockstar which are **falsy** are:

* `false`
* `null`
* `mysterious`
* the empty string `""`
* the number `0`

Everything else is truthy, which means if you put in in a Boolean context (such as the condition of an `if` statement) it'll be 'true'.

Rockstar also supports unary negation using the prefixes `not` and `non-`, so you can say a variable is totally non-non-non-NON true (which makes it true, 'cos a quadruple negation cancels itself out.)

```rockstar
{% include_relative {{ page.examples }}basic-logic.rock %}
```



