using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {
    public string sceneName;
    public KeyCardLogic access;

    private void OnTriggerEnter(Collider other) {
        if (access.obtainedKey)
        {
            if (other.CompareTag("Player") && sceneName != null)
            {
                other.transform.parent.position = other.transform.position;
                other.transform.localRotation = Quaternion.identity;
                other.transform.localPosition = new Vector3(0, 1.3f, 0);
                FindObjectOfType<CutsceneManager>().PlayCutscene(1, sceneName);
                other.GetComponent<PlayerController>().data.CompletedLevel();
            }
        }
        else
        {
            FindObjectOfType<Notification>().SendNotification("Access Card Required");
        }
    }
}
