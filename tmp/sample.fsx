// dotnet fsi sample.fsx

printfn "start"

// type Variable = char

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

printfn "goal"
