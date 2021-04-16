module AC.Modulo

let inline modPown (mod': 'a) (x: 'a) n =
    let rec f (acc: 'a) (x: 'a) n =
        if n > 0 then
            match n % 2 with
            | 0 -> f acc ((x * x) % mod') (n >>> 1)
            | _ -> f ((acc * x) % mod') ((x * x) % mod') (n >>> 1)
        else
            acc

    f LanguagePrimitives.GenericOne x n
