using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gridgenerator : MonoBehaviour
{
    public GameObject tilePrefab;  // Prefab for the grid tiles
    public GameObject playerPrefab;  // Prefab for the player
    public GameObject enemyPrefab;  // Prefab for the enemy
    public int gridSize = 10;  // Size of the grid (gridSize x gridSize)
    public Text tileInfoText;  // Text UI element to display tile information
    private GameObject enemy;  // Reference to the enemy GameObject
    private GameObject player;  // Reference to the player GameObject

    void Start()
    {
        GenerateGrid();  // Generate the grid of tiles
        SpawnPlayer();  // Spawn the player character
        SpawnEnemy();  // Spawn the enemy character
    }

    void Update()
    {
        HandleMouseInput();  // Handle mouse input for interacting with tiles
    }

    private void HandleMouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 targetPos = new Vector3(tile.gridPosition.x, 0.6f, tile.gridPosition.y); // Update y coordinate
                    if (!ObstacleManager.IsTileBlocked(tile.gridPosition))
                    {
                        MovePlayer(targetPos);
                        Invoke(nameof(MoveEnemy), 0.5f); // Delay enemy movement slightly
                        
                    }
                }
            }
        }
    }




    private void MovePlayer(Vector3 targetPos)
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().MoveTo(targetPos);  // Call MoveTo method of PlayerMovement script
        }
    }

    private void MoveEnemy()
    {
        if (enemy != null && player != null)
        {
            Vector2Int playerGridPos = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z));  // Get player's grid position
            enemy.GetComponent<EnemyAI>().MoveTowardsPlayer(playerGridPos);  // Move enemy towards player
        }
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);  // Instantiate tilePrefab at grid position
                tile.name = $"Tile_{x}_{z}";  // Set tile name with grid coordinates
                tile.AddComponent<Tile>();  // Add Tile component to the instantiated tile GameObject
            }
        }
    }

    private void SpawnPlayer()
    {
        Vector3 playerStartPos = new Vector3(0, 0.6f, 0); // Starting position for player
        player = Instantiate(playerPrefab, playerStartPos, Quaternion.identity);  // Instantiate playerPrefab at start position
    }

    private void SpawnEnemy()
    {
        Vector3 enemyStartPos = new Vector3(gridSize - 1, 0.6f, gridSize - 1); // Opposite corner of the grid for enemy start
        enemy = Instantiate(enemyPrefab, enemyStartPos, Quaternion.identity);  // Instantiate enemyPrefab at start position
    }

   
}
