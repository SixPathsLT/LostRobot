
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public static int INTRODUCTION_CUTSCENE = 0;
    public static int ELEVATOR_CUTSCENE = 1;
    public static int ABILITY_UNLOCKED_CUTSCENE = 2;
    public static int CAPTURED_CUTSCENE = 3;
    public static int ENDING_CUTSCENE = 4;

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
       /* if (Input.GetKeyDown(KeyCode.P))
        {
            AbilitiesManager.player.transform.parent.position = AbilitiesManager.player.transform.position;
            AbilitiesManager.player.transform.localRotation = Quaternion.identity;
            AbilitiesManager.player.transform.localPosition = new Vector3(0, 1.3f, 0);
            PlayCutscene(ENDING_CUTSCENE);
        }*/
        if (currentCutscene != null) {
            currentCutscene.Process();

            if (currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration)
                StopCutscene();
            else if (currentCutscene.playerCam != null && !RequestedSceneChange() && currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration - 1f) 
                currentCutscene.playerCam.SetActive(true);
            else if (RequestedSceneChange() && sceneName.Contains("EndingScene") && currentCutscene.GetPlayableDirector() != null && timeElapsed > currentCutscene.GetPlayableDirector().duration - 2.6f)
                StopCutscene(true);

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

    void StopCutscene(bool ending = false) {
        currentCutscene.Stop();
        currentCutscene = null;
        cutsceneCam.SetActive(false);
        canvas.SetActive(false);
        timeElapsed = 0f;
        if (!GameManager.GetInstance().InCapturedState() && PCUI.current == null)
            GameManager.GetInstance().ChangeState(GameManager.State.Playing);
        if (RequestedSceneChange()) {
            SceneManager.LoadScene(sceneName);
            if (ending) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
                
        }

        sceneName = "";

    }

    internal static bool RequestedSceneChange() { return sceneName.Length > 0; }

}
