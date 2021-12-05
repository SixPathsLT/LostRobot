using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    AudioPlayer audio;

    private void Start()
    {
        audio = GetComponent<AudioPlayer>();
        audio.source.volume = .35f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetKey(KeyCode.E))
        {
            audio.PlayAllClips();
        }
    }
}
