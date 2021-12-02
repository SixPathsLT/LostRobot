using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public GameObject canvas;
    public Cutscene[] cutscenes;
    private static Cutscene currentCutscene;
    public static float timeElapsed;
    
    void Start() {
        //PlayCutscene(DEFAULT);
    }
    
    void Update() {
        if (currentCutscene != null) {
            currentCutscene.Process();

            if (timeElapsed > currentCutscene.GetPlayableDirector().duration)
                StopCutscene();

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
        canvas.SetActive(true);
        currentCutscene = cutscenes[index];
    }

    void StopCutscene() {
        canvas.SetActive(false);
        currentCutscene.Stop();
        currentCutscene = null;
    }

    public static Cutscene GetActiveCutscene() {
        return currentCutscene;
    }
}
