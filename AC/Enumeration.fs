module AC.Enumeration

let rec permutations (xs: 'a list): 'a list list =
    let rec f =
        function
        | [] -> []
        | x :: xs -> (x, xs) :: [ for y, ys in f xs -> y, x :: ys ]

    match xs with
    | [] -> [ [] ]
    | xs ->
        [ for y, ys in f xs do
            for zs in permutations ys do
                y :: zs ]

let rec combination (n: int) (xs: 'a list): 'a list list =
    assert (n >= 0)

    if n = 0 then
        [ [] ]
    elif n = xs.Length then
        [ xs ]
    else
        let rec f =
            function
            | [] -> []
            | x :: xs -> (x, xs) :: [ for y, ys in f xs -> y, ys ]

        [ for y, ys in f xs do
            for zs in combination (n - 1) ys do
                y :: zs ]
