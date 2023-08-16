# LambdaCalculusFSharpの紹介

[LambdaCalculusFSharp](https://github.com/WhiteBlackGoose/LambdaCalculusFSharp/tree/main)は、pureなF#で書かれた、ラムダ計算のライブラリです。本記事では、リバースエンジニアリングによる設計図の理解を目的とします。

## コードリーディング

### ディレクトリ構造

まずは、ディレクトリ構造を把握します：

```sh
tree .
.
├── LICENSE
├── LambdaCalculus
│   ├── LambdaCalculus
│   │   ├── Atoms.fs
│   │   ├── LambdaCalculus.fsproj
│   │   ├── Output.fs
│   │   ├── Parsing
│   │   │   ├── Parsing.fs
│   │   │   └── TextParsing.fs
│   │   ├── ToCSharp.fs
│   │   └── Utils.fs
│   ├── LambdaCalculus.sln
│   ├── LambdaCalculusConsole
│   │   ├── LambdaCalculusConsole.fsproj
│   │   └── Program.fs
│   ├── LambdaCalculusTests
│   │   ├── AlphaEquality.fs
│   │   ├── BetaReduction.fs
│   │   ├── EtaReduction.fs
│   │   ├── LambdaCalculusTests.fsproj
│   │   ├── Parsing.fs
│   │   ├── Program.fs
│   │   └── Substitution.fs
│   └── LambdaCalculusWeb
│       ├── App.razor
│       ├── LambdaCalculusWeb.csproj
│       ├── Pages
│       │   └── Index.razor
│       ├── Program.cs
│       ├── Properties
│       │   └── launchSettings.json
│       ├── Shared
│       │   ├── MainLayout.razor
│       │   └── MainLayout.razor.css
│       ├── _Imports.razor
│       └── wwwroot
│           ├── favicon.ico
│           ├── icon-192.png
│           └── index.html
└── README.md

11 directories, 30 files
```

ファイル数は多くなく、テストファイルやWeb用ファイルを除くと、読むべきなのは10ファイル程度となります。

---

### `Program.fs`

出発点である、`LambdaCalculusConsole/Program.fs`から読んでいきます。

```fs
open System
open LambdaCalculus.Utils
open LambdaCalculus.Parsing
open LambdaCalculus.Output
open LambdaCalculus.Atoms
open LambdaCalculus.ToCSharp
```

`System`モジュール(一般的に使われるクラスや関数が定義されている)を除くと、`LambdaCalculus`配下の(一部の)ファイルがインポートされています。

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

本ファイルでは、1つの再帰的関数が定義されています。この関数はユーザからの入力を受け取り、それを構文解析(`parse`)して、その結果で処理を分岐しています。次に、`parse`メソッドが実装されている、`LambdaCalculus/Parsing/Parsing.fs`を見ていきます。

---

### `Parsing.fs`

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

リストが空の場合は、エラーとして扱います。2つ目のパターンで使用されている関数`ValidVariable`は、同じ`Parsing`配下の別ファイルで定義されています。

---

### `TextParsing.fs`

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

---

## 設計図

- 入力を受け取る
- パースする
  - 文字ごとのリストを作成する