using UnityEngine;
using System.Collections.Generic;

public class PikachuPathfinding : MonoBehaviour
{
    private int[,] grid;  // Grid representation of the game board
    private int gridSizeX;  // Number of columns in the grid
    private int gridSizeY;  // Number of rows in the grid

    private Vector2Int startPosition;  // Starting position for the pathfinding
    private Vector2Int targetPosition;  // Target position to reach

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private void Start()
    {
        // Initialize the grid with your game board layout
        gridSizeX = 8;  // Change this value based on your game board
        gridSizeY = 8;  // Change this value based on your game board

        grid = new int[gridSizeX, gridSizeY];

        // Set the start and target positions
        startPosition = new Vector2Int(0, 0);  // Change this value based on your game board
        targetPosition = new Vector2Int(7, 7);  // Change this value based on your game board

        // Call the pathfinding algorithm
        List<Vector2Int> path = FindPath(startPosition, targetPosition);

        // Use the generated path as needed (e.g., move the Pikachu along the path)
        if (path != null)
        {
            foreach (Vector2Int position in path)
            {
                // Move the Pikachu to the position in the path
                // Your code here...
            }
        }
        else
        {
            Debug.Log("Path not found!");
        }
    }

    private List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, int> turnsCount = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        turnsCount[start] = 0;
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == target)
                break;

            int currentTurns = turnsCount[current];

            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighbor = current + direction;
                int neighborTurns = currentTurns;

                if (IsValidPosition(neighbor))
                {
                    // Check if turning at this position
                    if (direction != (target - current))
                        neighborTurns++;

                    // Check if the maximum turns limit has been reached
                    if (neighborTurns <= 2 && !turnsCount.ContainsKey(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        turnsCount[neighbor] = neighborTurns;
                        cameFrom[neighbor] = current;
                    }
                }
            }
        }

        if (cameFrom.ContainsKey(target))
        {
            List<Vector2Int> path = new List<Vector2Int>();
            Vector2Int current = target;

            while (current != start)
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Reverse();
            return path;
        }

        return null;
    }

    private bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < gridSizeX &&
               position.y >= 0 && position.y < gridSizeY;
    }
}
