using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverMenu;

    private void OnEnable()
    {
       FollowerScript.onGameOver += EnableGameOverMenu; 
    }
    
    private void OnDisable()
    {
        FollowerScript.onGameOver -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        GameOverMenu.SetActive(true);
    }
}