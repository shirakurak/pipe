# LambdaCalculusFSharpの紹介

[LambdaCalculusFSharp](https://github.com/WhiteBlackGoose/LambdaCalculusFSharp/tree/main)は、pureなF#で書かれた、ラムダ計算のライブラリです。本記事では、リバースエンジニアリングによる設計図の理解を目的とします。

ラムダ計算の初歩的な内容については、[ラムダ計算とコンビネータ論理の基礎](../../../02_mathematical_logic/lambda_combinatory_intro.md)も参考のこと。

## ディレクトリ構造

ディレクトリ構造は以下となっています（途中略）：

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
│   ...
└── README.md

11 directories, 30 files
```

ファイル数は多くなく、テストファイルやWeb用ファイルを除くと、読むべきなのは10ファイル程度となります。

## 設計図

- 入力を受け取る
- パースする
  - 文字ごとのリストを作成する
