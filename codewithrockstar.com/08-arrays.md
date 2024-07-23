---
title: Arrays and Collections
layout: home
examples: examples/08-arrays/
nav_order: "1008"
---
## Arrays

Rockstar supports JavaScript-style arrays. Arrays are zero-based, and dynamically allocated when values are assigned using numeric indexes. Array elements are initialised to `null`; passing an out-of-range index returns `mysterious`:

```$rockstar
{% include_relative {{ page.examples }}basic-arrays-with-numeric-indices %}
```

> Array indexers can be primary values or arithmetic expressions, but **you can't use a logical expression as an array indexer.**
> 
> Consider `My array at 2 is 4`
> 
> If not for this restriction, the parser would consume `2 is 4` as a comparison expression, return `false`, try to set `My array at false` and then blow up 'cos there's nothing to put in it.


Returning an array in a scalar context will return the current length of the array:

```$rockstar
{% include_relative {{ page.examples }}array-length-as-scalar.rock %}
```

Rockstar also supports non-numeric array keys, so it is valid to say:

```
let my array at "some_key" be "some_value"
Shout my array at "some_key"
```

You can mix string and numeric keys within the same array. The array length property ignores any non-numeric keys:

```
Let my array at "some_key" be "some_value"
Shout my array (will print 0, since there are no numeric indexes)
Let my array at 7 be "some other value"
Shout my array (will now print 8, since assigning my array at 7 modifies the array length)
```

You can also use array index syntax to read (but not write) specific characters from a string

```$
Let my string be "abcdefg"
Shout my string at 0 (will print "a")
Shout my string at 1 (will print "b")
Let the character be my string at 2
```

### Queue operations

Rockstar arrays can also be created and manipulated by the queue operations `rock` and `roll`. (The aliases `push` and `pop` are supported for Rockstar developers who are into 80s dance music.)

#### Pushing elements onto an array

To create a new empty array, `rock` the name of the array:

```
Rock the array (the array is now [])
```

To push an element onto the end of an array:

```
Rock the array with the element
```

This supports list expressions, so you can push multiple elements onto the end of an array:

```
Rock ints with 1, 2, 3 (ints is now [1, 2, 3])
Rock the array with the first, the second, and the third
```

Remember the `with` keyword is context-sensitive, so in this example:

```
Rock ints with 1, 2 with 3, 4, 5
          ^         ^
          |         +-- this 'with' is the binary addition operator
          |
          +------------ this 'with' is part of the array push syntax
          
(ints is now [ 1, 5, 4, 5 ])
```

Rockstar supports a special syntax for pushing poetic literals onto a queue:

```rockstar
Rock the array like the poetic literal (the array is now [ 367 ])
Rock the array like a wolf (the array is now [ 367, 14 ])
```

This syntax is very useful for initialising strings without using string literals - see below. It also means that the following line is valid Rockstar:

```
Rock you like a hurricane (you is now [ 19 ])
```

#### Popping elements from an array

The `roll` keyword will remove the first element from an array and return the element that was removed.

```
Rock ints with 1, 2, 3
Roll ints (returns 1; ints is now [ 2, 3 ])
Roll ints (returns 2; ints is now [ 3 ])
Roll ints (returns 3; ints is now [] )
Roll ints (returns mysterious; ints is now [])
```

`roll` can be used in assignments:

```
Rock ints with 1, 2, 3
Let the first be roll ints
Let the second be roll ints
Let the third be roll ints
Shout the first (outputs 1)
Shout the second (outputs 2)
Shout the third (outputs 3)
```

Rockstar also supports a special `roll x into y` syntax for removing the first element from an array and assigning it to a variable:

```
Rock the list with 4, 5, 6
Roll the list into foo
Roll the list into bar
Roll the list into baz
Shout foo (will output 4)
Shout bar (will output 5)
Shout baz (will output 6)
```