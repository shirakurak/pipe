# `TextParsing.fs`

`LambdaCalculus/Parsing/TextParsing.fs`

```fs
let (|ValidVariable|_|) (value : char) =
  if VariableAlphabet.Contains value then
    Some value
  else
    None
```

`LambdaCalculus/Atoms.fs`にて、

```fs
let VariableAlphabet = "xyzabcdefghijklmnopqrstuvw"
```

と定義されており、アルファベットである場合は、マッチしたことを表す`Some value`が返却されます。
