using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Ensure this script is only compiled when in the Unity Editor environment
#if UNITY_EDITOR

// Add this using directive for CustomEditor attribute
using UnityEditor;

// Custom editor for ObstacleData, allowing editing of blockedTiles array in the Inspector
[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    // Override the default Inspector GUI
    public override void OnInspectorGUI()
    {
        // Cast the target object to ObstacleData
        ObstacleData data = (ObstacleData)target;

        // Display toggle buttons for each element in blockedTiles array
        for (int i = 0; i < 100; i++)
        {
            data.blockedTiles[i] = GUILayout.Toggle(data.blockedTiles[i], $"Tile {i % 10},{i / 10}");
            if ((i + 1) % 10 == 0)
                GUILayout.Space(10); // Add space after every 10 tiles for better visual separation
        }

        // Mark the target object as dirty to ensure changes are saved
        EditorUtility.SetDirty(target);
    }
}

#endif // End of UNITY_EDITOR
