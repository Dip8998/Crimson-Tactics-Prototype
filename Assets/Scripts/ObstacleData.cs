using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute to create a new instance of this scriptable object via Unity's CreateAssetMenu
[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    public bool[] blockedTiles = new bool[100]; // Array to store blocked status of 100 tiles (10x10 grid)
}
