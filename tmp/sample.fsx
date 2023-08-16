// dotnet fsi sample.fsx

printfn "start"

type Variable = char

type Lambda =
  | Variable of Variable
  | Abstraction of Head : Variable * Body : Lambda
  | Application of Function : Lambda * Argument : Lambda

printfn "goal"
