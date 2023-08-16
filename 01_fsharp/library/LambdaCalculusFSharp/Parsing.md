# Parsing.fs

`LambdaCalculus/LambdaCalculus/Parsing/Parsing.fs`

`parse`の実装は次です：

```fs
let parse (s : string) =
  s
  |> (fun s -> s.Replace("λ", "\\"))
  |> (fun s -> s.Replace(" ", ""))
  |> List.ofSeq
  |> parseInner
```

`List.ofSeq`の1行前までは、文字の置き換えが行われるだけの単純な処理です。

```fs
  "λx.x + 2"
  |> (fun s -> s.Replace("λ", "\\"))
  |> (fun s -> s.Replace(" ", "")) // "\x.x+2"
```

`List.ofSeq`は、[シーケンスからリストを生成する変換関数](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-collections-listmodule.html#ofSeq)です。

```fs
"\x.x+2" |> List.ofSeq // ['\\'; 'x'; '.'; 'x'; '+'; '2']
```

このように、入力に対してパースするために、まずは構成している文字のリストを生成しています。リストに対して、`parseInner`という関数を適用しています。本関数も、同一のファイルに実装されています。

```fs
let rec parseInner s : Result<Expression, string> =
  match s with
  | [] -> Error "Empty input"

  | [ ValidVariable x ] -> Ok (Variable x)

  | other ->
    let rec blockApplier applied blocks =
      match blocks with
      | [] -> applied |> Ok
      | hd::rest -> blockApplier (Applied(applied, hd)) rest

    opt {
      let! blocks = blocksParse other
      let res =
        match blocks with
        | hd::tl -> blockApplier hd tl
        | _ -> Error "Empty block"
      return! res
    }
```

大枠としては、パターンマッチが行われています。

```fs
let rec parseInner s : Result<Expression, string> =
  match s with
  | [] -> Error "Empty input"

  | [ ValidVariable x ] -> Ok (Variable x)

  | other ->
    ...
```

リストが空の場合は、エラーとして扱います。2つ目のパターンで使用されている関数`ValidVariable`は、同じ`Parsing`配下の`LambdaCalculus/Parsing/TextParsing.fs`で定義されています。
