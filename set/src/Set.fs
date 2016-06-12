namespace set

open System.Collections.Generic

module Set =
  type CustomSet<'a when 'a : equality>() =
    let hashtable = new Dictionary<'a, 'a>()

    member this.Put(item) =
      if not(hashtable.ContainsKey(item)) then
        hashtable.Add(item, item)

    member this.Remove(item, orElse) =
      if hashtable.ContainsKey(item) then
        ignore(hashtable.Remove(item))
        item
      else
        orElse

    member this.Count =
      hashtable.Count

    member this.Empty =
      this.Count.Equals(0)
