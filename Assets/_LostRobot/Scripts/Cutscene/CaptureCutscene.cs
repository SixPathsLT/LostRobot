using UnityEngine;
using TMPro;
public class CaptureCutscene : Cutscene {

    GameObject cam;
    public GameObject header;
    public GameObject sub;
    public GameObject notifs;
    
    public override void Init(){
        base.Init();
        GameManager.GetInstance().ChangeState(GameManager.State.Captured);
        notifs.SetActive(true);
        cam = FindObjectOfType<CutsceneManager>().cutsceneCam;
        header.GetComponent<TextMeshProUGUI>().text = "Detected";
        sub.GetComponent<TextMeshProUGUI>().text = "Consciousness Reduced";
        cam.SetActive(false);
    }

    public override void Process()
    {

    }

    public override void Stop()
    {
        base.Stop();
        notifs.SetActive(false);
        AIManager.player.GetComponent<PlayerController>().HandleCapture();
        header.GetComponent<TextMeshProUGUI>().text = "Ability Unlocked!";
    }

}
