module GraphGrid

open System.Text

type Direction =
    | North 
    | East
    | South
    | West

type Coord = int * int

type Movement = int*int

type Cell = Coord * Direction list

type SquareGrid = Cell list

let emptyDirs: Direction list = []

let coord (c: Cell): Coord = fst c

let links (c: Cell): Direction list = snd c

let addLink (c: Cell) (d: Direction): Cell = 
    (coord c, d :: links c)

let col (c: Coord): int = snd c

let row (c: Coord): int = fst c

let makeGrid h w : SquareGrid = 
    [for r in 0 .. h-1 do for c in 0 .. w-1 -> (r,c)] |> List.map (fun l -> l, emptyDirs)

let movement d : Movement = 
    match d with 
    | North -> (-1, 0) // move Up one Row
    | East -> (0, 1)   // move Right one Column
    | South -> (1, 0)  // move Down one Row
    | West -> (0, -1)  // move Left one Column

let move (c:Coord) (m: Movement): Coord = 
    (row c + fst m, col c + snd m)

let containsCoord (g:SquareGrid) (c:Coord): bool =
    List.exists (fun e -> coord e = c) g

let getCell (g:SquareGrid) (c:Coord): Cell option =
    List.tryFind (fun e -> coord e = c) g

let opposite d: Direction = 
    match d with 
    | North -> South
    | East -> West
    | South -> North
    | West -> East

let linkCell (g:SquareGrid) (c:Coord) (d:Direction): SquareGrid = 
    match (getCell g c, movement d |> move c |> getCell g) with
        | Some(orig), Some(conn) -> let linkf = List.map (fun cell -> if coord cell = coord orig then addLink cell d else cell) 
                                    let linkb = List.map (fun cell -> if coord cell = coord conn then addLink cell (opposite d) else cell)
                                    g |> (linkf >> linkb)
        | _, _ -> g

let getRow (g: SquareGrid) (r: int): Cell list = 
    (List.filter (fun c -> row (coord c) = r) >> List.sortBy (fun c -> col (coord c))) g

let height (g:SquareGrid): int = 
    g |> List.map coord |> List.map row |> List.max

let width (g:SquareGrid): int = 
    g |> List.map coord |> List.map col |> List.max

let printGrid (g: SquareGrid): string = 
    let (height, width) = (height g, width g)
    let output:StringBuilder = StringBuilder ("+" + (String.replicate width "---+") + "\n")
    for i in [0..height ] do
        let row = getRow g i
        let top = "|"
        let bottom = "+"
        let topLine = new StringBuilder(top)
        let bottomLine = new StringBuilder(bottom)
        for j in [0..row.Length - 1] do
            let c = List.item j row 
            let body = "   "
            let east_boundary = if List.contains East (links c)  then " " else "|"
            let south_boundary = if List.contains South (links c) then "   " else "---"
            let corner = "+"
            topLine.Append(body + east_boundary) |> ignore
            bottomLine.Append(south_boundary + corner) |> ignore
        output.AppendLine(topLine.ToString()) |> ignore
        output.AppendLine(bottomLine.ToString()) |> ignore
    output.ToString()
