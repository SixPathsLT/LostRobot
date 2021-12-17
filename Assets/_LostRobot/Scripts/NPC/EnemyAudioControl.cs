using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioControl : MonoBehaviour
{

    public AudioClip[] clips;
    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Step()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
        AudioClip clip = clips[Random.Range(0,3)];
        source.volume = .4f;
        RandomizePitch(.5f, 1f);
        source.PlayOneShot(clip);
    }

    void RandomizePitch(float min, float max)
    {

        source.pitch = Random.Range(min, max);
    }

    void Investigate()
    {
        source.volume = .4f;
        AudioClip clip = clips[3];
        source.PlayOneShot(clip);
    }

    public void Found()
    {
        source.volume = .4f;
        AudioClip clip = clips[4];
        source.PlayOneShot(clip);
    }

    public void Stun()
    {
        source.volume = .4f;
        AudioClip clip = clips[5];
        source.PlayOneShot(clip);
    }
}
