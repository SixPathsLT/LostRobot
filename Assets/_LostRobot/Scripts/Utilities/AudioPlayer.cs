using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    [System.Serializable]
    struct Data {
        public AudioClip audio;
        public string subtitle;
    }

    [SerializeField] private Data[] audioData;
    public AudioSource source;

    void Start() {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayClip(string name) {
        bool foundClip = false;

        foreach (var data in audioData) {
            if (data.audio == null)
                continue;

            if (data.audio.name.Equals(name)) {
                foundClip = true;
                source.clip = data.audio;
                source.Play();
                if (data.subtitle != null)
                    FindObjectOfType<Notification>().SendSubtitle(data.subtitle, source.clip.length);
                break;
            }
        }

        if (!foundClip)
            Debug.Log("Clip not found: " + name);
    }

}