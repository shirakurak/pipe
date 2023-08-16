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
let rec allvar e =
  match e with
  | Variable(u) -> [ u ]
  | Abstraction(u, e1) -> u :: allvar(e1)
  | Application(e1, e2) -> allvar(e1) @ allvar(e2)

// 出現している自由変数

// 出現している束縛変数

// λへの翻訳
// CalcFormulaみたいに

printfn "goal"
