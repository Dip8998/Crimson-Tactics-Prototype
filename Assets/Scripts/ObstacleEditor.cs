using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]  // Custom editor for the ObstacleData scriptable object
public class Obstacle2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData data = (ObstacleData)target;  // Cast the target object to ObstacleData type

        int gridSize = Mathf.RoundToInt(Mathf.Sqrt(data.blockedTiles.Length)); // Calculate grid size dynamically based on array length

        for (int y = 0; y < gridSize; y++)  // Iterate over rows (y-axis) of the grid
        {
            EditorGUILayout.BeginHorizontal();  // Begin a horizontal group in the editor window
            for (int x = 0; x < gridSize; x++)  // Iterate over columns (x-axis) of the grid
            {
                int i = x + y * gridSize;  // Calculate index in the 1D array corresponding to the 2D grid position
                data.blockedTiles[i] = GUILayout.Toggle(data.blockedTiles[i], $"({x}, {y})");  // Create a toggle button for each grid cell
            }
            EditorGUILayout.EndHorizontal();  // End the horizontal group
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target); // Mark the object as dirty to save changes when toggles are modified
        }
    }
}
