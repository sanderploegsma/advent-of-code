namespace Common

module Collections =
    /// Create combinations of the input list with length `n`
    let combinations n l =
        // Source: https://stackoverflow.com/a/1231711
        let rec comb n' l' =
            match n', l' with
            | 0, _ -> [[]]
            | _, [] -> []
            | k, (x::xs) -> List.map ((@) [x]) (comb (k-1) xs) @ comb k xs

        match n, l with
        | k, _ when k < 0 -> failwith "n should be between 0 and len(l)"
        | k, _ when k > List.length l -> failwith "n should be between 0 and len(l)"
        | _, _ -> comb n l

    /// Create all possible sublists of the input list
    let rec sublists l =
        // Source: https://stackoverflow.com/a/51343582
        match l with
        | [] -> [[]]
        | h::t -> List.fold (fun ys s -> (h::s)::s::ys) [] (sublists t)
