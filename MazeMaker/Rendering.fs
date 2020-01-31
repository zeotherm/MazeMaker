module Rendering

open ListGrid
open System.Text
open System.Drawing

let toString (g: SquareGrid): string = 
    let output = StringBuilder ("\n+" + (String.replicate ((width g)+1) "---+") + "\n")
    let body = "   "
    let corner = "+"
    List.map snd (getRows g) |> List.iter (fun row ->
        let topLine = new StringBuilder("|")
        let bottomLine = new StringBuilder("+")
        List.iter (fun c ->
            let east_boundary = if List.contains East (links c)  then " " else "|"
            let south_boundary = if List.contains South (links c) then "   " else "---"
            topLine.Append(body + east_boundary) |> ignore
            bottomLine.Append(south_boundary + corner) |> ignore
        ) row             
        output.AppendLine(topLine.ToString()) |> ignore
        output.AppendLine(bottomLine.ToString()) |> ignore
    )
    output.ToString()

let drawHorizontalLine (y: int) (x_b: int) (x_e: int) (c: Color) (b: Bitmap): Unit = 
    for x in x_b .. x_e do
        b.SetPixel(x, y, c)

let drawVerticalLine (x: int) (y_b: int) (y_e: int) (c: Color)  (b: Bitmap): Unit = 
    for y in y_b .. y_e do
        b.SetPixel(x, y, c)
 
let cell_size = 20

let toImage (g: SquareGrid): Bitmap = 
    let w = (width g) + 1
    let bmp_w = w* cell_size + cell_size
    let h = (height g) + 1
    let bmp_h = h*cell_size + cell_size
    let x_offset = cell_size/2
    let y_offset = cell_size/2
    let wall_color = Color.Black
    let background_color = Color.White
    let b = new Bitmap(bmp_w, bmp_h)
    // color it white
    for xs in 0..bmp_w-1 do 
        for ys in 0..bmp_h-1 do
            b.SetPixel(xs, ys, background_color)

    for cell in g do
        let crd = coord cell
        let links = links cell
        let (r, c) = (row crd, col crd)
        let x0 = c * cell_size + x_offset
        let x1 = (c + 1) * cell_size + x_offset
        let y0 = r * cell_size + y_offset
        let y1 = (r + 1) * cell_size + y_offset
        let ns = validNeighbors g (r, c) [North; West]
        if not (List.contains North ns) then drawHorizontalLine y0 x0 x1 wall_color b
        if not (List.contains West ns) then drawVerticalLine x0 y0 y1 wall_color b
        if not (List.contains East links) then drawVerticalLine x1 y0 y1 wall_color b 
        if not (List.contains South links) then drawHorizontalLine y1 x0 x1 wall_color b
    b
