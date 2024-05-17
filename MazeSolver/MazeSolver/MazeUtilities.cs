namespace MazeSolver
{
    static class MazeUtilities
    {
        // Find the coordinates of a specific point in the maze
        public static (int x, int y) FindPoint(char[,] maze, char point)
        {
            // Iterate through each cell in the maze
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    // If the cell matches the point, return its coordinates
                    if (maze[i, j] == point)
                    {
                        return (i, j);
                    }
                }
            }

            // If the point is not found, throw an exception
            throw new Exception("Point not found in maze");
        }

        // Heuristic function for A* algorithm, calculating distance between two points
        public static int Heuristic((int x, int y) a, (int x, int y) b)
        {
            // Calculate and return distance
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        // Calculate the total number of coins collected on a given path
        public static int CollectCoins(char[,] maze, List<(int x, int y)> path)
        {
            int coins = 0;
            // Iterate through each step in the path
            foreach (var step in path)
            {
                char cell = maze[step.x, step.y];
                // If the cell contains a digit, add its value to the coin total
                if (char.IsDigit(cell))
                {
                    coins += cell - '0'; // Convert character to integer and add to coins
                }
            }

            // Return the total number of coins collected
            return coins;
        }
    }
}