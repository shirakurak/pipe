// dotnet fsi sample.fsx

printfn "start"

type Variable = char

// ラムダ式の定義
type Lambda =
  | Variable of Variable
  | Abstraction of Head : Variable * Body : Lambda
  | Application of Function : Lambda * Argument : Lambda

// Abstractionを生成する関数
let abstracter(v, E) =
  Abstraction(v, E)

printfn "goal"
