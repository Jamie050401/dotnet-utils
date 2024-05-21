module Utils.FSharp.Math

/// Safe division function that will return Some/None instead of a potential NaN/DivideByZeroException
let inline (/*/) (numerator: 'a) (denominator: 'a) =
    match denominator = LanguagePrimitives.GenericZero with
    | true ->
        None
    | false ->
        Some <| numerator / denominator

let private rnd func adj precision number =
    number * 10.0 ** precision + adj |> func |> (/) <| 10.0 ** precision |> (*) (sign number |> float)

let round precision number =
    rnd floor 0.5 precision number

let roundUp precision number =
    rnd ceil -0.0000000000001 precision number

let roundDown precision number =
    rnd floor -0.0000000000001 precision number
