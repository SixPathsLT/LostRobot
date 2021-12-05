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
    public AudioSource source;

    void Awake() {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayClip(string name) {
        bool foundClip = false;
        foreach (var data in audioData){
            if (data.audio == null)
                continue;

            if (data.audio.name.Equals(name)) {
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