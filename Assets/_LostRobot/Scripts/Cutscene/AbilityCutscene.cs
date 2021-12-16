using UnityEngine;
using TMPro;

public class AbilityCutscene : Cutscene {

    GameObject cam;
    public GameObject sub;
    public GameObject notifs;
    public override void Init() {
        base.Init();
        if (LockDown.LockDownInitiated)
            FindObjectOfType<LockDown>().GetComponent<AudioSource>().Stop();
        Cursor.visible = false;
        notifs.SetActive(true);
        cam = FindObjectOfType<CutsceneManager>().cutsceneCam;
        Vector3 desiredPos = player.transform.position + ((-player.transform.forward) * 5f) + player.transform.up;
        cam.transform.position = desiredPos;
        cam.transform.rotation = Quaternion.LookRotation(((player.transform.position + player.transform.up) - cam.transform.position).normalized);

        int readEmails = player.GetComponent<PlayerController>().data.GetEmailsCount();
        foreach (var ability in player.GetComponent<AbilitiesManager>().abilities) {
            if (ability.requiredReadEmails == readEmails) {
                sub.GetComponent<TextMeshProUGUI>().text = ability.name.Replace("Ability", "").Replace("Data", "");
                AbilitiesManager.selectedAbility = ability;
                break;
            }
        }    
    }

    public override void Process() {
        cam.transform.position = Vector3.Lerp(cam.transform.position, player.transform.position + (player.transform.up * 1.4f), 0.2f *  Time.deltaTime);
    }

    public override void Stop() {
        base.Stop();
        if (LockDown.LockDownInitiated)
            FindObjectOfType<LockDown>().GetComponent<AudioSource>().Play();
        GameManager.GetInstance().ChangeState(GameManager.State.Email);
        notifs.SetActive(false);
        Cursor.visible = true;
    }

}
