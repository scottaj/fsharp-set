namespace set

open System.Collections.Generic

module Set =
  type CustomSet<'a when 'a : equality>() =
    let hashtable = new Dictionary<'a, 'a>()

    member this.Empty =
      hashtable.Count.Equals(0)

    member this.Put(item) =
      hashtable.Add(item, item)

    member this.Remove(item) =
      ignore(hashtable.Remove(item))
      item
