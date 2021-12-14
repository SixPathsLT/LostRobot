using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [System.Serializable]
    struct AudioData
    {
        public AudioClip audio;
        public string subtitle;
    }

    [SerializeField] AudioData[] audioData;
    [HideInInspector]
    public AudioSource source;

    void Awake() {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayClip(int index) {
        if (index >= audioData.Length) {
            Debug.Log("Clip #" + index + " does not exist.");
            return;
        }
        AudioData data = audioData[index];
        Play(data, false);
    }

    public void PlayClip(string name) {
        bool foundClip = false;
        foreach (var data in audioData){
            if (data.audio == null)
                continue;
            
            if (data.audio.name.Equals(name)) {
                foundClip = true;
                Play(data, false);
                break;
            }
        }

        if (!foundClip)
            Debug.Log("Clip not found: " + name);
    }

    private void Play(AudioData data, bool reset)  {
        source.clip = data.audio;
        source.Play();

        if (data.subtitle != null)
            FindObjectOfType<Notification>().SendSubtitle(data.subtitle, reset);
    }

    public void PlayAllClips() {
        StartCoroutine(PlayAll());
    }

   IEnumerator PlayAll() {
        yield return new WaitForSeconds(1);
        foreach (var data in audioData) {
            Play(data, data.audio == audioData[audioData.Length - 1].audio);
            yield return new WaitForSeconds(data.audio.length);
        }
    }
}