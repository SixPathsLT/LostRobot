using System.Collections;
using UnityEngine;
public class EndingCutscene : Cutscene {

    bool played = false;
    public override void Init() {
        base.Init();

        GameObject.Find("Environment Audio").SetActive(false);
        GameManager.GetInstance().GetComponent<AudioSource>().Play();
        Time.timeScale = 0.7f;
        GameManager.GetInstance().data.level = 7;
    }

    public override void Process() {
        if ((int) CutsceneManager.timeElapsed == 1f)
            Time.timeScale = 1f;

       if (!played && CutsceneManager.timeElapsed >= 2f) {
            played = true;
            GetComponent<AudioPlayer>().PlayAllClips();
       }
    }

    public override void Stop() {
        base.Stop();
        playerCam.SetActive(false);
        StartCoroutine(StartElevatorCutscene());
    }

    IEnumerator StartElevatorCutscene() {
        yield return new WaitForEndOfFrame();
        FindObjectOfType<CutsceneManager>().PlayCutscene(CutsceneManager.ELEVATOR_CUTSCENE, "EndingScene");
        yield return new WaitForSeconds(2);
        GameObject.Find("AudioPlayer").GetComponent<AudioPlayer>().PlayClip("Level_6_AI_3_Clip_1", true);
    }

}
