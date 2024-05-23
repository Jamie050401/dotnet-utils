namespace Utils.FSharp

type Dictionary<'TKey, 'TValue> = System.Collections.Generic.Dictionary<'TKey, 'TValue>

module Dictionary =
    let add key value (dictionary: Dictionary<'TKey, 'TValue>) =
        dictionary.Add (key, value)

    let remove key (dictionary: Dictionary<'TKey, 'TValue>) =
        dictionary.Remove key

    let exists key (dictionary: Dictionary<'TKey, 'TValue>) =
        dictionary.ContainsKey key

    let create (data: KeyValuePair<'TKey, 'TValue> seq) =
#if NET48
        let dictionary = Dictionary<'TKey, 'TValue> ()
        data |> Seq.iter (fun (keyValuePair: KeyValuePair<'TKey, 'TValue>) ->
            dictionary |> add keyValuePair.Key keyValuePair.Value
        )
        dictionary
#else
        new Dictionary<'TKey, 'TValue> (data)
#endif
