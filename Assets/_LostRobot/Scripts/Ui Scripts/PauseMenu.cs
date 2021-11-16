using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseGame;
    public bool GamePaused;
    public string mainMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                ResumeGame();

            }

            else
            {
                GamePaused = true;
                PauseGame.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void ResumeGame()
    {

        GamePaused = false;
        PauseGame.SetActive(false);
        Time.timeScale = 1f;
    }




    public void LeaveGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
