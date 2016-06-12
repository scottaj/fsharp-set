# Set Implementation

The problem definition is to implement a set data structure.

No specific interface was given for implementing the set, so I am assuming that I should implement a reasonable subset of common set operations. The operations I plan to implement are:

## Interface

### Basic operations
 * `Put(val)`: add an item to the set
 * `Empty()`: Return true if the set is empty, false otherwise
 * `Present(val)`: Returns true if the value is present in the set, false otherwise
 * `Remove(val, default)`: Return the passed value from the set, if it was present return it, otherwise return default
 * `Count`: Return the number of items in the set

### Operations with other sets
 * `Union(otherSet)`: Return a new set that is the union of this set and otherSet
 * `Intersection(otherSet)`: Return a new set this is the intersection of this set and otherSet
 * `Difference(otherSet)`: Return a new set this is the difference of this set and otherSet
 * `Equals(otherSet)`: Return true if the set is equal to otherSet
 * `Subset(otherSet)`: Return true if the set is a subset of otherSet
 * `StrictSubset(otherSet)`: Return true if the set is a strict subset of otherSet
 * `Superset(otherSet)`: Return true if the set is a superset of otherSet
 * `StrictSuperset(otherSet)`: Return true if the set is a strict superset of otherSet
 
### Iteration

I feel like the above operations are the most interesting. If time permits I would like to implement a standard iteration interface for my set. Since this is usually platform dependent and I'm not sure of the details for .NET, I'm not as worried about this part.

#### Update
I ended up going down this road, and boy was it a doozy. First oof, I had to get my search terms right. Searching for `F# looping` and `f# custom type iteration` just got me basic loop stuff. Eventually, The compiler gave me enough clues to figure out I needed to search for `F# implement IEnumerable`. 

My first problem was that since I am using a map type to back my set, it exposes map enumerators with two values, even if you get the enumerator for just the keys or values. This is more specific than the generic `IEnumerable` enumerator the interface wanted so it would not compile. After some more digging I figured out that if I could convert my key set into a sequence, I could get the enumerator I wanted. [This StackOverflow question](http://stackoverflow.com/questions/1117302/how-to-convert-a-dictionary-into-a-sequence-in-f) pointed me in the right direction there.

This got me far enough to implement the `IEnumerable<'a>` interface for my set's generic type. However it still gave me a compiler error about not implementing the interface. After some more googling I found some a straight forward example [here](https://viralfsharp.com/2012/02/11/implementing-a-stack-in-f/) but could not get my equivilent of it to work. I kept getting various compiler errors telling me I was implementing the IEnumerable interface wrong because it wanted one type param. This had me stalled for a while, until I found [this stack overflow question](http://stackoverflow.com/questions/30128320/custom-ienumerator-in-f) about the related `IEnumerator` interface. Reading the code example finally made me realize that `IEnumerator<T>` and the generic `IEnumerator` were in different packages, which is why it was complaining about me instantiating the generic version (because I had imported the typed version)

Once I figured that out, I was able to get iteration working pretty quickly with my set type.

## Technical

### Technology
I wanted to use either OCaml or F#, because i think they are cool languages I'd like to know more. While I'm not a big Microsoft guy, I decided to go with F# simply because I played with it once before and had a [boilerplate project](https://github.com/scottaj/fsharp-tweet-analyser) and editor setup ready to go for it.

I'm using the mono platform to build and run my code. I have a build script set up with the [FAKE build tool](http://fsharp.github.io/FAKE/). I'm using nuget to manage external dependencies, and [FsUnit](https://github.com/fsprojects/FsUnit) to write tests.

### Implementation details

There are two reasonable ways to implement a set: hash tables, or a sorted sequence (probably a binary search tree, though an array could work too). Hash tables have better performance characteristics (constant time insertion and lookup) and can make creating heterogeneous sets easier. A BST or array can let you sort the set though which may be desirable if you're going to iterate over it. This comes at the cost of logarithmic access and insertion instead of constant time.

My initial plan is to use the prebuilt Dictionary data structure that the .NET framework comes with to back my set implementation and provide the above described interface. If time permits I may also look into implementing the backing data structure, although if I did that I would probably do the BST approach instead of making my own hash table.

One additional implementation detail is that these sets are homogeneous, all of the members must be of the same type which is defined at instantiation time.

## Building and running
To start with, you need to install make, mono, and nuget. On a mac, make is preinstalled and you can get the others with homebrew: `brew install mono nuget`.

With that done, you should be able to simply run `make` from the project root. You should see compiler output ending in the results of the automated test run.
