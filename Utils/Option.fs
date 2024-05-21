module Utils.Option

let setDefault defaultValue optionValue =
    match optionValue with
    | Some value -> value
    | None       -> defaultValue
