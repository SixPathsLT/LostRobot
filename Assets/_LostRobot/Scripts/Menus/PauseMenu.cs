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

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            GameManager.GetInstance().ChangeState(GamePaused ? GameManager.State.Playing : GameManager.State.Paused);
           /* if (GamePaused)
            {
                ResumeGame();

            }

            else
            {
                GamePaused = true;
                PauseGame.SetActive(true);
                Time.timeScale = 0f;
            }*/
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GamePaused = true;
        PauseGame.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GamePaused = false;
        PauseGame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Toggle()
    {
        GameManager.GetInstance().ChangeState(GamePaused ? GameManager.State.Playing : GameManager.State.Paused);
    }




    public void LeaveGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
