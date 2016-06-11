namespace setTests

open NUnit.Framework
open FsUnit

[<TestFixture>]
type ``smoke test`` ()=
  let testString = "a simple test"

  [<Test>]
  member x.``sanity`` ()=
    testString |> should equal "a simple test"
