---
title: Flow Control
layout: home
examples: examples/06-flow-control/
nav_order: "1006"
---
## Conditionals: If / Else

Conditionals in Rockstar are... odd. When they work, they look like perfectly normal English, but they have the same syntactic ambiguities as normal English.

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

Rockstar doesn't have curly braces, because you can't sing curly braces.
### One-line if

One-line if statements don't create any block scope. However many `if` statements you stack on the same line, the final statement on the line either runs or it doesn't, and then you're done.

```rockstar

{% include_relative {{ page.examples }}if-else-oneliners.rock %}
```

### Multiline conditionals

Multiline conditionals are a little more complex, because they can create nested scopes. Here's an example where I've used some very un-Rockstar indentation to keep track of scope:

```
if x > 1
	if x > 2
		if x > 3
			if x > 4
				if x > 5
					print "X is greater than 5"!
					end
				print "X is greater than 4!"
				end
			print "X is greater than 3!"
			end
		print "X is greater than 2!"
		end
	print "X is greater than 1"
otherwise
	print "X was not greater than 1"
```

If the condition or the `else` keyword is followed by a new line, it begins a new **rock block**.

A rock block is any number of statements separated by newlines. A rock block ends with:

* An empty line (a line containing only whitespace and/or comments)
* The `end` keyword or any of its aliases `oh`, `yeah` or `baby`
*  







