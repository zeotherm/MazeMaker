module Cell

let nns = [(-1,0);(0,1);(1,0);(0,-1)]

type Direction =
    | North 
    | East
    | South
    | West

type Coord = int * int
let locX (c: Coord): int = 
    fst c

let locY (c: Coord): int = 
    snd c

type Cell = {
    loc : Coord;
    neighbors : Coord list
}

let makeCell (c: Coord): Cell = {loc = c; neighbors = []}

let addNeighbor (c: Cell) (n: Cell): Cell = 
    { c with neighbors = n.loc :: c.neighbors}

let removeNeighbor (c: Cell) (n: Cell): Cell = 
    { c with neighbors = List.filter (fun z -> z <> n.loc) c.neighbors }

let getNeighbors (c: Cell): Coord list = 
    nns |> List.map (fun dir -> (fst dir + fst c.loc, snd dir + snd c.loc))

let hasNeighbor (c: Cell) (d:Direction) = // 0,0 is the top left (Northwest most point)
    match d with 
        | North -> List.exists (fun n -> locX n = locX c.loc && locY n = (locY c.loc) - 1)  c.neighbors
        | East -> List.exists (fun n -> (locX c.loc) + 1 = locX n && locY c.loc = locY n) c.neighbors
        | South -> List.exists (fun n -> locX c.loc = locX n && locY n = (locY c.loc) + 1) c.neighbors
        | West -> List.exists (fun n -> (locX c.loc) - 1 = locX n && locY c.loc = locY n) c.neighbors

let isLinked (this: Cell) (that: Cell) = 
    this.neighbors |> List.exists (fun c -> c = that.loc)

