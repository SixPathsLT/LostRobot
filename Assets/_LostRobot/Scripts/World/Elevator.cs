using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {
    public string sceneName;
    public KeyCardLogic access;

    private void OnTriggerStay(Collider other) {
        if (access.obtainedKey)
        {
            if (other.CompareTag("Player") && sceneName != null)
            {
                other.transform.parent.position = other.transform.position;
                other.transform.localRotation = Quaternion.identity;
                other.transform.localPosition = new Vector3(0, other.transform.localPosition.y, 0);
                FindObjectOfType<CutsceneManager>().PlayCutscene(1, sceneName);
            }
        }
        else
        {
            FindObjectOfType<Notification>().SendNotification("Access Card Required");
        }
    }
}
