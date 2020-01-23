module GraphGrid

type Direction =
    | North 
    | East
    | South
    | West

type Coord = int * int

type Cell = Coord * Direction list

type SquareGrid = Map<Coord, Direction list>

let empty: Direction list = []

let col (c: Coord): int = 
    snd c

let row (c: Coord): int =
    fst c

let makeGrid h w : SquareGrid = 
    [for r in 0 .. h-1 do for c in 0 .. w-1 -> (r,c)] |> List.map (fun l -> l, empty) |> Map.ofList

let tryGetCell (g:SquareGrid) (c:Coord): Cell option =
    if g.ContainsKey c then Some(c, g.Item(c)) else None
    

