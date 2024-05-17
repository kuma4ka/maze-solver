namespace MazeSolver;

public class ComplexityCalculator
{
    // Calculates the time complexity based on the number of nodes expanded and the size of the maze
    public static string CalculateTimeComplexity(int nodesExpanded, int totalNodes)
    {
        return $"Time Complexity: O({nodesExpanded} expanded nodes out of {totalNodes} total nodes)";
    }

    // Calculates the space complexity based on the size of the open set and the maze dimensions
    public static string CalculateSpaceComplexity(int maxOpenSetSize, int rows, int cols)
    {
        return $"Space Complexity: O({maxOpenSetSize} nodes in the open set, maze size {rows}x{cols})";
    }
}