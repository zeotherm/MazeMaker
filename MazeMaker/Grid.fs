module Grid
open Cell
open System.Text

type Grid = { height: int; 
              width: int; 
              cells: Cell [,] }

let makeEmptyGrid c r = { height = r;
                          width = c;
                          cells = Array2D.init c r (fun i j -> makeCell (i,j)) }

let getCell (g: Grid) (c: Coord): Cell = g.cells.[(fst c),(snd c)]

let tryGetCell (g: Grid) (p: Coord): Cell option = 
    let r = fst p
    let c = snd p
    if r < 0 || c < 0 || r >= g.height || c >= g.width then None
    else Some(g.cells.[r,c])

let buildSimpleGrid col row : Grid = 
    let g = makeEmptyGrid col row
    for c in [|0..col-1|] do
        for r in [|0..row-1|] do
            let nns = [(r - 1, c); (r, c + 1); (r + 1, c); (r, c - 1)] // [Above/N; Right/E; Below/S; Left/W]
            for pcell in nns |> List.map (tryGetCell g) do g.cells.[r,c] <- pcell |> addNeighbor g.cells.[r,c]
    g

let getRow (g: Grid) (r: int): Cell[] = g.cells.[r, *]

let size (g:Grid) : int = g.height * g.width

let randomCell (g:Grid) : Cell = 
    let R = System.Random()
    let r = R.Next(0, g.height - 1)
    let c = R.Next(0, g.width - 1)
    g.cells.[r,c]

let printGrid (g: Grid): string = 
    let output:StringBuilder = StringBuilder ("+" + (String.replicate g.width "---+") + "\n")
    for i in [0..g.width - 1] do
        let row = getRow g i
        let top = "|"
        let bottom = "+"
        let topLine = new StringBuilder(top)
        let bottomLine = new StringBuilder(bottom)
        for j in [0..row.Length - 1] do
            let body = "   "
            let east_boundary = if hasLink row.[j] East then " " else "|"
            let south_boundary = if hasLink row.[j] South then "   " else "---"
            let corner = "+"
            topLine.Append(body + east_boundary) |> ignore
            bottomLine.Append(south_boundary + corner) |> ignore
        output.AppendLine(topLine.ToString()) |> ignore
        output.AppendLine(bottomLine.ToString()) |> ignore
    output.ToString()