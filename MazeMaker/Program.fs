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
    //let coor = makeCell (1,1)
 
    //printfn "%A" (getNeighbors coor)
    //let maze = [| makeCell (0,1); makeCell (0,2); makeCell(0,3); makeCell(0,4); makeCell(0,5) |]

    //maze.[1] <- (addNeighbor maze.[1] maze.[0])

    //printfn "%A" maze

    //let maze2 = makeEmptyGrid 3 3


    //[0,0   0,1]
    //[1,0   1,1]

    //maze2.cells.[0,0] <- (addNeighbor maze2.cells.[0,0] maze2.cells.[0,1])
    //maze2.cells.[0,0] <- (addNeighbor maze2.cells.[0,0] maze2.cells.[1,0])

    //printfn "%A" maze2

    //printfn "Cell 0,0 have Northern neighbor? %A" (hasNeighbor maze2.cells.[0,0] North)
    //printfn "Cell 0,0 have Western neighbor? %A" (hasNeighbor maze2.cells.[0,0] West)
    //printfn "Cell 0,0 have Eastern neighbor? %A" (hasNeighbor maze2.cells.[0,0] East)
    //printfn "Cell 0,0 have Southern neighbor? %A" (hasNeighbor maze2.cells.[0,0] South)

    //printfn "Cell 1,1 have Northern neighbor? %A" (hasNeighbor maze2.cells.[1,1] North)
    //printfn "Cell 1,1 have Western neighbor? %A" (hasNeighbor maze2.cells.[1,1] West)
    //printfn "Cell 1,1 have Eastern neighbor? %A" (hasNeighbor maze2.cells.[1,1] East)
    //printfn "Cell 1,1 have Southern neighbor? %A" (hasNeighbor maze2.cells.[1,1] South)


    let maze = makeSimpleGrid 4 6 

    maze.cells.[0,0] <- addLink maze.cells.[0,0] maze.cells.[0,1]
    maze.cells.[0,1] <- addLink maze.cells.[0,1] maze.cells.[0,0]

    printfn "Cell 0,0 has Eastern neighbor? %A" (hasNeighbor maze.cells.[0,0] East)

    printfn "Cell 0,0 has Eastern link? %A" (hasLink maze.cells.[0,0] East)
    printfn "Cell 0,0 has Southern link? %A" (hasLink maze.cells.[0,0] South)
    printfn "Cell 0,1 has Western link? %A" (hasLink maze.cells.[0,1] West)
    printfn "Cell 0,1 has Southern link? %A" (hasLink maze.cells.[0,1] South)


    printfn "%A" maze

    printfn "Cell 0,0 have Northern neighbor? %A" (hasNeighbor maze.cells.[0,0] North)
    printfn "Cell 0,0 have Western neighbor? %A" (hasNeighbor maze.cells.[0,0] West)
    printfn "Cell 0,0 have Eastern neighbor? %A" (hasNeighbor maze.cells.[0,0] East)
    printfn "Cell 0,0 have Southern neighbor? %A" (hasNeighbor maze.cells.[0,0] South)

    printfn "Cell 1,1 have Northern neighbor? %A" (hasNeighbor maze.cells.[1,1] North)
    printfn "Cell 1,1 have Western neighbor? %A" (hasNeighbor maze.cells.[1,1] West)
    printfn "Cell 1,1 have Eastern neighbor? %A" (hasNeighbor maze.cells.[1,1] East)
    printfn "Cell 1,1 have Southern neighbor? %A" (hasNeighbor maze.cells.[1,1] South)

    
    printfn "%s" (printGrid maze)

    let binTreeMaze = makeBinaryTreeMaze 20 20

    printGrid binTreeMaze |> printfn "%s"
    Console.ReadLine() |> ignore

    //drawSquare().Save(Path.Combine(__SOURCE_DIRECTORY__, "square.png"))
    0 // return an integer exit code
