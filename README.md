# rockstar2
It's time. Rockstar 2: "The Difficult Second Version"

OK, project constraints:

examples/

```
codewithrockstar.com
	/index.html
	/example.md
	/js
		/rockstar-editor.js (from codemirror)
		/main.js (from Starship Rockstar.Wasm)
		/_framework
			/* (all from dotnet publish Rockstar.Wasm)
			
		
		
	
```

Symlinking the examples folder

There's a folder called `examples` which has to exist as a child folder of both the .NET project *and* the Jekyll website.

I've done this using `mklink`, provided with the Windows `cmd` interpreter:

```cmd
D:\Rockstar2\codewithrockstar.com\> mklink /D examples ..\Starship\Rockstar.Test\programs\examples
```

This is incredibly fragile - moving or renaming folders breaks the link and you just end up with two disconnected copies of everything.

