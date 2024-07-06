using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define an interface named AI
public interface AI
{
    // Method signature for moving towards a player position
    void MoveTowardsPlayer(Vector2Int playerPosition);
}
