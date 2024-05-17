namespace MazeSolver;

class Program
{
    static void Main()
    {
        // Read the maze from a text file
        string? maze11x11 = "D:\\RTU\\Algorithms\\maze-solver\\MazeSolver\\MazeSolver\\Mazes\\maze_11x11.txt";
        string? maze31x31 = "D:\\RTU\\Algorithms\\maze-solver\\MazeSolver\\MazeSolver\\Mazes\\maze_31x31.txt";
        string? maze101x101 = "D:\\RTU\\Algorithms\\maze-solver\\MazeSolver\\MazeSolver\\Mazes\\maze_101x101.txt";
            
        string[] mazeLines = File.ReadAllLines(maze11x11);
        char[,] maze = new char[mazeLines.Length, mazeLines[0].Length];
        for (int i = 0; i < mazeLines.Length; i++)
        {
            for (int j = 0; j < mazeLines[i].Length; j++)
            {
                maze[i, j] = mazeLines[i][j];
            }
        }

        // Find the starting point and goal
        var start = MazeUtilities.FindPoint(maze, 'S');
        var goal = MazeUtilities.FindPoint(maze, 'G');

        // Run A* algorithm
        var result = AStarSolver.Solve(maze, start, goal);

        if (result != null)
        {
            var (path, coins, nodesExpanded, maxOpenSetSize) = result.Value;

            Console.WriteLine("Path found with coins collected: " + coins);
            Console.WriteLine("Path:");
            foreach (var step in path)
            {
                Console.WriteLine($"({step.x}, {step.y})");
            }

            // Calculate and display time and space complexity
            int totalNodes = maze.GetLength(0) * maze.GetLength(1);
            Console.WriteLine(ComplexityCalculator.CalculateTimeComplexity(nodesExpanded, totalNodes));
            Console.WriteLine(ComplexityCalculator.CalculateSpaceComplexity(maxOpenSetSize, maze.GetLength(0), maze.GetLength(1)));
        }
        else
        {
            Console.WriteLine("No path found.");
        }
    }
}