using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;       // Reference to the ScriptableObject holding obstacle data
    public GameObject obstaclePrefab;       // Prefab used to instantiate obstacles
    public static ObstacleManager instance; // Static reference to the ObstacleManager instance
    public int gridSize = 10;              // Define your grid size here

    void Awake()
    {
        instance = this;  // Set the static instance reference when the object awakens
    }

    void Start()
    {
        GenerateObstacles();  // Generate obstacles at the start of the game
    }

    void GenerateObstacles()
    {
        // Loop through each cell in the grid
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                int i = x + y * gridSize;  // Calculate the index in the 1D array
                if (obstacleData.blockedTiles[i])  // Check if the current tile is blocked
                {
                    // Instantiate an obstacle prefab at the corresponding grid position
                    Instantiate(obstaclePrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                }
            }
        }
    }

    // Method to check if a tile at the given position is blocked
    public static bool IsTileBlocked(Vector2Int position)
    {
        if (instance == null) return false;  // Return false if the instance is null (safety check)

        // Calculate the index in the 1D array based on the grid position
        int index = position.x + position.y * instance.gridSize;

        // Check if the index is within bounds
        if (index < 0 || index >= instance.obstacleData.blockedTiles.Length) return false;

        // Return the value from the blockedTiles array indicating if the tile is blocked
        return instance.obstacleData.blockedTiles[index];
    }
}
