using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public GameObject canvas;
    public GameObject cutsceneCam;
    public Cutscene[] cutscenes;
    private static Cutscene currentCutscene;
    public static float timeElapsed;
    
    void Start() {
        PlayCutscene(0);
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

    void PlayCutscene(int index) {
        if (index >= cutscenes.Length) {
            Debug.Log("Cutscene #" + index + " does not exist.");
            return;
        }
        if (currentCutscene != null) {
            Debug.Log("A cutscene is already playing.");
            return;
        }

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
    }

    public static Cutscene GetActiveCutscene() {
        return currentCutscene;
    }
}
