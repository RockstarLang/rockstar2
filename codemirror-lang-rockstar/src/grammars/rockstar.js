// This file was generated by lezer-generator. You probably shouldn't edit it.
import {LRParser} from "@lezer/lr"
import {Variables, Keywords} from "../tokenizers/rockstar-tokenizer"
import {highlighting} from "./rockstar-highlight.js"
export const parser = LRParser.deserialize({
  version: 14,
  states: "$hO]QUOOPhOSOOOOQQ'#ET'#ETO]QUOOOpQUO'#DzOuQVO'#D|OOQO'#E]'#E]O!aQSO'#E[Q!oQSOOPOOO'#EZ'#EZPOOO)C?})C?}OOQQ-E8R-E8RO!wQTO,5:fOOQO'#D{'#D{OOQO'#EQ'#EQOOQO'#D}'#D}OOQO,5:h,5:hO#VQUO'#EUO#_QSO,5:vO#VQUO'#EVQ#mQSOOQOQSOOOOQO1G0Q1G0QOOQO,5:p,5:pOOQO-E8S-E8SOOQO,5:q,5:qOOQO-E8T-E8T",
  stateData: "#u~O!|OS!jPQ!kPQ~OuSO!STO!mQO~O!jXO!kXO~O!`[O~OP]OQ]OR]OS]Oj^O!a^O!r_O!s_O~O!maO!u#OX!v#OX!z#OX~O!ucO!veO~OP]OQ]OR]OS]O~OuSO!STO~O!maO!u#Oa!v#Oa!z#Oa~O!ucO~O!m!u~",
  goto: "$Z#QPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP#R#X#R#_PP#bPP#e#k#qPPP#w#z$RXUORacQ_TRf[R`TR_TQRORZRQbVRhbQdWRjdRYPSWORRicUVORcRga",
  nodeNames: "⚠ ProperVariable CommonVariable SimpleVariable Pronoun Above And Around As AsGreat AsSmall At Back Be Break Build Call Cast Continue Debug Divided Down Else Empty End Exactly False His If Into Is Isnt Join Knock Less Let Like Listen Minus More Mysterious Non Nor Not Now Null Or Over Plus Pop Print Push Put Return Says Split Takes Taking Than The Then Times To True Turn Under Until Up Using While With Write LineComment BlockComment Program EOS ListenStatement Variable OutputStatement Expression Number String Boolean EOB EOF",
  maxTerm: 93,
  propSources: [highlighting],
  skippedNodes: [0,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,27,28,29,30,31,32,33,34,35,36,38,39,40,41,42,43,44,45,46,47,48,49,51,52,53,54,55,56,57,58,59,60,61,64,65,66,67,68,69,70,71,72,73],
  repeatNodeCount: 3,
  tokenData: "'q~R^XY}YZ!Y]^!vpq}rs!|st$jxy%X{|%v}!O%v!O!P&P!Q![&_!}#O&j#o#p'S~~'l~!SQ!|~XY}pq}~!_Q!m~YZ!e]^!p~!jQ!u~YZ!e]^!p~!sPYZ!e~!yPYZ!Y~#PVOr!|rs#fs#O!|#O#P#k#P;'S!|;'S;=`$d<%lO!|~#kO!s~~#nRO;'S!|;'S;=`#w;=`O!|~#zWOr!|rs#fs#O!|#O#P#k#P;'S!|;'S;=`$d;=`<%l!|<%lO!|~$gP;=`<%l!|~$mTOY$jYZ$|Z;'S$j;'S;=`%R<%lO$j~%RO!j~~%UP;=`<%l$j~%[TOy%Xyz%kz;'S%X;'S;=`%p<%lO%X~%pO!k~~%sP;=`<%l%X~%yQ!O!P&P!Q![&_~&SP!Q![&V~&[P!r~!Q![&V~&dQ!r~!O!P&P!Q![&_~&mTO#P&j#P#Q%k#Q;'S&j;'S;=`&|<%lO&j~'PP;=`<%l&j~'VTO#q'S#q#r%k#r;'S'S;'S;=`'f<%lO'S~'iP;=`<%l'S~'qO!v~",
  tokenizers: [Variables, Keywords, 0],
  topRules: {"Program":[0,74]},
  tokenPrec: 126
})
