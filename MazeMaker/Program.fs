// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open System.IO
open BinaryTree
open Sidewinder
open Rendering

type Algorithm = 
    | BinaryTree
    | Sidewinder

let makeMaze alg h w = 
    match alg with
    | BinaryTree -> (makeBinaryTreeMaze h w, "BinaryTree")
    | Sidewinder -> (makeSidewinderMaze h w, "Sidewinder")     

[<EntryPoint>]
let main argv = 

    let maze, algtype = makeMaze BinaryTree 50 50

    (toImage maze).Save(Path.Combine(__SOURCE_DIRECTORY__, "maze_" + algtype + "_" + DateTime.Now.ToString("yyyyMMddHH_mm_ss") + ".png"))
    0 // return an integer exit code
