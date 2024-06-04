module Utils.FSharp.KeyValuePair

type KeyValuePair<'TKey, 'TValue> = System.Collections.Generic.KeyValuePair<'TKey, 'TValue>

let create (key: 'TKey) (value: 'TValue) =
    System.Collections.Generic.KeyValuePair.Create (key, value)

let ofTuple (key: 'TKey, value: 'TValue) =
    create key value

let toTuple (keyValuePair: KeyValuePair<'TKey, 'TValue>) =
    keyValuePair.Key, keyValuePair.Value
