module Cell

type Direction =
    | North 
    | East
    | South
    | West

type NeighborOrLink = 
    | Neighbor
    | Link

type Coord = int * int
let locX (c: Coord): int = 
    snd c

let col (c: Coord): int = 
    snd c

let row (c: Coord): int =
    fst c

let locY (c: Coord): int = 
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

let private directionChecker (c: Cell) (d: Direction) = // 0,0 is the top left (Northwest most point)
    match d with 
        | North -> List.exists (fun n -> locX n = locX c.loc && locY n = (locY c.loc) - 1) 
        | East -> List.exists (fun n -> (locX c.loc) + 1 = locX n && locY c.loc = locY n) 
        | South -> List.exists (fun n -> locX c.loc = locX n && locY n = (locY c.loc) + 1)
        | West -> List.exists (fun n -> (locX c.loc) - 1 = locX n && locY c.loc = locY n)

let hasNeighbor (c: Cell) (d:Direction) = c.neighbors |> directionChecker c d
    //match d with 
    //    | North -> List.exists (fun n -> locX n = locX c.loc && locY n = (locY c.loc) - 1) c.neighbors
    //    | East -> List.exists (fun n -> (locX c.loc) + 1 = locX n && locY c.loc = locY n) c.neighbors
    //    | South -> List.exists (fun n -> locX c.loc = locX n && locY n = (locY c.loc) + 1) c.neighbors
    //    | West -> List.exists (fun n -> (locX c.loc) - 1 = locX n && locY c.loc = locY n) c.neighbors

    
let hasLink (c: Cell) (d:Direction) = c.links |> directionChecker c d
    //match d with 
    //    | North -> List.exists (fun n -> locX n = locX c.loc && locY n = (locY c.loc) - 1) c.links
    //    | East -> List.exists (fun n -> (locX c.loc) + 1 = locX n && locY c.loc = locY n) c.links
    //    | South -> List.exists (fun n -> locX c.loc = locX n && locY n = (locY c.loc) + 1) c.links
    //    | West -> List.exists (fun n -> (locX c.loc) - 1 = locX n && locY c.loc = locY n) c.links



let isLinked (this: Cell) (that: Cell) = this.links |> List.exists (fun c -> c = that.loc)
    

