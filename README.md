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
 * `Union(otherSet)`: Return the union of this set and otherSet
 * `Intersection(otherSet)`: Return the intersection of this set and otherSet
 * `Difference(otherSet)`: Return the difference of this set and otherSet
 * `Equals(otherSet)`: Return true if the set is equal to otherSet
 * `Subset(otherSet)`: Return true if the set is a subset of otherSet
 * `StrictSubset(otherSet)`: Return true if the set is a strict subset of otherSet
 * `Superset(otherSet)`: Return true if the set is a superset of otherSet
 * `StrictSuperset(otherSet)`: Return true if the set is a strict superset of otherSet
 
### Iteration

I feel like the above operations are the most interesting. If time permits I would like to implement a standard iteration interface for my set. Since this is usually platform dependent and I'm not sure of the details for .NET, I'm not as worried about this part.

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
