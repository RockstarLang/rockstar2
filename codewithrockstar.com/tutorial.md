---
title: Tutorial
layout: main
---
# Getting Started with Rockstar

Welcome! In this tutorial, you'll learn the basics of Rockstar, and find out what makes Rockstar one of the world's most delightfully pointless programming languages.
## Hello, World

"Hello, World" in Rockstar looks like this:

{% rockstar_include print-hello-world.rock play %}

All the examples in this tutorial are interactive: press the "Rock <i class="fa-solid fa-play"></i>" button to try it out and see what it does.

Because Rockstar's designed to write programs that look like song lyrics, it's very relaxed when it comes to syntax. Almost everything in Rockstar is case-insensitive, and most keywords have several different `aliases` so you can pick the one that suits the mood of your program.

{% rockstar_include hello-world-aliases.rock play %}

## Variables in Rockstar

A variable stores a value so you can refer back to it later. In most programming languages, a variable name can't contain spaces, so programmers have to use names like `customerTaxPayable` or `year_end_date`, or `customer-shipping-address`.

Rockstar is not like most programming languages: if you want to put spaces in your variables, you go right ahead. After all, nobody ever wrote a power ballad about `customerTaxPayable`.
### Common Variables

Common variables in Rockstar start with `a`, `the`, `my`, `your`, `their`, `his` or `her`. To assign a value to a variable, use `put`, `is`, or `let`[^1]

{% rockstar_include common-variables.rock play %}

You might also notice we've got three statements on the same line there. That's just fine, because each statement ends with full stop - just like regular English. Statements in Rockstar can end with a full stop `.`, question mark `?`, exclamation mark `!`, semi-colon `;`, or a line break.
### Proper Variables

Proper variables in Rockstar are two or more words which must all begin with a capital letter. Initials are allowed -- `Johnny B. Goode` -- but abbreviations aren't: you can't have a variable called `Mr. Crowley` because Rockstar treats the `.` in `Mr.` as the end of a statement:

{% rockstar_include proper-variables.rock play %}

Simple Variables

If you *really* want to, you can use **simple variables**, which work just like variables in Python, Ruby, and many other programming languages:

{% rockstar_include simple-variables.rock play %}
## Types and Expressions

Rockstar supports numbers and basic arithmetic expressions:

{% rockstar_include basic-arithmetic.rock play %}

Problem is, there's only [one good song about mathematics](https://open.spotify.com/track/4e5XPD3qh9miordZiBf5jp?si=9e5ea3937ca8462d) and Little Boots already wrote it, so all the arithmetic operators in Rockstar support aliases. Instead of `+`, use `plus` or `with`. `-` can be `minus` or `without`, `*` can be `times` or `of`, and `/` is `over` or `divided by`:

{% rockstar_include lyrical-arithmetic-1.rock play %}

This is also probably a good time to mention that you can't use brackets in Rockstar. Well, you can, but they're used to indicate comments (like this) - because that's how lyrics work.

{% rockstar_include lyrical-arithmetic-2.rock play %}

## Poetic Numbers

You notice in the last example, I wrote `Let Tommy be a boy with a dream` - and not `Tommy is a boy with a dream`?

Try this:

{% rockstar_include ricky-was-a-young-boy.rock play %}

...OK, where the heck did `153231525` come from? Welcome to one of Rockstar's most unusual features: **poetic literals**.

When you initialise a variable using `is`, `was` or `were`, if the thing on the right-hand side doesn't start with a digit `0-9`, Rockstar treats it as a **poetic number**. It'll take the length of each word, modulo 10, and treat those as decimal digits.

So `Ricky was a young boy, he had a heart of stone`, gives us:

`a` => `1`, `young` => `5`, `boy` => `3`

A poetic number includes everything up to the end of the line, including punctuation.

[^1]: Technically `let` will declare a new variable in local scope, where `put` and `is` will declare or assign a global variable. It's complicated. See the documentation on variable scope if you really care.