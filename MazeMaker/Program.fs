// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open ListGrid
open System
open System.Drawing
open System.IO
open BinaryTree
open Sidewinder

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
    let maze = makeGrid 4 6 
    
    printfn "%s" (printGrid maze)

    let binTreeMaze = makeBinaryTreeMaze 5 5

    printGrid binTreeMaze |> printfn "%s"

    //let sidewinderMaze = makeSidewinderMaze 12 12

    //printGrid sidewinderMaze |> printfn "%s"

    Console.ReadLine() |> ignore

    //drawSquare().Save(Path.Combine(__SOURCE_DIRECTORY__, "square.png"))
    0 // return an integer exit code
