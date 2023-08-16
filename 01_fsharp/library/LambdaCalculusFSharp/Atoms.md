# Atoms.fs

`LambdaCalculus/Atoms.fs`

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
