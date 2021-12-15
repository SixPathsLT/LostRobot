using UnityEngine;

public class Elevator : MonoBehaviour {
    public string sceneName;
    public KeyCardLogic access;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || sceneName == null)
            return;

        if (access.obtainedKey) {
            other.transform.parent.position = other.transform.position;
            other.transform.localRotation = Quaternion.identity;
            other.transform.localPosition = new Vector3(0, 1.3f, 0);
            FindObjectOfType<CutsceneManager>().PlayCutscene(sceneName.Equals("Ending") ? CutsceneManager.ENDING_CUTSCENE : CutsceneManager.ELEVATOR_CUTSCENE, sceneName);
            other.GetComponent<PlayerController>().data.CompletedLevel();
        } else
            FindObjectOfType<Notification>().SendNotification("Access Card Required");
    }
}
