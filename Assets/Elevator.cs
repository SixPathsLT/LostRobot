using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour {
    public string sceneName;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && sceneName != null)
            FindObjectOfType<CutsceneManager>().PlayCutscene(1, sceneName);
    }
}
