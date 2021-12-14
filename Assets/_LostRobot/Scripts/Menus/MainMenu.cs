
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        //Debug.Log("Game starts");
        

       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.GetInstance().Load(); 
    }

    public void NewGame() {
        GameManager.GetInstance().NewGame();
    }

    public void Quit()
    {
       // Debug.Log("Quit Game");
        Application.Quit();

    }
}
