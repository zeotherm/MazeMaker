module BinaryTree

open Grid
open Cell

let R = System.Random()

let makeBinaryTreeMaze height width  = 
    let g = makeEmptyGrid height width 
    for c in [|0..width-1|] do
        for r in [|0..height-1|] do
            let nns = [(r - 1, c); (r, c + 1)] // [Above/N; Right/E]
            for pcell in nns |> List.map (tryGetCell g) do g.cells.[r,c] <- pcell |> addNeighbor g.cells.[r,c]
            let occ = g.cells.[r,c].neighbors |> List.tryItem (R.Next(g.cells.[r,c].neighbors.Length))
            match occ with 
            | Some(oc) -> g.cells.[r,c] <- addLink g.cells.[r,c] g.cells.[row oc, col oc]
                          g.cells.[row oc, col oc] <- addLink g.cells.[row oc, col oc] g.cells.[r, c]
            | None -> ()
    g


