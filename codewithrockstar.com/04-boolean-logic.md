---
title: Binary Logic
layout: home
examples: examples/04-boolean-logic/
nav_order: "1003"
---
## Truthiness

Every possible value in Rockstar evaluates to either true or false -- this is known as **truthiness**. The only things in Rockstar which are **falsy** are:

* `false`
* `null`
* `mysterious`
* the empty string `""`
* the number `0`

Everything else is truthy, which means if you put in in a Boolean context (such as the condition of an `if` statement) it'll be 'true'.
### Unary Not

Unary not in Rockstar uses the keywords `not` and `non-`:

```rockstar
{% include_relative {{ page.examples }}unary-not.rock %}
```

### Binary Logic

Rockstar supports binary logic expressions using the keywords `and`, `or`,  and `nor`. 

```rockstar
{% include_relative {{ page.examples }}basic-logic.rock %}
```

`not` has the highest precedence, then `and`, then `or`, then `nor`:

```rockstar
{% include_relative {{ page.examples }}operator-precedence.rock %}
```

### Binary Logic for Non-Boolean Types

Binary logic applied to strings and numbers in Rockstar doesn't necessarily return a Boolean result: it returns whichever of the operands resolves the logical constraint of the expression:#

```rockstar
{% include_relative {{ page.examples }}binary-operands.rock %}
```

The `and` and `or` operators in Rockstar will short-circuit:

* `X or Y`: evaluate `X`. If the result is **truthy**, return it, otherwise evaluate `Y` and return that.
	* **If `X` is truthy, `Y` is never evaluated.**
* `X and Y`:  evaluate `X`. If the result is **falsy**, return it, otherwise evaluate `Y` and return that.
	* **If `X` is falsy, `Y` is never evaluated.**

In the following examples, short-circuiting means the division by zero is never evaluated:

```rockstar
{% include_relative {{ page.examples }}short-circuit.rock %}
```

## Equality and Comparison

Equality in Rockstar works as follows:

* If one operand is a Boolean, it's compared to the truthiness of the other argument
* Otherwise, if one operand is a String, the string is compared to the string representation of the other argument
* Otherwise, the values of the two operands are compared











