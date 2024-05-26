module Utils.FSharp.Tree

type Node<'T> = {
    Value: 'T
}

type Branch<'T> =
    | Branch of Node<'T> * Branch<'T> array
    | Node of Node<'T>

module Tree =
    // TODO: Needs unit tests
    let rec iter func tree =
        tree |> Array.iter (fun branch ->
            match branch with
            | Node node ->
                func node
            | Branch (node, tree) ->
                func node
                tree |> iter func
        )
