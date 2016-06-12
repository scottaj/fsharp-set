namespace setTests

open NUnit.Framework
open FsUnit

open set.Set

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
    ignore(testSet.Remove("Hello"))
    testSet.Empty |> should be True
