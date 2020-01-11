// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Cell
open Grid
open System
open System.Drawing
open System.IO

let drawSquare () = 
    let bitmap = new Bitmap(16,16)
    for px in 2..10 do 
        bitmap.SetPixel(px, 2, Color.Black)
        bitmap.SetPixel(px, 10, Color.Black)
        bitmap.SetPixel(2, px, Color.Black)
        bitmap.SetPixel(10, px, Color.Black)
    bitmap


type B = {foo: int;
            bar: bool
            }
[<EntryPoint>]
let main argv = 
    let coor = makeCell (1,1)
 
    printfn "%A" (getNeighbors coor)
    let maze = [| makeCell (0,1); makeCell (0,2); makeCell(0,3); makeCell(0,4); makeCell(0,5) |]

    maze.[1] <- (addNeighbor maze.[1] maze.[0])

    printfn "%A" maze

    let maze = makeEmptyGrid 3 3


    //[0,0   0,1]
    //[1,0   1,1]

    maze.cells.[0,0] <- (addNeighbor maze.cells.[0,0] maze.cells.[0,1])
    maze.cells.[0,0] <- (addNeighbor maze.cells.[0,0] maze.cells.[1,0])

    printfn "%A" maze

    printfn "Cell 0,0 have Northern neighbor? %A" (hasNeighbor maze.cells.[0,0] North)
    printfn "Cell 0,0 have Western neighbor? %A" (hasNeighbor maze.cells.[0,0] West)
    printfn "Cell 0,0 have Eastern neighbor? %A" (hasNeighbor maze.cells.[0,0] East)
    printfn "Cell 0,0 have Southern neighbor? %A" (hasNeighbor maze.cells.[0,0] South)

    printfn "Cell 1,1 have Northern neighbor? %A" (hasNeighbor maze.cells.[1,1] North)
    printfn "Cell 1,1 have Western neighbor? %A" (hasNeighbor maze.cells.[1,1] West)
    printfn "Cell 1,1 have Eastern neighbor? %A" (hasNeighbor maze.cells.[1,1] East)
    printfn "Cell 1,1 have Southern neighbor? %A" (hasNeighbor maze.cells.[1,1] South)

    //drawSquare().Save(Path.Combine(__SOURCE_DIRECTORY__, "square.png"))
    0 // return an integer exit code
