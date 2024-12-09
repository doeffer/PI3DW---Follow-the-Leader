using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void GameOver()
    {
        // Implement your game over logic here
        // For example, reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
