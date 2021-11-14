using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip[] clips;
    private AudioSource source;

    static private AudioManager instance;

    void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = 0.188f;
    }

    public void PlayClip(string name) {
        bool foundClip = false;

        foreach (var clip in clips) {
            if (clip == null)
                continue;

            if (clip.name.Equals(name)) {
                foundClip = true;
                source.clip = clip;
                source.Play();
                break;
            }
        }

        if (!foundClip)
            Debug.Log("Clip not found: " + name);
    }

    public static AudioManager GetInstance() {
        return instance;
    }

}