namespace setTests

open NUnit.Framework
open FsUnit

open set.Set

[<TestFixture>]
type ``SetOf`` ()=
  let testSet = CustomSet.SetOf("a", "b", "c")

  [<Test>]
  member test.``creates a new CustomSet with the provided arguments as members`` ()=
    testSet.Count |> should equal 3
    testSet.Remove("b", "") |> should equal "b"

  [<Test>]
  member test.``handles duplicates gracefully`` ()=
    let willNotHaveDuplicates = CustomSet.SetOf(1, 1, 2, 3, 2, 3)
    willNotHaveDuplicates.Count |> should equal 3

[<TestFixture>]
type ``#Put`` ()=
  let testSet = CustomSet.EmptySet

  [<Test>]
  member test.``does not cause error when putting the same key twice`` ()=
    testSet.Put(5)
    testSet.Put(5)
    testSet.Count |> should equal 1

[<TestFixture>]
type ``#Remove`` ()=
  let testSet = CustomSet.SetOf(7)

  [<Test>]
  member test.``returns the removed item if it was present`` ()=
    testSet.Put(7)
    testSet.Remove(7, -1) |> should equal 7

  [<Test>]
  member test.``returns the default if the item to remove is not present`` ()=
    testSet.Put(7)
    testSet.Remove(3, -1) |> should equal -1

[<TestFixture>]
type ``#Present`` ()=
  let testSet = CustomSet.SetOf(5)

  [<Test>]
  member test.``returns true of the item is present`` ()=
    testSet.Present(5) |> should be True

  [<Test>]
  member test.``returns false if the item is absent`` ()=
    testSet.Present(10) |> should be False

  [<Test>]
  member test.``updates as items are added and removed`` ()=
    testSet.Put(3)
    testSet.Present(3) |> should be True
    ignore(testSet.Remove(3, 0))
    testSet.Present(3) |> should be False

[<TestFixture>]
type ``#Count`` ()=
  let testSet = CustomSet.EmptySet

  [<Test>]
  member test.``returns the number of items in the set`` ()=
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
  member test.``is true when first created`` ()=
    testSet.Empty |> should be True

  [<Test>]
  member test.``changes status as items are added and removed`` ()=
    testSet.Put("Hello")
    testSet.Empty |> should be False
    ignore(testSet.Remove("Hello", ""))
    testSet.Empty |> should be True


[<TestFixture>]
type ``Union`` ()=
  let set1 = CustomSet.SetOf(1, 2, 3)
  let set2 = CustomSet.SetOf(3, 4, 5)
  let set3 = CustomSet.SetOf(5, 6, 7)

  [<Test>]
  member test.``returns a new set with all the items from all the sets`` ()=
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
  member test.``returns an identical set when you union a set with itself`` ()=
    let unionSet = CustomSet.Union(set1, set1)
    unionSet.Count |> should equal 3
    unionSet.Present(1) |> should be True
    unionSet.Present(2) |> should be True
    unionSet.Present(3) |> should be True

  [<Test>]
  member test.``returns an identical set when you union with an empty set`` ()=
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
  member test.``returns an empty set when you union two empty sets`` ()=
    let unionSet = CustomSet.Union(CustomSet.EmptySet, CustomSet.EmptySet)
    unionSet.Empty |> should be True

[<TestFixture>]
type ``Intersection`` ()=
  let set1 = CustomSet.SetOf(1, 2, 3, 4)
  let set2 = CustomSet.SetOf(4, 5, 6, 7)
  let set3 = CustomSet.SetOf(2, 4, 6, 8)

  [<Test>]
  member test.``returns a new set with the common items from all the provided sets`` ()=
    let intersection = CustomSet.Intersection(set1, set3)
    intersection.Count |> should equal 2
    intersection.Present(2) |> should be True
    intersection.Present(4) |> should be True

    let intersection2 = CustomSet.Intersection(intersection, set2)
    intersection2.Count |> should equal 1
    intersection2.Present(4) |> should be True

  [<Test>]
  member test.``the intersection of a set with itself is the set`` ()=
    let intersection = CustomSet.Intersection(set1, set1)
    intersection.Count |> should equal 4
    intersection.Present(1) |> should be True
    intersection.Present(2) |> should be True
    intersection.Present(3) |> should be True
    intersection.Present(4) |> should be True

  [<Test>]
  member test.``this intersection of two disjoint sets is the empty set`` ()=
    let intersection = CustomSet.Intersection(set1, CustomSet.SetOf(5, 6))
    intersection.Empty |> should be True

  [<Test>]
  member test.``the intersection of any set and the empty set is the empty set`` ()=
    let intersection = CustomSet.Intersection(set1, CustomSet.EmptySet)
    intersection.Empty |> should be True

[<TestFixture>]
type ``#Difference`` ()=
  let set1 = CustomSet.SetOf(1, 2, 3, 4)
  let set2 = CustomSet.SetOf(2, 4, 6, 8)
  let set3 = CustomSet.SetOf(5, 6, 7, 8)

  [<Test>]
  member test.``returns the items in the set that are not in the other set`` ()=
    let differenceSet = set1.Difference(set2)
    differenceSet.Count |> should equal 2
    differenceSet.Present(1) |> should be True
    differenceSet.Present(3) |> should be True

  [<Test>]
  member test.``is not communative`` ()=
    let differenceSet = set1.Difference(set2, set3)
    differenceSet.Count |> should equal 2
    differenceSet.Present(1) |> should be True
    differenceSet.Present(3) |> should be True

    let differenceSet = set3.Difference(set2, set1)
    differenceSet.Count |> should equal 2
    differenceSet.Present(5) |> should be True
    differenceSet.Present(7) |> should be True

  [<Test>]
  member test.``the difference of any set and itself is the empty set`` ()=
    let differenceSet = set1.Difference(set1)
    differenceSet.Empty |> should be True

  [<Test>]
  member test.``the difference of any set and the empty set is the starting set`` ()=
    let differenceSet = set1.Difference(CustomSet.EmptySet)
    differenceSet.Count |> should equal 4
    differenceSet.Present(1) |> should be True
    differenceSet.Present(2) |> should be True
    differenceSet.Present(3) |> should be True
    differenceSet.Present(4) |> should be True

  [<Test>]
  member test.``the difference of the empty set with any other set is the empty set`` ()=
    let differenceSet = CustomSet.EmptySet.Difference(set1)
    differenceSet.Empty |> should be True
