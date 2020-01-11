module Grid
open Cell

type Grid = { height: int; 
              width: int; 
              cells: Cell [,] }

let makeEmptyGrid c r = { height = r;
                          width = c;
                          cells = Array2D.init c r (fun i j -> makeCell (i,j)) }