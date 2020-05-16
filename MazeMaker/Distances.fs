module Distances
open Utils
open ListGrid

type DistMap = Map<Coord, int>

let computeDistance (g: SquareGrid) (c: Coord): DistMap = 
    let rec processFrontier (g: SquareGrid) (f:Set<Coord>) (d: DistMap): DistMap =
        let addIfNotPresent (v: int) (dm:DistMap) (c:Coord): DistMap = 
            match dm.TryFind(c) with 
            | Some(_) -> dm
            | None -> dm.Add(c, v)
        match (sampleOpt f) with 
        | Some(c) ->
            let ns = neighboringCoords g c
            let d' = List.fold (addIfNotPresent (d.Item(c) + 1)) d ns 
            let f' = List.fold (fun (acc:Set<Coord>) (elem: Coord) -> if not (d.ContainsKey(elem)) then 
                                                                        acc.Add(elem) 
                                                                      else 
                                                                        acc) 
                               f
                               ns
            processFrontier g (f'.Remove(c)) d'
        | None -> d
    processFrontier g (Set.singleton(c)) (Map.ofList [c, 0])

let convolute (d: DistMap) (g: SquareGrid): SquareGrid = 
    Map.fold (fun acc k (v: int) -> (overwritePayload ((getCell g k).Value) (Some(v))) :: acc) [] d



