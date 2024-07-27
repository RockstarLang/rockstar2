https://codemirror.net/examples/bundle/

Step 1: create editor.mjs

```javascript

import {EditorView, basicSetup} from "codemirror"
import {rockstar} from "???"

let editor = new EditorView({
  extensions: [basicSetup, rockstar()],
  parent: document.body
})
```

Install the pacakges
```cmd
# The CodeMirror packages used in our script
npm i codemirror
# Rollup and its plugin
npm i rollup @rollup/plugin-node-resolve
```

Run the rollup:

```
node_modules/.bin/rollup editor.mjs -f iife -o editor.bundle.js -p @rollup/plugin-node-resolve
```

Create `rollup.config.mjs`:

```javascript
import {nodeResolve} from "@rollup/plugin-node-resolve"
export default {
  input: "./editor.mjs",
  output: {
    file: "./editor.bundle.js",
    format: "iife"
  },
  plugins: [nodeResolve()]
}
```

