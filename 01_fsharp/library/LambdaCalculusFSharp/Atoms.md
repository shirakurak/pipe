# Atoms.fs

`LambdaCalculus/Atoms.fs`

```fs
type Variable = char
```

`char`のエイリアスとして、`Variable`型を定義しています。

```fs
type Expression =
  | Variable of Variable
  | Lambda of Head : Variable * Body : Expression
  | Applied of Lambda : Expression * Arugument : Expression
```

`Expression`型が、ラムダ式を表している。

```fs
let rec betaReduce expr : ReductionResult =
  let rec betaReduceInner expr : Expression =
  ...
```

`betaReduce`は再帰的に`betaReduceInner`を呼び出しています。

```fs
let rec betaReduceInner expr : Expression =
    match expr with
    | Applied(Lambda(x, body), arg) ->
      ...
    | Applied(Variable x, arg) ->
      ...
    | Applied(maybeLambda, arg) ->
      ...
    | Lambda(x, body) -> ...
    | Variable x -> ...
```
