module Distances
open Utils
open ListGrid

type DistMap = Map<Coord, int>

let computeDistance (c: Coord) (g: SquareGrid): DistMap = 
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
    g |> List.map (fun (c: Cell) -> overwritePayload c (d.TryFind(coord c)))
    
let findShortestPath (start:Coord) (finish: Coord) (g:SquareGrid): DistMap = 
    let getPayloadFromCoord (coord: Coord): int = 
        match getCell g coord with 
        | Some(c') -> match payload c' with 
                      | Some(v) -> v
                      | None -> failwith "Missing payload"
        | None -> failwith "Unreachable state"

    let rec shortestPathAux (c: Coord) (breadcrumbs: DistMap): DistMap = 
        if c = start then
            breadcrumbs
        else
            let neighbors = neighboringCoords g c
            let cellDist = getPayloadFromCoord c
            let nextCell = (List.filter (fun testCell -> getPayloadFromCoord testCell = cellDist - 1) neighbors).Head
            shortestPathAux nextCell (breadcrumbs.Add(nextCell, cellDist - 1))
    
    if Seq.exists (fun c -> (payload c).IsNone) g then
        failwith "Distances have not been fully computed for this maze"
    else
        let initBreadcrumbs = Map.empty.Add(finish, (getPayloadFromCoord finish))
        shortestPathAux finish initBreadcrumbs