module BinaryTree

open Grid
open Cell

let makeBinaryTreeMaze height width  = 
    let g = makeEmptyGrid height width 
    for c in [|0..width-1|] do
        for r in [|0..height-1|] do
            let nns = [(r - 1, c); (r, c + 1)] // [Above/N; Right/E]
            for pcell in nns |> List.map (tryGetCell g) do g.cells.[r,c] <- pcell |> addNeighbor g.cells.[r,c]
            match randomNeighbor g.cells.[r,c] with 
            | Some(oc) -> linkCells g g.cells.[r,c] g.cells.[row oc, col oc]
            | None -> ()
    g


