module Cell

open Utils

type Direction =
    | North 
    | East
    | South
    | West

type Coord = int * int

let col (c: Coord): int = 
    snd c

let row (c: Coord): int =
    fst c

type Cell = {
    loc : Coord;
    links : Coord list;
    neighbors: Coord list;
}

let makeCell (c: Coord): Cell = {loc = c; links = []; neighbors = [] }

let addNeighbor (c: Cell) (n: Cell option): Cell = 
    match n with 
    | Some(cell) -> { c with neighbors = cell.loc :: c.neighbors}
    | None -> c

let removeNeighbor (c: Cell) (n: Cell): Cell = 
    { c with neighbors = List.filter (fun z -> z <> n.loc) c.neighbors }

let removeAllNeighbors (c: Cell) : Cell =
    { c with neighbors = []}

let addLink (c: Cell) (l: Cell): Cell = 
    { c with links = l.loc :: c.links}

let removeLink (c: Cell) (l: Cell): Cell =
    { c with links = List.filter (fun z -> z <> l.loc) c.links }

let private directionChecker (c: Cell) (d: Direction) : Coord list -> bool = 
    // 0,0 is the top left (Northwest most point)
    match d with 
        | North -> List.exists (fun n -> col n = col c.loc && row n = (row c.loc) - 1) 
        | East -> List.exists (fun n -> (col c.loc) + 1 = col n && row c.loc = row n) 
        | South -> List.exists (fun n -> col c.loc = col n && row n = (row c.loc) + 1)
        | West -> List.exists (fun n -> (col c.loc) - 1 = col n && row c.loc = row n)

let hasNeighbor (c: Cell) (d:Direction) = c.neighbors |> directionChecker c d

let getNeighborCoords (c: Cell) (d: Direction) : Coord option = 
    if hasNeighbor c d then
        match d with 
        | North -> Some(List.find (fun n -> col n = col c.loc && row n = (row c.loc) - 1) c.neighbors)
        | East -> Some(List.find (fun n -> (col c.loc) + 1 = col n && row c.loc = row n) c.neighbors)
        | South -> Some(List.find (fun n -> col c.loc = col n && row n = (row c.loc) + 1) c.neighbors)
        | West -> Some(List.find (fun n -> (col c.loc) - 1 = col n && row c.loc = row n) c.neighbors)
    else 
        None

let hasLink (c: Cell) (d:Direction) = c.links |> directionChecker c d

let isLinked (this: Cell) (that: Cell) = this.links |> List.exists (fun c -> c = that.loc)

let randomNeighbor (c: Cell) : Coord option = c.neighbors |> sampleOpt
    

