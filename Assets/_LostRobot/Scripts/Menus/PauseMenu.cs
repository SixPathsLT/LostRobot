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
            Toggle();
           /* if (GamePaused)
            * {
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
        if (!GameManager.GetInstance().InPlayingState())
            return;
        GameManager.GetInstance().ChangeState(GameManager.State.Paused);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GamePaused = true;
        PauseGame.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (!GameManager.GetInstance().InPausedState() || !PauseGame.activeSelf)
            return;
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        GamePaused = false;
        PauseGame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Toggle(){
        if (GamePaused)
            ResumeGame();
        else
            Pause();
    }

    public void LeaveGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
