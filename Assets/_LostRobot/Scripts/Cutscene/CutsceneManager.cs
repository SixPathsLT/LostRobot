using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public GameObject canvas;
    public GameObject cutsceneCam;
    public Cutscene[] cutscenes;

    public static float timeElapsed;

    private static Cutscene currentCutscene;
    private static string sceneName = "";

    private void Awake() {
        canvas.SetActive(false);
        cutsceneCam.SetActive(false);
    }

    void Update() {
        if (currentCutscene != null) {
            currentCutscene.Process();

            if (currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration)
                StopCutscene();
            else if (!RequestedSceneChange() && currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration - 1f) 
                    currentCutscene.playerCam.SetActive(true);
            
          
            timeElapsed += Time.deltaTime;
        }
    }

    public void PlayCutscene(int index, string nextScene = "") {
        if (index >= cutscenes.Length || cutscenes[index] == null) {
            Debug.Log("Cutscene #" + index + " does not exist.");
            return;
        }
        if (currentCutscene != null) {
            Debug.Log("A cutscene is already playing.");
            return;
        }
        GameManager.GetInstance().ChangeState(GameManager.State.Cutscene);
        sceneName = nextScene;
        cutsceneCam.SetActive(true);
        canvas.SetActive(true);
        currentCutscene = cutscenes[index];
        currentCutscene.Init();
    }

    void StopCutscene() {
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
        currentCutscene.Stop();
        currentCutscene = null;
        cutsceneCam.SetActive(false);
        canvas.SetActive(false);
        timeElapsed = 0f;
        if (RequestedSceneChange())
            SceneManager.LoadScene(sceneName);

        sceneName = "";

    }



    internal static bool RequestedSceneChange() { return sceneName.Length > 0; }

}
