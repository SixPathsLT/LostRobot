
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    AudioPlayer audio;
    bool played;

    private void Start()
    {
        audio = GetComponent<AudioPlayer>();
        audio.source.volume = .35f;
       
            AreasOfInterest.Register(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetKey(KeyCode.E) && !played)
        {
            played = true;
            audio.PlayAllClips();
            AreasOfInterest.DeRegister(gameObject);
        }
    }
}
