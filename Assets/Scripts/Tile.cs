using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;  // Grid position of the tile
    public Material defaultMaterial;  // Default material for the tile
    public Material highlightMaterial;  // Material to highlight the tile

    private Renderer tileRenderer;  // Renderer component of the tile

    void Start()
    {
        tileRenderer = GetComponent<Renderer>();  // Get the Renderer component attached to this GameObject
        string[] splitName = name.Split('_');  // Split the GameObject's name using '_' as delimiter
        int x = int.Parse(splitName[1]);  // Parse the second part of the name as x position
        int z = int.Parse(splitName[2]);  // Parse the third part of the name as z position
        gridPosition = new Vector2Int(x, z);  // Set the grid position based on parsed x and z values

        if (defaultMaterial == null)
        {
            defaultMaterial = tileRenderer.material;  // Use the Renderer's current material as default if not set
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse entered tile.");  // Log message when the mouse enters the tile
        if (highlightMaterial != null && tileRenderer != null) // Check if highlightMaterial and tileRenderer are valid
        {
            tileRenderer.material = highlightMaterial;  // Change the tile's material to highlightMaterial
        }
        else
        {
            Debug.LogWarning("Highlight material or tileRenderer is null.");  // Log a warning if materials or renderer are not set
        }
    }

    void OnMouseExit()
    {
        if (defaultMaterial != null)
        {
            tileRenderer.material = defaultMaterial;  // Reset the tile's material to defaultMaterial when the mouse exits
        }
    }

    public void SelectTile()
    {
        // Handle tile selection logic if needed
        Debug.Log($"Tile selected at {gridPosition}");  // Log a message indicating which tile was selected
    }
}
