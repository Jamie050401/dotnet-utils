module Utils.FSharp.KeyValuePair

type KeyValuePair<'TKey, 'TValue> = System.Collections.Generic.KeyValuePair<'TKey, 'TValue>

let create (key: 'TKey) (value: 'TValue) =
#if NET48
    System.Collections.Generic.KeyValuePair<'TKey, 'TValue> (key, value)
#else
    System.Collections.Generic.KeyValuePair.Create (key, value)
#endif

let ofTuple (key: 'TKey, value: 'TValue) =
    create key value

let toTuple (keyValuePair: KeyValuePair<'TKey, 'TValue>) =
    keyValuePair.Key, keyValuePair.Value
