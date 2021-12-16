
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public GameObject sensitivity;
   // public GameObject subtitles;

    private void Start()
    {
        // sensitivity.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");

        /*if (PlayerPrefs.HasKey("Subtitles") && PlayerPrefs.GetInt("Subtitles") == 1)
            subtitles.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
        else
            subtitles.transform.GetChild(0).GetComponent<Toggle>().isOn = true;*/

        if (name.ToLower().Contains("settings"))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform child = gameObject.transform.GetChild(i);
                if (child.name.ToLower().Contains("subtitle"))
                {
                    if (PlayerPrefs.HasKey("Subtitles") && PlayerPrefs.GetInt("Subtitles") == 1)
                        child.GetChild(1).GetComponent<Toggle>().isOn = true;
                    else
                        child.GetChild(0).GetComponent<Toggle>().isOn = true;
                }
                if (child.name.ToLower().Contains("sensitivity"))
                {
                    
                    if (PlayerPrefs.HasKey("Sensitivity"))
                        child.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");
                    else
                    {
                        child.GetComponentInChildren<Slider>().value = .5f;
                        PlayerPrefs.SetFloat("Sensitivity", .5f);
                    }
                }
            }
        }
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
