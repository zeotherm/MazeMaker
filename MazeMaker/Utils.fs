module Utils
open System

let R = System.Random()

let shuffle (r : Random) xs = xs |> Seq.sortBy (fun _ -> r.Next())

let sampleOpt xs = xs |> shuffle R |> Seq.tryHead

let sample xs = xs |> shuffle R |> Seq.head

let intToString n = if n < 10 then
                        " " + string n + " "
                    else
                        " " + string ((n + 87) |> char) + " "