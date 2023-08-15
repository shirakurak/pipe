# LambdaCalculusFSharpの紹介

[LambdaCalculusFSharp](https://github.com/WhiteBlackGoose/LambdaCalculusFSharp/tree/main)は、pureなF#で書かれた、ラムダ計算のライブラリです。本記事では、リバースエンジニアリングによる設計図の理解を目的とします。

## 実装

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

### コードリーディング

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

本ファイルでは、1つの再帰的関数が定義されています。この関数はユーザからの入力を受け取り、それを構文解析(`parse`)して、その結果で処理を分岐しています。

`LambdaCalculus/LambdaCalculus/Parsing/Parsing.fs`

- `parse`
- `parseInner`
