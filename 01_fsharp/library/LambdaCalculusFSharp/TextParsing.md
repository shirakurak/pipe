# `TextParsing.fs`

`LambdaCalculus/Parsing/TextParsing.fs`

```fs
let (|ValidVariable|_|) (value : char) =
  if VariableAlphabet.Contains value then
    Some value
  else
    None
```
