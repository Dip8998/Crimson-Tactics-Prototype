using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves
    private Vector3 targetPosition;  // Target position the player is moving towards
    private bool isMoving = false;  // Flag indicating if the player is currently moving
    private List<Vector2Int> path;  // List of grid positions for the player's path
    private int pathIndex;  // Index to track the current position in the path

    void Start()
    {
        targetPosition = transform.position;  // Initialize target position to current position
    }

    void Update()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the player has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                pathIndex++;  // Move to the next position in the path
                if (pathIndex < path.Count)
                {
                    MoveAlongPath();  // Move along the path
                }
                else
                {
                    isMoving = false;  // Stop moving if reached the end of the path
                }
            }
        }
    }

    // Method to initiate movement to a specific target position
    public void MoveTo(Vector3 targetPos)
    {
        if (!isMoving)  // Check if not already moving
        {
            var obstacleData = FindObjectOfType<ObstacleManager>().obstacleData;  // Get obstacle data
            var gridSize = FindObjectOfType<Gridgenerator>().gridSize;  // Get grid size
            Vector2Int currentGridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));  // Current grid position
            Vector2Int targetGridPos = new Vector2Int(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.z));  // Target grid position

            // Find a path using A* algorithm from current position to target position
            path = AStar.FindPath(currentGridPos, targetGridPos, obstacleData, gridSize);

            // If a valid path is found
            if (path != null && path.Count > 0)
            {
                pathIndex = 0;  // Reset path index
                MoveAlongPath();  // Start moving along the path
            }
        }
    }

    // Method to move the player along the path
    void MoveAlongPath()
    {
        if (path != null && pathIndex < path.Count)
        {
            Vector2Int gridPosition = path[pathIndex];  // Get the grid position from the path
            targetPosition = new Vector3(gridPosition.x, 0.6f, gridPosition.y);  // Set the target position for movement
            isMoving = true;  // Start moving towards the target position
        }
    }
}
