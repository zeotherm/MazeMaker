module Utils
open System

let R = System.Random()

let shuffle (r : Random) xs = xs |> Seq.sortBy (fun _ -> r.Next())

let sampleOpt xs = xs |> shuffle R |> Seq.tryHead

let sample xs = xs |> shuffle R |> Seq.head
