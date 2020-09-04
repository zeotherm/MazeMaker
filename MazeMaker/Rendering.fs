module Rendering

open ListGrid
open Utils
open Distances
open System.Text
open System.Drawing

type DisplayTheme =
    | Green
    | Red
    | Blue
    | Gold
    | Purple
    | Aqua

let toString (g: SquareGrid): string = 
    let output = StringBuilder ("\n+" + (String.replicate ((width g)+1) "---+") + "\n")
    let corner = "+"
    List.map snd (getRows g) |> List.iter (fun row ->
        let topLine = new StringBuilder("|")
        let bottomLine = new StringBuilder("+")
        List.iter (fun c ->
            let east_boundary = if List.contains East (links c)  then " " else "|"
            let south_boundary = if List.contains South (links c) then "   " else "---"
            let bodyval = match (payload c) with
                          | Some(v) -> intToString v
                          | None -> "   "
            topLine.Append(bodyval + east_boundary) |> ignore
            bottomLine.Append(south_boundary + corner) |> ignore
        ) row             
        output.AppendLine(topLine.ToString()) |> ignore
        output.AppendLine(bottomLine.ToString()) |> ignore
    )
    output.ToString()

let drawHorizontalLine (y: int) (x_b: int) (x_e: int) (c: Color) (b: Bitmap): Unit = 
    for x in x_b .. x_e do
        b.SetPixel(x, y, c)

let drawVerticalLine (x: int) (y_b: int) (y_e: int) (c: Color) (b: Bitmap): Unit = 
    for y in y_b .. y_e do
        b.SetPixel(x, y, c)

let fillCell (x_l: int) (y_b: int) (x_r: int) (y_t: int) (c: Color) (b: Bitmap): Unit = 
    for x in x_l+1 .. x_r do
        for y in y_b+1 .. y_t do
            b.SetPixel(x, y, c)

let computeCellColor (dist:int) (max:int) (t: DisplayTheme): Color = 
    let intensity = ((max - dist) |> double)/ (double max)
    let dark = (255.0*intensity) |> round
    let bright = 128 + (round (127.0 * intensity))
    match t with
    | Green -> Color.FromArgb(dark, bright, dark)
    | Red -> Color.FromArgb(bright, dark, dark)
    | Blue -> Color.FromArgb(dark, dark, bright)
    | Gold -> Color.FromArgb(bright, bright, dark)
    | Purple -> Color.FromArgb(bright, dark, bright)
    | Aqua -> Color.FromArgb(dark, bright, bright)

let cell_size = 20

let toImage (maze: SquareGrid) (theme: DisplayTheme) : Bitmap = 
    
    // set up the maximum distance path
    let dist = computeLongestDistance maze
    let g = convolute dist maze
    let longestDist = dist |> Map.toSeq |> Seq.maxBy snd |> snd

    let w = (width g) + 1
    let bmp_w = w * cell_size + cell_size
    let h = (height g) + 1
    let bmp_h = h * cell_size + cell_size
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
        let distance = payload cell
        let cell_color = match distance with
                         | Some(d) -> computeCellColor d longestDist theme
                         | None -> Color.White
        let (r, c) = (row crd, col crd)
        let x0 = c * cell_size + x_offset
        let x1 = (c + 1) * cell_size + x_offset
        let y0 = r * cell_size + y_offset
        let y1 = (r + 1) * cell_size + y_offset
        fillCell x0 y0 x1 y1 cell_color b
        let ns = validNeighbors g (r, c) [North; West]
        if not (List.contains North ns) then drawHorizontalLine y0 x0 x1 wall_color b
        if not (List.contains West ns) then drawVerticalLine x0 y0 y1 wall_color b
        if not (List.contains East links) then drawVerticalLine x1 y0 y1 wall_color b 
        if not (List.contains South links) then drawHorizontalLine y1 x0 x1 wall_color b
    b
