module BinaryTree

open ListGrid
open Utils

let makeBinaryTreeMaze height width = 
    let g = makeGrid height width
    let dirs = [North; East]
    let binaryTreeOp (c:Cell): SquareGrid -> SquareGrid =
        let neighbors = validNeighbors g (coord c) dirs
        let other = sampleOpt neighbors
        match other with 
        | Some(o) -> linkCellOp g (coord c) o
        | None -> id
    
    let transforms = List.map binaryTreeOp g 

    List.fold (fun grid xform -> xform grid) g transforms






