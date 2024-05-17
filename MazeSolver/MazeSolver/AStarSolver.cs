using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    static class AStarSolver
    {
        // Solves the maze using the A* algorithm
        public static (List<(int x, int y)> path, int coins, int nodesExpanded, int maxOpenSetSize)? Solve(char[,] maze,
            (int x, int y) start, (int x, int y) goal)
        {
            int rows = maze.GetLength(0);
            int cols = maze.GetLength(1);
            int[,] cost = new int[rows, cols];

            // Initialize cost array with maximum values
            for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                cost[i, j] = int.MaxValue;

            // Define possible movement directions, including diagonals
            var directions = new (int x, int y)[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            // Initialize open set and cameFrom dictionary
            var openSet = new SortedSet<(int fScore, (int x, int y) point)>();
            var cameFrom = new Dictionary<(int x, int y), (int x, int y)>();

            int nodesExpanded = 0; // Track the number of nodes expanded
            int maxOpenSetSize = 0; // Track the maximum size of the open set

            cost[start.x, start.y] = 0; // Cost from start to start is zero
            openSet.Add((MazeUtilities.Heuristic(start, goal), start)); // Add start to the open set

            while (openSet.Any())
            {
                // Update the maximum size of the open set
                if (openSet.Count > maxOpenSetSize)
                    maxOpenSetSize = openSet.Count;

                // Get the node with the lowest fScore from the open set
                var current = openSet.First().point;
                openSet.Remove(openSet.First());

                // If the goal is reached, reconstruct the path and return the result
                if (current == goal)
                {
                    var path = ReconstructPath(cameFrom, current);
                    var coins = MazeUtilities.CollectCoins(maze, path);
                    return (path, coins, nodesExpanded, maxOpenSetSize);
                }

                nodesExpanded++; // Increment the number of nodes expanded

                // Iterate over each possible direction
                foreach (var direction in directions)
                {
                    (int x, int y) neighbor = (current.x + direction.x, current.y + direction.y);

                    // Check if neighbor is within bounds and not a wall
                    if (neighbor.x >= 0 && neighbor.x < rows && neighbor.y >= 0 && neighbor.y < cols &&
                        maze[neighbor.x, neighbor.y] != 'X')
                    {
                        int tentativeCost = cost[current.x, current.y] + 1; // Cost of moving to neighbor

                        // If a cheaper path to neighbor is found
                        if (tentativeCost < cost[neighbor.x, neighbor.y])
                        {
                            cameFrom[neighbor] = current; // Update path to neighbor
                            cost[neighbor.x, neighbor.y] = tentativeCost; // Update cost to neighbor
                            int fScore = tentativeCost + MazeUtilities.Heuristic(neighbor, goal); // Calculate fScore
                            openSet.Add((fScore, neighbor)); // Add neighbor to open set
                        }
                    }
                }
            }

            return null; // Return null if no path is found
        }

        // Reconstructs the path from the start to the goal
        private static List<(int x, int y)> ReconstructPath(Dictionary<(int x, int y), (int x, int y)> cameFrom,
            (int x, int y) current)
        {
            var totalPath = new List<(int x, int y)> { current }; // Initialize path with goal node
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current]; // Move to the predecessor of the current node
                totalPath.Add(current); // Add the predecessor to the path
            }

            totalPath.Reverse(); // Reverse the path to get it from start to goal
            return totalPath; // Return the reconstructed path
        }
    }
}