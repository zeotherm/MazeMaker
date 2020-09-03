// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open System.IO
open BinaryTree
open Sidewinder
open Rendering
open ListGrid
open Distances

type Algorithm = 
    | BinaryTree
    | Sidewinder

let makeMaze alg h w = 
    match alg with
    | BinaryTree -> (makeBinaryTreeMaze h w, "BinaryTree")
    | Sidewinder -> (makeSidewinderMaze h w, "Sidewinder")     

[<EntryPoint>]
let main argv = 

    let maze, algtype = makeMaze Sidewinder 10 10

    let maze_2x2 = linkCell (linkCell (linkCell (makeGrid 2 2) (0,0) East) (0,1) South) (1,1) West

    let dists = computeDistance (0,0) maze_2x2

    printfn "---------------- TEST MAZE ----------------"

    printfn "%s" (toString maze_2x2)

    printfn "%A" dists

    let maze_2x2_w_dist = convolute dists maze_2x2

    printfn "%s" (toString maze_2x2_w_dist)

    printfn "--------------- ACTUAL MAZE ---------------"

    let dists_m = computeDistance (0,0) maze

    printfn "%s" (toString maze)

    printfn "%A" dists_m

    let maze_w_dists = convolute dists_m maze
    let f = maze_w_dists |> findShortestPath (0,0) (9,9)
    printfn "%s" (toString maze_w_dists)

    let maze_w_sp_dists = convolute f maze
    printfn "%s" (toString maze_w_sp_dists)

    //(toImage maze).Save(Path.Combine(__SOURCE_DIRECTORY__, "maze_" + algtype + "_" + DateTime.Now.ToString("yyyyMMdd_HH_mm_ss") + ".png"))
    0 // return an integer exit code
