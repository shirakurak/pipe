# `Program.fs`

出発点と言える、`LambdaCalculusConsole/Program.fs`を読みます。

```fs
open System
open LambdaCalculus.Utils
open LambdaCalculus.Parsing
open LambdaCalculus.Output
open LambdaCalculus.Atoms
open LambdaCalculus.ToCSharp
```

`System`モジュール（一般的に使われるクラスや関数が定義されている）を除くと、`LambdaCalculus`配下の（一部の）ファイルがインポートされています。

```fs
let rec inputAndRespond () =
  Console.ForegroundColor <- ConsoleColor.White
  let input = Console.ReadLine()
  match parse input with
  | Ok parsed ->
  ...
  | Error error ->
  ...
inputAndRespond ()
```

本ファイルでは、1つの再帰的関数が定義されています。この関数はユーザからの入力を受け取り、それを構文解析（`parse`）して、その結果で処理を分岐しています。`parse`メソッドについては、`LambdaCalculus/Parsing/Parsing.fs`を見ていきましょう。

`Ok parsed`の中で、さらにパターンマッチの実装があります：

```fs
...
  | Ok parsed ->
    ...
    match betaReduce parsed with
    | MayTerminate expr ->
      printfn $"Beta-reduced: {sprintLambda expr}"
    | NeverTerminates ->
      ...
```

パターンマッチが入れ子の状態となっています。`betaReduce`の実装を見ていく必要があります。
