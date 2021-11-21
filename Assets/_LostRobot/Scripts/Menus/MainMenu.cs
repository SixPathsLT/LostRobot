using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Game starts");
        /*
         if there's another scene we  can use this code to go to the next scene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        */
    }



    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();

    }
}
