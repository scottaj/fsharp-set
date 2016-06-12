namespace setTests

open NUnit.Framework
open FsUnit

open set.Set

[<TestFixture>]
type ``#Put`` ()=
  let testSet = new CustomSet<int>()

  [<Test>]
  member x.``does cause error when putting the same key twice`` ()=
    testSet.Put(5)
    testSet.Put(5)
    testSet.Count |> should equal 1

[<TestFixture>]
type ``#Remove`` ()=
  let testSet = new CustomSet<int>()

  [<Test>]
  member x.``returns the removed item if it was present`` ()=
    testSet.Put(7)
    testSet.Remove(7, -1) |> should equal 7

  [<Test>]
  member x.``returns the default if the item to remove is not present`` ()=
    testSet.Put(7)
    testSet.Remove(3, -1) |> should equal -1

[<TestFixture>]
type ``#Empty`` ()=
  let testSet = new CustomSet<string>()

  [<Test>]
  member x.``set is empty when first created`` ()=
    testSet.Empty |> should be True

  [<Test>]
  member x.``set changes emptiness status as items are added and removed`` ()=
    testSet.Put("Hello")
    testSet.Empty |> should be False
    ignore(testSet.Remove("Hello", ""))
    testSet.Empty |> should be True
