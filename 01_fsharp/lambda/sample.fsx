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

// α-変換できるか判定

// β-簡約する

// 与えられた文字列がラムダ式か判定する

printfn "goal"
