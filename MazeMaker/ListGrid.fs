module ListGrid

type Direction =
    | North 
    | East
    | South
    | West

type Coord = int * int

type Movement = int * int

type Cell = Coord * Direction list * int option

type SquareGrid = Cell list

let emptyDirs: Direction list = []

let coord (c: Cell): Coord = 
    let (coord, _, _) = c
    coord

let links (c: Cell): Direction list = 
    let (_, ds, _) = c
    ds

let payload (c: Cell): int option = 
    let (_, _, p) = c
    p

let addLink (c: Cell) (d: Direction): Cell = (coord c, d :: links c, payload c)

let col (c: Coord): int = snd c

let row (c: Coord): int = fst c

let makeGrid h w : SquareGrid = 
    [for r in 0 .. h-1 do for c in 0 .. w-1 -> (r,c)] |> List.map (fun l -> l, emptyDirs, None)

let movement d : Movement = 
    match d with 
    | North -> (-1, 0) // move Up one Row
    | East -> (0, 1)   // move Right one Column
    | South -> (1, 0)  // move Down one Row
    | West -> (0, -1)  // move Left one Column

let opposite d: Direction = 
    match d with 
    | North -> South
    | East -> West
    | South -> North
    | West -> East

let move (c:Coord) (m: Movement): Coord = 
    (row c + fst m, col c + snd m)

let containsCoord (g:SquareGrid) (c:Coord): bool =
    List.exists (fun e -> coord e = c) g

let getCell (g:SquareGrid) (c:Coord): Cell option =
    List.tryFind (fun e -> coord e = c) g

let validNeighbors (g:SquareGrid) (c:Coord) (ds: Direction list): Direction list =
    List.filter (fun d -> containsCoord g (move c (movement d)))  ds

let hasNeighbor (g: SquareGrid) (c: Coord) (d: Direction): bool = 
    not (validNeighbors g c [d] |> List.isEmpty)

let linkCellOp (g:SquareGrid) (c:Coord) (d:Direction) : SquareGrid -> SquareGrid = 
    match (getCell g c, movement d |> move c |> getCell g) with
        | Some(orig), Some(conn) -> let linkf = List.map (fun cell -> if coord cell = coord orig then addLink cell d else cell) 
                                    let linkb = List.map (fun cell -> if coord cell = coord conn then addLink cell (opposite d) else cell)
                                    linkf >> linkb
        | _, _ -> id

let linkCell (g:SquareGrid) (c:Coord) (d:Direction): SquareGrid = 
    g |> linkCellOp g c d

let getRow (g: SquareGrid) (r: int): Cell list = 
    (List.filter (fun c -> row (coord c) = r) >> List.sortBy (fun c -> col (coord c))) g

let getRows (g: SquareGrid) : (int * Cell list) list = 
    let gs = List.groupBy (fun c -> row (coord c)) g
    let gss = List.map (fun (i, row) -> (i, List.sortBy (fun c -> col (coord c)) row)) gs
    List.sortBy fst gss

let private maxByCellParam (g: SquareGrid) (f : Coord -> int): int =
    List.maxBy (coord >> f) g |> (coord >> f)

let height (g:SquareGrid): int = maxByCellParam g row

let width (g:SquareGrid): int = maxByCellParam g col