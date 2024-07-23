---
title: Appendix
layout: home
examples: examples/99-appendix/
nav_order: "1099"
---
Lists in Rockstar

See also 
 
Rockstar grammar supports three different kinds of lists:

* Expression lists
* Variable lists
* Primary lists

The most restrictive is the **primary list**, used in compound arithmetic expressions:

```rockstar
{% include_relative {{ page.examples }}primary-lists.rock %}
```

Elements in a primary list must be primary expressions. A **primary expression** in Rockstar is anything which yields a value without using any operators. Primaries are literal strings and numbers, variables, function calls, and language constants.

Elements in a primary list are separated by one of:

* Comma `,`
* Ampersand `&`
* A *nacton*

> **Nacton** *(n.)* The 'n' with which cheap advertising copywriters replace the word 'and' (as in 'fish 'n' chips', 'mix 'n' match', 'assault 'n' battery'), in the mistaken belief that this is in some way chummy or endearing.
> 
> 	- "The Meaning of Liff", Douglas Adams & John Lloyd

Rockstar supports both the **UK nacton** `'n'` (as in *fish 'n' chips*) and the **US nacton** `n'` (as in *Guns n' Roses*.)

Next, there are **expression lists**, used to pass arguments to functions. An expression list supports the same separators as the primary list, but also supports the **Oxford comma** separator `, and `

Because you can't put an Oxford comma in a primary list, this means an expression list can contain expressions that themselves contain primary lists; the Oxford comma provides an unambiguous way to separate the sub-expressions:

```rockstar
{% include_relative {{ page.examples }}expression-lists.rock %}
```

Finally, there are **variable lists**, used to specify the arguments when defining a function.

Because the elements in the list can **only** be variable names, a variable list supports all the separators used in primary and expression lists, and also the bareword ` and ` (with no commas or punctuation required).

```rockstar
{% include_relative {{ page.examples }}variable-lists.rock %}
```
