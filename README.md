# Hide and Seek Game

Hide and seek game with Graph and DFS Algorithm.

## PROBLEM DESCRIPTION

Oliver and Bob are best friends. They have spent their entire childhood in the beautiful city of Byteland. The people of Byteland live happily along with the King.
The city has a unique architecture with total N houses. The King's Mansion is a very big and beautiful bungalow having address = 1. Rest of the houses in Byteland have some unique address, (say A), are connected by roads and there is always a unique path between any two houses in the city. Note that the King's Mansion is also included in these houses.

Oliver and Bob have decided to play Hide and Seek taking the entire city as their arena. In the given scenario of the game, it's Oliver's turn to hide and Bob is supposed to find him.
Oliver can hide in any of the houses in the city including the King's Mansion. As Bob is a very lazy person, for finding Oliver, he either goes towards the King's Mansion (he stops when he reaches there), or he moves away from the Mansion in any possible path till the last house on that path.

Oliver runs and hides in some house (say X) and Bob is starting the game from his house (say Y). If Bob reaches house X, then he surely finds Oliver.

Given Q queries, you need to tell Bob if it is possible for him to find Oliver or not.

The queries can be of the following two types:
0 X Y : Bob moves towards the King's Mansion.
1 X Y : Bob moves away from the King's Mansion

INPUT :
The first line of the input contains a single integer N, total number of houses in the city. Next N-1 lines contain two space separated integers A and B denoting a road between the houses at address A and B.
Next line contains a single integer Q denoting the number of queries.
Following Q lines contain three space separated integers representing each query as explained above.

OUTPUT :
Print "YES" or "NO" for each query depending on the answer to that query.

CONSTRAINTS :
1 ≤ N ≤ 10^5
1 ≤ A,B ≤ N
1 ≤ Q ≤ 5*10^5
1 ≤ X,Y ≤ N

### Prerequisites

What things you need to install:

```
C# IDE and Compiler
```

## Running the tests

Run the program,
Sample input:

```
9
1 2
1 3
2 6
2 7
6 9
7 8
3 4
3 5
5
0 2 8
1 2 8
1 6 5
0 6 5
1 9 1
```

Sample output
```
YES
NO
NO
NO
YES
```

## Built With

* [VISUAL STUDIO COMMUNITY 2017](https://visualstudio.microsoft.com) - IDE

## Contributing

**Harry Rahmadi Munly 13517033**

**Muhammad Hendry Prasetya 13517105** - *Backend Programmer* - [GitHub Profile](https://github.com/hendpraz)

**Sekar Larasati Muslimah 13517xxx**

## Oliver and the Game Case

Try to solve this one [OLIVER n THE GAME](https://www.hackerearth.com/practice/algorithms/graphs/topological-sort/practice-problems/algorithm/oliver-and-the-game-3/description)

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration : Geeksforgeeks, Stackoveflow etc.

