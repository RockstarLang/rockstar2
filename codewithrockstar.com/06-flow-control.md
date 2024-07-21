---
title: Flow Control
layout: home
examples: examples/06-flow-control/
nav_order: "1006"
---
## Conditionals: If / Else

Consider this instruction:

> Go to the store. If they have oranges, get a dozen, then get some bagels.

If the store doesn't have oranges, should you still get bagels?

C-style languages resolve this using braces and block syntax:

```c
if (they have oranges) {
	get a dozen
}
get some bagels
```
compared with:
```c
if (they have oranges) {
	get a dozen
	get some bagels
}
```

Rockstar doesn't have curly braces, because you can't sing curly braces, so Rockstar has to use some syntactic tricks to resolve these kinds of ambiguities.

If statements can be one-liners, or conditional blocks.
### One-line if

One-line if statements don't create any block scope. However many `if` statements you stack on the same line, the final statement on the line either runs or it doesn't, and then you're done:

```rockstar

{% include_relative {{ page.examples }}if-else-oneliners.rock %}
```

### Multiline conditionals

Multiline conditionals are a little more complex, because they can create nested scopes. Here's an example where I've used some very un-Rockstar indentation to keep track of scope:

```rockstar
{% include_relative {{ page.examples }}indented-if.rock %}
```

If the condition or the `else` keyword is followed by a new line, it begins a new **rock block**.

A rock block is any number of statements separated by newlines. A rock block ends with:

* An empty line (a line containing only whitespace and/or comments)
* The `end` keyword or any of its aliases `oh`, `yeah` or `baby`
* The `else` or `otherwise` keywords (indicating the start of the alternate result of the current if statement)
*  The end of file EOF

## Oh, yeah, baby

Consider this example from a C-like language:
```
if (x) {
	if (y) {
		if (z {
			print "this might get printed"
		}
	}
}
print "this always gets printed"
```

Before the final line, we need to close three blocks, so we use three closing braces -- `} } }`.

Languages like Python that use indentation to control scope don't have this problem: indenting creates a block, and if you drop a level of indentation, the block's over.

Rock lyrics don't have curly braces, and they don't have indentation... but they do have `oh`, `yeah`, and `baby`. And you can get away with repeating that stuff almost *ad infinitum* and it just sounds like song lyrics... The Corrs' "So Young" opens with the line "yeah, yeah, yeah, yeah, yeah", Whitesnake's "The Deeper The Love" has a "baby baby baby", and if you throw in the odd "oh yeah", nobody's gonna notice.



### Oooooh

Sometimes, you'll find yourself needing to close multiple blocks:









