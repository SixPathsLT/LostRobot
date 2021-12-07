using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public GameObject canvas;
    public GameObject cutsceneCam;
    public Cutscene[] cutscenes;
    private static Cutscene currentCutscene;
    public static float timeElapsed;

    string sceneName;

    private void Awake() {
        canvas.SetActive(false);
        cutsceneCam.SetActive(false);
    }

    void Update() {
        if (currentCutscene != null) {
            currentCutscene.Process();

            if (currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration)
                StopCutscene();
            else if (currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration - 1f)
                currentCutscene.playerCam.SetActive(true);
          
            timeElapsed += Time.deltaTime;
        }
    }

    public void PlayCutscene(int index, string sceneName = null) {
        if (index >= cutscenes.Length || cutscenes[index] == null) {
            Debug.Log("Cutscene #" + index + " does not exist.");
            return;
        }
        if (currentCutscene != null) {
            Debug.Log("A cutscene is already playing.");
            return;
        }
        GameManager.GetInstance().ChangeState(GameManager.State.Cutscene);
        this.sceneName = sceneName;
        cutsceneCam.SetActive(true);
        canvas.SetActive(true);
        currentCutscene = cutscenes[index];
        currentCutscene.Init();
    }

    void StopCutscene() {
        canvas.SetActive(false);
        currentCutscene.Stop();
        currentCutscene = null;
        cutsceneCam.SetActive(false);
        timeElapsed = 0f;
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
        if (sceneName != null)
            SceneManager.LoadScene(sceneName);
    }

}
