```
program = head:statement EOL+ tail:program
	{ return [ head ].concat(tail) }
	/ head:statement
    	{ return [ head ] }
   / EOF { return [] }

block
	= _ head:statement EOS tail:block
    	{ return [ head ].concat(tail) }
    / _ head:statement 
    	{ return [ head ] }        

EOB = 
	EOS* &(_ 'otherwise')
    / EOS* _ 'end'

conditional 
	= 'if' _ d:digit EOL c:block EOB _ 'otherwise' EOL a:block EOB
		{ return { if: d, then: c, else: a } }        
	/ 'if' _ d:digit EOL c:block EOB
		{ return { if: d, then: c } }
	/ 'if' _ d:digit _ c:statement _ 'otherwise' _ a:statement
		{ return { if: d, then: c, else: a } }
    / 'if' _ d:digit _ c:statement
		{ return { if: d, then: c } }

statement =
	conditional    
    / 'say' _ d:digit { return "say " + d }

EOS = _ [.?!;]
	/ EOL

digit = $([0-9]+)
EOL = _ '\r'? '\n'
_ = [ \t]*
EOF = EOL* _!.
	
```

```
if 1
  if 2
    if 3
	    say 3
    otherwise
    	say 2
  otherwise 
    say 1
  end
end

```
