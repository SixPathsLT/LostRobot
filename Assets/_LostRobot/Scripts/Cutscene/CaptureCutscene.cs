using UnityEngine;
using TMPro;
public class CaptureCutscene : Cutscene {

    GameObject cam;
    public GameObject header;
    public GameObject sub;
    public GameObject notifs;
        
    public override void Init(){
        base.Init();
        if (LockDown.LockDownInitiated)
            FindObjectOfType<LockDown>().GetComponent<AudioSource>().Stop();
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
        if (LockDown.LockDownInitiated)
            FindObjectOfType<LockDown>().GetComponent<AudioSource>().Play();
        notifs.SetActive(false);



        Collider[] colliders = Physics.OverlapSphere(AbilitiesManager.player.transform.position, 10f);
        foreach (var collider in colliders) {
            AIManager aiManager = collider.GetComponent<AIManager>();
            if (aiManager != null)
                aiManager.transform.position = aiManager.nodes[0].transform.position;

        }

        AIManager.player.GetComponent<PlayerController>().HandleCapture();
        header.GetComponent<TextMeshProUGUI>().text = "Ability Unlocked!";

    }

}
