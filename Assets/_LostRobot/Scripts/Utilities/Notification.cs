using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour {
    
    [SerializeField]
    private TextMeshProUGUI notification, subtitle;

    public void SendNotification(string message, float time = 4, float delay = 0) {
        StartCoroutine(ProcessNotification(notification, message, time, delay));
    }

    public void SendSubtitle(string message, bool reset = false) {
        if (message.Length < 1)
            return;

        int setting = PlayerPrefs.GetInt("Subtitles");
        if (setting == 0) {
            subtitle.text = message;

            if (reset)
                StartCoroutine(Reset(subtitle));
        }        
    }

    IEnumerator ProcessNotification(TextMeshProUGUI mesh, string message, float time, float delay) {
        yield return new WaitForSeconds(delay);
        mesh.text = message;
        yield return new WaitForSeconds(time);
        mesh.text = "";
    }

    IEnumerator Reset(TextMeshProUGUI mesh) {
        yield return new WaitForSeconds(6);
        mesh.text = "";
    }

}