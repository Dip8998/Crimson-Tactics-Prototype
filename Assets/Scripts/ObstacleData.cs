using UnityEngine;

// Create a new ScriptableObject asset in the Unity Editor under "ScriptableObjects" menu
[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[] blockedTiles = new bool[100]; // This will create a 10x10 grid by default
}
