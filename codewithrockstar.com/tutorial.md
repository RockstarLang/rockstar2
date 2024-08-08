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

...OK, where did `153231525` come from? 

Welcome to one of Rockstar's most unusual features: **poetic literals**.

When you initialise a variable using `is`, `was` or `were`, if the thing on the right-hand side doesn't start with a digit `0-9`, or with one of the arithmetic operator keywords (`plus`, `with`,  etc.) Rockstar treats it as a **poetic number**. It'll take the length of each word, modulo 10, and interpret those word lengths as decimal digits.

So `Ricky was a young boy, he had a heart of stone`, gives us:

```
a young boy, he had a heart of stone
1   5    3    2  3  1   5   2    5
```

A poetic number includes everything up to the end of the line, so watch out for statements like `Lucy was a dancer. Say Lucy!` - that's not going to print `Lucy`, it's going to assign `Lucy` the value 1634. Poetic numbers count hyphens (`-`) and ignore all other punctuation, so you can use phrases like `cold-hearted` for the digit `2` instead of having to think of 12-letter words.

If you want to use a poetic number anywhere else in your Rockstar program, prefix it with the `like` keyword: `Let my variable be like a rolling stone` will initialise `my variable` with the value `175`.
## Viewing the Parse Tree
Features like poetic numbers can make it hard to figure out exactly what a Rockstar program is doing, so the Rockstar engine that runs on this website also allows you to see the **parse tree** - an abstract representation of the structure of your Rockstar program. Try clicking "Parse <i class="fa-solid fa-list-tree"></i>" here and see what you get:

{% rockstar_include parse-trees.rock play,parse %}
## Strings

No, not that kind of strings. Strings are how Rockstar handles text. A string in Rockstar is surrounded by double quotes; to include double quotes in a string, use two sets of double quotes. You can also use **poetic string** syntax using the `says` keyword:

{% rockstar_include strings.rock play,parse %}
## Rocking Strings

The problem with literal strings is they often don't fit the mood of the song you're trying to write. `FizzBuzz` is all well & good, but shouting the word "fizz" in the middle of power ballad just isn't gonna work.

To get around this, Rockstar includes a feature that lets you build strings without ever having to refer to them directly. Using the `rock` keyword, you can add characters to the end of a string by specifying their ASCII/Unicode character codes. The `with` keyword is optional, and using the `like` keyword, you can build strings using poetic numbers corresponding to their character codes:

{% rockstar_include rocking-strings.rock play,parse %}
## Conditionals and Loops
Conditionals in Rockstar use the `if` keyword, alias `when`, and the `else` / `otherwise` keywords. Loops begin with `while` or `until`. 
 
{% rockstar_include if-else.rock play,parse %}

Multi-line conditionals and loops have to end with an **end of block**. In previous versions of Rockstar, this had to be a blank line. Rockstar 2 adds an explicit `end` keyword, along with the aliases `yeah` and `baby`.
### Oh, ooh, ooooh yeah, baby

You can also end a Rockstar block with the keyword `oh`. `Ooh` ends **two** blocks, `oooh` ends three blocks, and so on until you get bored or your computer runs out of memory. Think of this like the Rockstar equivalent of `}}}}` in C-style languages, or the `)))))` that ends most Lisp programs.

{% rockstar_include if-else-baby.rock play,parse,reset %}
## Pronouns

Oh, yes. You bet we have pronouns.[^2] In natural languages, a pronoun is just a way to refer to something based on context, instead of explicitly having to name things every time - it's the difference between "Tommy put his guitar in the back of his car, he drove out into the night" and "Tommy put Tommy's guitar in the back of Tommy's car, Tommy drove out into the night".

Rockstar supports the pronouns `it`, `he`, `she`, `him`, `her`, `they`, `them`, and a whole lot more - see the docs for the full list.

A Rockstar pronoun refers to the last variable which was assigned, or the last variable that appeared as the left-hand side of the test in a conditional or loop statement. That sounds complicated, but it's not: most of the time, you can just use `it`, `him` or `her` in your programs as you would in regular English and it'll probably work.

{% rockstar_include pronouns.rock play,parse,reset %}



[^1]: Technically `let` will declare a new variable in local scope, where `put` and `is` will declare or assign a global variable. It's complicated. See the documentation on variable scope if you really care.
[^2]: Rockstar is also woke, fetch, rizz, *and* skibidi, no cap -- but that's not why it has pronouns. 