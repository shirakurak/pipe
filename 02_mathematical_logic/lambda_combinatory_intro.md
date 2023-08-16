# ラムダ計算とコンビネータ論理の基礎

本記事では、ラムダ計算とコンビネータ論理に関して、ごく初歩的なことを記載します。[LambdaCalculusFSharpの紹介](../01_fsharp/library/LambdaCalculusFSharp.md)に対する、補足資料及び応用可能性の検討のための資料という意味合いもあります。できる限り平易な文章とすることを心がけます。

## 小噺

ちょっとした小噺から始めてみましょう。次の簡単な式は、別の書き方ができるでしょうか？

```math
1 + 2
```

次の書き方は、すぐ浮かぶでしょう：

```math
2 + 1
```

または、計算してしまってもいいかもしれません：

```math
3
```

そのほかにも、関数を使って、書き換えることができます：

```math
f(x, y) = x + y
```

```math
g(x) = f(x, 2)
```

```math
h(u, v) = u + v
```

とすれば、

```math
f(1, 2)
```

```math
g(1)
```

```math
h(1, 2)
```

も、同じ数式を表しています。ここには、変数への(一部)代入や変数の名前の変更をすることで、同じものとみなす、という考え方があります。

## ラムダ項

ラムダ項を定義します。 $V = { a, b, c, … x, y, z, … }$ を可算無限集合とします。

### 定義(ラムダ項)

**ラムダ項**とは、以下の規則により機能的に定義される文字列のこと：

1. $V$ の要素は、ラムダ項である
2. $v ∈ V$ , $E$ がラムダ項なら、 $λv.E$ はラムダ項である
3. $E_1$ , $E_2$ がラムダ項なら、 $E_1 E_2$ はラムダ項である

### 定義(抽象と適用)

### 定義

コンビネータ項とは、以下の形式の1つ:

- $x$
- $P$
- $(E_1 E_2)$

ここで、 $x$ は変数、 $P$ は原始的関数を表す。

## 参考

- [ラムダ計算 - ウィキペディア](https://ja.wikipedia.org/wiki/%E3%83%A9%E3%83%A0%E3%83%80%E8%A8%88%E7%AE%97)
- [コンビネータ論理 - ウィキペディア](https://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%93%E3%83%8D%E3%83%BC%E3%82%BF%E8%AB%96%E7%90%86)