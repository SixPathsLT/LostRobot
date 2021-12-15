using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public bool playAtEnd;
    public KeyCardLogic key;
    AudioPlayer audio;
    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playAtEnd && !played)
        {
            if (key.obtainedKey)
            {
                audio.PlayAllClips();
                played = true;
            }
        }
        else if (!played)
        {
            audio.PlayAllClips();
            played = true;
        }        
    }
}
