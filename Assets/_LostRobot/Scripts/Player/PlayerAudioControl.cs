using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{

    public AudioClip[] clips;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Step()
    {
        AudioClip clip = clips[Random.Range(0,2)];
        source.volume = .2f;
        RandomizePitch();
        source.PlayOneShot(clip);
    }

    void RandomizePitch()
    {
        source.pitch = Random.Range(.7f, 1.3f);
    }
}
