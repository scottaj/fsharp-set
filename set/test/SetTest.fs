namespace setTests

open NUnit.Framework
open FsUnit

open set.Set

[<TestFixture>]
type ``SetOf`` ()=
  let testSet = CustomSet.SetOf("a", "b", "c")

  [<Test>]
  member x.``creates a new CustomSet with the provided arguments as members`` ()=
    testSet.Count |> should equal 3
    testSet.Remove("b", "") |> should equal "b"

  [<Test>]
  member x.``handles duplicates gracefully`` ()=
    let willNotHaveDuplicates = CustomSet.SetOf(1, 1, 2, 3, 2, 3)
    willNotHaveDuplicates.Count |> should equal 3

[<TestFixture>]
type ``#Put`` ()=
  let testSet = CustomSet.EmptySet

  [<Test>]
  member x.``does not cause error when putting the same key twice`` ()=
    testSet.Put(5)
    testSet.Put(5)
    testSet.Count |> should equal 1

[<TestFixture>]
type ``#Remove`` ()=
  let testSet = CustomSet.SetOf(7)

  [<Test>]
  member x.``returns the removed item if it was present`` ()=
    testSet.Put(7)
    testSet.Remove(7, -1) |> should equal 7

  [<Test>]
  member x.``returns the default if the item to remove is not present`` ()=
    testSet.Put(7)
    testSet.Remove(3, -1) |> should equal -1

[<TestFixture>]
type ``#Present`` ()=
  let testSet = CustomSet.SetOf(5)

  [<Test>]
  member x.``returns true of the item is present`` ()=
    testSet.Present(5) |> should be True

  [<Test>]
  member x.``returns false if the item is absent`` ()=
    testSet.Present(10) |> should be False

  [<Test>]
  member x.``updates as items are added and removed`` ()=
    testSet.Put(3)
    testSet.Present(3) |> should be True
    ignore(testSet.Remove(3, 0))
    testSet.Present(3) |> should be False

[<TestFixture>]
type ``#Count`` ()=
  let testSet = CustomSet.EmptySet

  [<Test>]
  member x.``returns the number of items in the set`` ()=
    testSet.Count |> should equal 0
    testSet.Put(5)
    testSet.Count |> should equal 1
    testSet.Put(10)
    testSet.Count |> should equal 2
    testSet.Put(5)
    testSet.Count |> should equal 2
    ignore(testSet.Remove(5, 0))
    testSet.Count |> should equal 1

[<TestFixture>]
type ``#Empty`` ()=
  let testSet = CustomSet.EmptySet

  [<Test>]
  member x.``is true when first created`` ()=
    testSet.Empty |> should be True

  [<Test>]
  member x.``changes status as items are added and removed`` ()=
    testSet.Put("Hello")
    testSet.Empty |> should be False
    ignore(testSet.Remove("Hello", ""))
    testSet.Empty |> should be True


[<TestFixture>]
type ``#Union`` ()=
  let set1 = CustomSet.SetOf(1, 2, 3)
  let set2 = CustomSet.SetOf(3, 4, 5)
  let set3 = CustomSet.SetOf(5, 6, 7)

  [<Test>]
  member x.``returns a new set with all the items from all the sets`` ()=
    let unionSet = CustomSet.Union(set1, set2, set3)
    unionSet.Count |> should equal 7
    unionSet.Present(1) |> should be True
    unionSet.Present(2) |> should be True
    unionSet.Present(3) |> should be True
    unionSet.Present(4) |> should be True
    unionSet.Present(5) |> should be True
    unionSet.Present(6) |> should be True
    unionSet.Present(7) |> should be True

  [<Test>]
  member x.``returns an identical set when you union a set with itself`` ()=
    let unionSet = CustomSet.Union(set1, set1)
    unionSet.Count |> should equal 3
    unionSet.Present(1) |> should be True
    unionSet.Present(2) |> should be True
    unionSet.Present(3) |> should be True

  [<Test>]
  member x.``returns an identical set when you union with an empty set`` ()=
    let unionSet = CustomSet.Union(set1, CustomSet.EmptySet)
    unionSet.Count |> should equal 3
    unionSet.Present(1) |> should be True
    unionSet.Present(2) |> should be True
    unionSet.Present(3) |> should be True
    let unionSet2 = CustomSet.Union(CustomSet.EmptySet, set1)
    unionSet2.Count |> should equal 3
    unionSet2.Present(1) |> should be True
    unionSet2.Present(2) |> should be True
    unionSet2.Present(3) |> should be True

  [<Test>]
  member x.``returns an empty set when you union two empty sets`` ()=
    let unionSet = CustomSet.Union(CustomSet.EmptySet, CustomSet.EmptySet)
    unionSet.Empty |> should be True
