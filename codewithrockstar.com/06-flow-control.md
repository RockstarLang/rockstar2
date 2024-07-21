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

This means the syntax for one-line conditionals is slightly different from the syntax for block conditionals.

```
If 1 say 1
If 1 say 1 otherwise say 2
If 1 if 2 say 2 otherwise say 1
if 1 if 2 if 3 say 3 otherwise say 2 otherwise say 1
```





