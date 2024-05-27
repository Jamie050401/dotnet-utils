module Utils.FSharp.Tree

type Node<'T> = {
    Value: 'T
}

type Branch<'T> =
    | Branch of Node<'T> * Branch<'T> array
    | Node of Node<'T>

let rec private _iter arrayFunc func tree =
    tree |> arrayFunc (fun branch ->
        match branch with
        | Node node ->
            func node
        | Branch (node, tree) ->
            func node
            tree |> _iter arrayFunc func
    )

// TODO: Needs unit tests
let rec iter func tree =
    tree |> _iter Array.iter func

module Parallel =
    // TODO: Needs unit tests
    let rec iter func tree =
        tree |> _iter Array.Parallel.iter func
