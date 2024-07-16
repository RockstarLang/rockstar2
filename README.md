# rockstar2
It's time. Rockstar 2: "The Difficult Second Version"

The Build Process

Building codewithrockstar.com works like this:

build-and-test-rockstar-engine

- runs on Linux
- Builds the parser and interpreter
- Runs the test suite
- Uploads artifacts for:
	- linux native binary
	- WASM interpreter for the website

IF THAT WORKS:

build-windows-binary
* builds the Rockstar windows binary

build-macos-binary
* builds the macOS binary

 build-and-deploy-website
	* Downloads the linux binary Rockstar WASM artifact from step 1
	* Downloads the windows and macOS binaries from steps 2 and 3
	* Builds the Jekyll site
	* 



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

This is incredibly fragile - moving or renaming folders breaks the link and you just end up with two disconnected copies of everything. Bizarrely, it works just fine on the GitHub Actions checkout.

