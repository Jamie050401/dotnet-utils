namespace Utils.FSharp

type KeyValuePair<'TKey, 'TValue> = System.Collections.Generic.KeyValuePair<'TKey, 'TValue>

module KeyValuePair =
    let create (key: 'TKey) (value: 'TValue) =
#if NET48
        new System.Collections.Generic.KeyValuePair<'TKey, 'TValue> (key, value)
#else
        System.Collections.Generic.KeyValuePair.Create (key, value)
#endif

    let ofTuple (key: 'TKey, value: 'TValue) =
        create key value

    let toTuple (keyValuePair: KeyValuePair<'TKey, 'TValue>) =
        keyValuePair.Key, keyValuePair.Value
