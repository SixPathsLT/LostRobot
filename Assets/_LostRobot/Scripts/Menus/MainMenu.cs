
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject sensitivity;
    public GameObject subtitles;

    private void Start()
    {
            sensitivity.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");

        if (PlayerPrefs.HasKey("Subtitles") && PlayerPrefs.GetInt("Subtitles") == 1)
            subtitles.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
        else
            subtitles.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
    }

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

    public void Subtitles(int state)
    {
        PlayerPrefs.SetInt("Subtitles", state);
    }

    public void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }
}
