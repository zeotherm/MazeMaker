// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Cell
open Grid
open System
open System.Drawing
open System.IO
open BinaryTree

let drawSquare () = 
    let bitmap = new Bitmap(16,16)
    for px in 2..10 do 
        bitmap.SetPixel(px, 2, Color.Black)
        bitmap.SetPixel(px, 10, Color.Black)
        bitmap.SetPixel(2, px, Color.Black)
        bitmap.SetPixel(10, px, Color.Black)
    bitmap

[<EntryPoint>]
let main argv = 
    let maze = makeSimpleGrid 4 6 
    
    printfn "%s" (printGrid maze)

    let binTreeMaze = makeBinaryTreeMaze 20 20


    printGrid binTreeMaze |> printfn "%s"
    Console.ReadLine() |> ignore

    //drawSquare().Save(Path.Combine(__SOURCE_DIRECTORY__, "square.png"))
    0 // return an integer exit code
