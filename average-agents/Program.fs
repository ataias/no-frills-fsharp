open System
open Agents

let measureTime f =
  let stopWatch = Diagnostics.Stopwatch.StartNew()
  f()
  stopWatch.Stop()
  stopWatch.Elapsed.TotalMilliseconds

let useAgents() =
  use statsAgent = new StatsAgent()

  let producers =
    [| { TotalNumbers = 100
         SleepIntervalMilliseconds = 50 }
       { TotalNumbers = 100
         SleepIntervalMilliseconds = 50 }
       { TotalNumbers = 200
         SleepIntervalMilliseconds = 30 }
       { TotalNumbers = 200
         SleepIntervalMilliseconds = 30 } |]

  let poster = { Producers = producers }

  let posterAgent = PosterAgent(statsAgent, poster)
  posterAgent.Execute()

[<EntryPoint>]
let main _ =
  let elapsedTime = measureTime useAgents
  printfn "Program execution took %fms" elapsedTime
  0 // return an integer exit code
