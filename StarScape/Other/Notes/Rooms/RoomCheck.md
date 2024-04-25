
on tile placed:
    if the tile placed is a wall (some kind of divider)
    room check on the tiles adjacent.


room check:
    let D: Dictionary<pos, bool> represent (position, checked)
    
    add pos to queue
    
    while queue isn't empty

        let pos = dequeue

        if pos isn't in ship tilemap, it must be space so can't be a room:
            return
        if pos is a wall (some kind of divider):
            remove pos from D
            continue
        if t.pos is airtight:
            let up      = pos + ( 0,  1 )
            let right   = pos + ( 0,  1 )
            let down    = pos + ( 0,  1 )
            let left    = pos + ( 0,  1 )
            
            queue(up)
            queue(right)
            queue(down)
            queue(left)
            
            add pos to checked tiles
    
    return D.keys