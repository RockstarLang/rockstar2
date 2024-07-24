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

Function arguments must be primary expressions:

```rockstar
{% include_relative {{ page.examples }}function-calls-as-arguments.rock %}
```

> The reason you can't use operators inside function arguments is that it makes it awkward to write recursive function calls. Consider this expression
> 
> ```rockstar
>  {% include_relative {{ page.examples }}fibonacci.rock %}
> Fibonacci takes a number
> If a number is 
> result = foo taking 1 + foo taking 2
> ```
> ...is that `foo(1 + foo(2)`, or `foo(1 + foo(2))`? Without using parentheses to surround function arguments, the parser 
```
> 
> 
```


This is one of the few features where the language **grammar** is ambiguous, and what's produced by the parser doesn't necessarily match what's executed by the interpreter. The parser is greedy and it doesn't know anything about how many arguments a function takes (its *arity*), so this expression:

```rockstar
 FuncA taking FuncB taking 1, 2, FuncB taking 3, 4
 ```

will produce this parse tree:

```
call: FuncA
  args:
  1: call: FuncB:
     args:
     1: number: 1
     2: number: 2
     3: call: FuncB
        args:
        1: number: 3
        2: number: 4
```

That's actually wrong, because it'll try to invoke FuncB with three arguments - so rather than failing, the Rockstar interpreter only evaluates as many function arguments as the function is expecting, and any "leftover" expressions will be passed back to the outer function call, so what actually gets executed is:

```
call: FuncA
  args:
  1: call: FuncB:
     args:
     1: number: 1
     2: number: 2
  2: call: FuncB    }
     args:          } FuncB only expects two arguments, so 
     1: number: 3   } the interpreter passes this one to the 
     2: number: 4   } outermost function instead.
```

Functions can contain other functions, and because every function defines its own variable scope, nested functions can have the same names as the functions which enclose them. *(I have no idea why you would ever want to do this, but making it impossible would have been really difficult.)*

```rockstar
 {% include_relative {{ page.examples }}nested-functions.rock %}
 ```

To declare a function with no arguments, specify it `takes null` (or aliases `nothing`, `nowhere`, `nobody`). To call a function with no arguments, use the `call` keyword, or suffix the function name with an exclamation mark:

```rockstar
 {% include_relative {{ page.examples }}functions-with-no-arguments.rock %}
 ```




