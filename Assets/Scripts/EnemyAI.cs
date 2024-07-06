using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, AI
{
    public float moveSpeed = 4f;  // Speed at which the enemy moves
    private Transform target;  // Reference to the player's transform
    private Vector3 targetPosition;  // Target position the enemy is moving towards
    private bool isMoving = false;  // Flag indicating if the enemy is currently moving
    private List<Vector2Int> path;  // List of grid positions for the enemy's path
    private int pathIndex;  // Index to track the current position in the path

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;  // Find and assign the player's transform
        targetPosition = transform.position;  // Initialize target position to current position
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the target position
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
        else
        {
            // If not moving, check if the player is within range and start pursuing
            if (Vector3.Distance(transform.position, target.position) <= 10f)  // Adjust the detection range as needed
            {
                Vector2Int playerGridPosition = new Vector2Int(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));
                MoveTowardsPlayer(playerGridPosition);  // Start moving towards the player
            }
        }
    }

    // Method to initiate movement towards the player's position
    public void MoveTowardsPlayer(Vector2Int playerGridPosition)
    {
        if (!isMoving)  // Check if not already moving
        {
            // Determine current grid position of the enemy
            var currentGridPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

            // Find a path using A* algorithm from current position to player's position
            path = AStar.FindPath(currentGridPosition, playerGridPosition, FindObjectOfType<ObstacleManager>().obstacleData, FindObjectOfType<Gridgenerator>().gridSize);

            // If a valid path is found
            if (path != null && path.Count > 0)
            {
                pathIndex = 0;  // Reset path index
                MoveAlongPath();  // Start moving along the path
            }
        }
    }

    // Method to move the enemy along the path
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
