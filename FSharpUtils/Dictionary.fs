module Utils.FSharp.Dictionary

open KeyValuePair

type Dictionary<'TKey, 'TValue> = System.Collections.Generic.Dictionary<'TKey, 'TValue>

let add key value (dictionary: Dictionary<'TKey, 'TValue>) =
    dictionary.Add (key, value)

let remove key (dictionary: Dictionary<'TKey, 'TValue>) =
    dictionary.Remove key

let exists key (dictionary: Dictionary<'TKey, 'TValue>) =
    dictionary.ContainsKey key

let create (data: KeyValuePair<'TKey, 'TValue> seq) =
    Dictionary<'TKey, 'TValue> (data)
