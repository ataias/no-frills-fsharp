module Agents
open System

type Stats =
    { Count: int;
      Sum: double; }

type Msg =
    | Incr of double
    | Fetch of AsyncReplyChannel<Stats>
    | Die

type StatsAgent() =
    let agent =
        MailboxProcessor.Start(fun inbox ->
            let rec loop stats =
                async {
                    let! msg = inbox.Receive()
                    match msg with
                    | Incr(x) ->
                        return! loop { Count = stats.Count + 1; Sum = stats.Sum + x}
                    | Fetch(replyChannel) ->
                        replyChannel.Reply(stats)
                        return! loop stats
                    | Die -> return ()
                }
            loop { Count = 0; Sum = 0.0 }
        )

    interface IDisposable with
        member this.Dispose() = this.Die()

    member _.Incr(x) = agent.Post(Incr x)
    member _.Fetch() = agent.PostAndReply(Fetch)
    member _.Die() = agent.Post(Die)