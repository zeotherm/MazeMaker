module Sidewinder

open Utils
open ListGrid

let makeSidewinderMaze inp_height inp_width = 
    let g = makeGrid inp_height inp_width
    let rec colProcess (m: SquareGrid) (loc: Coord) (l: int) (run: Coord list): SquareGrid = 
        let (r, c) = (row loc, col loc)
        if c <= l then
            let cell = getCell m loc
            let augRun = (r, c) :: run
            let ns = validNeighbors m loc [North; East]
            let atEasternBoundary = not (List.contains East ns)
            let atNorthernBoundary = not (List.contains North ns)
            if atEasternBoundary || (not atNorthernBoundary && sample [true; false]) then
                let randCoord = sample augRun
                if hasNeighbor g randCoord North then
                    colProcess (linkCell m randCoord North) (r, c+1) l []
                else
                    colProcess m (r, c+1) l []
            else
                colProcess (linkCell m (coord cell.Value) East) (r, c+1) l augRun
        else
            m
    let rec rowProcess (m: SquareGrid) (rowNum: int): SquareGrid =
        let h = height m
        if rowNum > h then
            m
        else
            rowProcess (colProcess m (rowNum, 0) (width m) []) (rowNum + 1)

    rowProcess g 0
                    
                