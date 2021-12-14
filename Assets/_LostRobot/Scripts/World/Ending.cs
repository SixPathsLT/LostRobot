using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public PlayerData data;
    public TextMeshProUGUI mesh;
    bool end;
    AudioSource source;

    float timeElapsed;

    private void Start() {
        source = GameManager.GetInstance().GetComponent<AudioSource>();
        mesh.text = "Obtained " + data.GetEmailsCount() + " email logs out of 28.";
    }

    public void EndGame() {
        if (end)
            return;

        mesh.text = "Thanks for Playing!";
        end = true;
        StartCoroutine(Exit());
    }

    IEnumerator Exit() {
        while (source.volume > 0.02) {
            timeElapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(source.volume, 0, timeElapsed);
            yield return new WaitForSeconds(timeElapsed);
        }
        source.Stop();
        SceneManager.LoadScene(0);
    }


}
