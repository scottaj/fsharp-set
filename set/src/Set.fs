namespace set

open System
open System.Collections.Generic

module Set =
  type CustomSet<'a when 'a : equality> ()=
    let hashtable = new Dictionary<'a, 'a>()

    static member EmptySet =
      let emptySet = new CustomSet<'a>()
      emptySet

    static member SetOf([<ParamArray>] setMembers : 'a[]) =
      let set = new CustomSet<'a>()
      for setMember in setMembers do
        set.Put(setMember)

      set

    static member Union([<ParamArray>] sets : CustomSet<'a>[]) =
      let unionSet = new CustomSet<'a>()

      for set in sets do
        for setMember in set do
          unionSet.Put(setMember)

      unionSet

    static member Intersection([<ParamArray>] sets : CustomSet<'a>[]) =
      let intersectionSet = new CustomSet<'a>()

      let shortestSet = sets |> Array.minBy(fun s -> s.Count)

      for setMember in shortestSet do
        let memberInAllSets = sets |> Array.forall(fun s -> s.Present(setMember))
        if memberInAllSets then
          intersectionSet.Put(setMember)

      intersectionSet

    member this.Put(item) =
      if not(this.Present(item)) then
        hashtable.Add(item, item)

    member this.Remove(item, orElse) =
      if this.Present(item) then
        ignore(hashtable.Remove(item))
        item
      else
        orElse

    member this.Present(item) =
      hashtable.ContainsKey(item)

    member this.Count =
      hashtable.Count

    member this.Empty =
      this.Count.Equals(0)

    member this.Difference([<ParamArray>] sets : CustomSet<'a>[]) =
      let differenceSet = CustomSet.EmptySet

      for setMember in this do
        let memberInAnyOtherSet = sets |> Array.exists(fun s -> s.Present(setMember))

        if not memberInAnyOtherSet then
          differenceSet.Put(setMember)

      differenceSet

    member this.Subset(otherSet : CustomSet<'a>) =
      this.Difference(otherSet).Empty

    override this.Equals(other) =
      let otherSet = other :?> CustomSet<'a>
      let unionCount = CustomSet.Union(this, otherSet).Count
      this.Count = otherSet.Count && this.Count = unionCount

    interface IEnumerable<'a> with
      member this.GetEnumerator() =
        let keySeq = hashtable |> Seq.map (fun (KeyValue(k,v)) -> k)
        keySeq.GetEnumerator()

    interface System.Collections.IEnumerable with
      member this.GetEnumerator() =
        let keySeq = hashtable |> Seq.map (fun (KeyValue(k,v)) -> k)
        keySeq.GetEnumerator() :> System.Collections.IEnumerator
