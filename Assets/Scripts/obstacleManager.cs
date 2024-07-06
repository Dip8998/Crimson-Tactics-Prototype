using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;  // Reference to ObstacleData scriptable object
    public GameObject obstaclePrefab;  // Prefab to instantiate for obstacles
    public static ObstacleManager instance;  // Static instance of ObstacleManager

    void Awake()
    {
        instance = this;  // Set the static instance to this object
    }

    void Start()
    {
        GenerateObstacles();  // Generate obstacles based on obstacleData
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < obstacleData.blockedTiles.Length; i++)
        {
            if (obstacleData.blockedTiles[i])
            {
                // Calculate x and z positions from index
                int x = i % 10;
                int z = i / 10;

                // Instantiate obstaclePrefab at calculated position
                Instantiate(obstaclePrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
            }
        }
    }

    // Method to check if a specific tile position is blocked
    public static bool IsTileBlocked(Vector2Int position)
    {
        if (instance == null) return false;  // Return false if instance is null (no ObstacleManager)

        // Calculate index in blockedTiles array from 2D position
        int index = position.x + position.y * 10;

        // Check if index is within valid range
        if (index < 0 || index >= instance.obstacleData.blockedTiles.Length) return false;

        // Return blocked status of the tile at the calculated index
        return instance.obstacleData.blockedTiles[index];
    }
}
