open System
open Agents

[<EntryPoint>]
let main argv =
    use statsAgent = new StatsAgent()
    statsAgent.Incr(5.0)
    statsAgent.Incr(7.0)
    printfn "%A" (statsAgent.Fetch())
    statsAgent.Incr(7.0)
    printfn "%A" (statsAgent.Fetch())
    0 // return an integer exit code
