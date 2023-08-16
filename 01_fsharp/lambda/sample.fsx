// dotnet fsi sample.fsx

printfn "start"

// --------------------

// ラムダ式の定義
type Lambda =
  | Variable of char
  | Abstraction of Head : char * Body : Lambda
  | Application of Function : Lambda * Argument : Lambda

// 例1（変数u）
let u = Variable('u') // u : Lambda

// 例2（ラムダ抽象）
let E_1 = Abstraction('u', u) // λu.u
let E_2 = Abstraction('v', E_1) // λv.(λu.u)

// 例3（関数適用）
let a = Variable('a')
let E = Application(E_1, a) // (λu.u)a

// --------------------

// 深さ
let rec calLambdaDepth e =
  match e with
  | Variable(u) -> 1
  | Abstraction(u, e1) -> 1 + calLambdaDepth(e1)
  | Application(e1, e2) -> calLambdaDepth(e1) + calLambdaDepth(e2)

// --------------------

// 出現している変数
let rec allVar e =
  match e with
  | Variable(u) -> [ u ]
  | Abstraction(u, e1) -> u :: allVar(e1)
  | Application(e1, e2) -> allVar(e1) @ allVar(e2)

// 出現している自由変数
let rec freeVar e =
  match e with
  | Variable(u) -> [ u ]
  | Abstraction(u, e1) -> List.filter (fun c -> c <> u ) (freeVar(e1))
  | Application(e1, e2) -> freeVar(e1) @ freeVar(e2)

// 出現している束縛変数
let rec boundVar e =
  match e with
  | Variable(u) -> []
  | Abstraction(u, e1) -> u :: boundVar(e1)
  | Application(e1, e2) -> boundVar(e1) @ boundVar(e2)

// --------------------

// 代入
let rec substitute e s t =
  match e with
  | Variable(u) ->
    if u = s then Variable(t) else Variable(u)
  | Abstraction(u, e1) ->
    if u = s then Abstraction(u, e1) else Abstraction(u, substitute e1 s t)
  | Application(e1, e2) -> Application(substitute e1 s t, substitute e2 s t)

// ラムダ項に対して、出現しない変数を用意する
let alphabet = ['a' .. 'z']
let rec getNewVar e =
  match e with
  | Variable(u) -> (List.filter (fun c -> c <> u) alphabet)[0]
  | Abstraction(u, e1) ->
    ...
  | Application(e1, e2) -> ...

// α-変換できるか判定
// 引数：Lambda, Lambda
// 戻り値：boolean
// 2つのタイプが一致している必要がある
// 違かったら、false
// どちらもvariableなら、true
// どちらもApplicationなら、各構成要素ごとの比較
// Abstractionの時が問題
// λx.E1とλy.E2
// xがλy.E2の変数として出現していなかったら、λy.E2をλx.E[y/x]として、E1とE2の
// 同値性を見ればいい
// 出現していた場合、yをx以外の全然違う変数にする必要がある
// λy.E2に出現している変数全体の配列を用意
// その長さ + 1分の添字ごとの変数を用意して、その差分の中から、
// 最も小さい添字の変数を持ってくる
// それでxを置換して、そのあとで、yをxにする
// もし全部出てたら、for文で、[a1, b1, ...]を生成する、などとして
// 特定のラムダ式に出現していない変数を取得してくるという関数を書いてもいいか

// β-簡約する

// 与えられた文字列がラムダ式か判定する

printfn "goal"
