module Sidewinder

open Grid
open Cell
open Utils

let makeSidewinderMaze height width  = 
    let g = makeSimpleGrid height width 
    
    let rec aux (coord: Coord) (l: int) (run: Coord list) =
        let (r, c) = (row coord, col coord)
        if c < l then
            let cell = g.cells.[r,c]
            let augRun = (r,c) :: run
            let atEasternBoundary = not (hasNeighbor cell East)
            let atNorthernBoundary = not (hasNeighbor cell North)
            let shouldCloseOut = atEasternBoundary || (not atNorthernBoundary && R.Next(2) = 0)
            if shouldCloseOut then
                // sample from run and if it has a northern neighbor, link it
                match sample augRun with
                | Some(m) -> let (m_r, m_c) = (row m, col m)
                             if hasNeighbor g.cells.[m_r, m_c] North then
                                g.cells.[m_r,m_c] <- addLink g.cells.[m_r,m_c] g.cells.[m_r-1,m_c]
                                g.cells.[m_r-1,m_c] <- addLink g.cells.[m_r-1,m_c] g.cells.[m_r,m_c]
                | None -> failwith "Should never reach here, something has gone wrong"
                aux (r, c+1) l []
            else 
                // Link to eastern cell (r, c+1)
                g.cells.[r,c] <- addLink g.cells.[r,c] g.cells.[r,c + 1]
                g.cells.[r,c+1] <- addLink g.cells.[r,c+1] g.cells.[r,c]
                aux (r, c+1) l augRun
        else
            ()

    for r in [|0..height-1|] do
        let row = getRow g r
        aux (r, 0) row.Length []
    g

