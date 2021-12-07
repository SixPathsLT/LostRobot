using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Game starts");
        

       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GetInstance().StartGame();

        
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();

    }
}
