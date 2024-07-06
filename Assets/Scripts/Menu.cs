using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Method to start the game
    public void StartGame()
    {
        // Load scene with index 1 (assuming scene 1 is the game scene)
        SceneManager.LoadScene(1);
    }
}
