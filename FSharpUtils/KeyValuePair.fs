module Utils.FSharp.KeyValuePair

type KeyValuePair<'TKey, 'TValue> = System.Collections.Generic.KeyValuePair<'TKey, 'TValue>

module KeyValuePair =
    // TODO: Add unit tests
    let create (key: 'TKey) (value: 'TValue) =
#if NET48
        System.Collections.Generic.KeyValuePair<'TKey, 'TValue> (key, value)
#else
        System.Collections.Generic.KeyValuePair.Create (key, value)
#endif

    // TODO: Add unit tests
    let ofTuple (key: 'TKey, value: 'TValue) =
        create key value

    // TODO: Add unit tests
    let toTuple (keyValuePair: KeyValuePair<'TKey, 'TValue>) =
        keyValuePair.Key, keyValuePair.Value
