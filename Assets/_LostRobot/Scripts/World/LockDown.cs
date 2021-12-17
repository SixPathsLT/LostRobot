
using System.Collections;
using UnityEngine;

public class LockDown : MonoBehaviour
{
    public Vector2 timerRange;
    public Vector2 intervalRange;
    public static bool LockDownInitiated = false;
    private bool stopped = false;

    AudioSource source;
    DoorManager doorManager;
    public Color originalColor;
    public Material mat;
    private void Start() {
        StartCoroutine(LockDownCoroutine());
        doorManager = FindObjectOfType<DoorManager>();
        mat.SetColor("_EmissionColor", Color.white * 1.1f);
        source = GetComponent<AudioSource>();
    }

    float timePassed;
    Color lerpedColor = Color.white;
    bool red;
    private void Update()
    {
        /*if (LockDownInitiated && !playing)
            GetComponent<AudioSource>().Play();
        else if (!LockDownInitiated && playing)
            GetComponent<AudioSource>().Stop();*/
            
        if (LockDownInitiated && GameManager.GetInstance().InPausedState() && source.isPlaying) {
            source.Stop();
            stopped = true;
        }

        if (LockDownInitiated && stopped && GameManager.GetInstance().InPlayingState()) {
            source.Play();
            stopped = false;
        }

        timePassed += Time.deltaTime;
        if (timePassed > (red || !LockDownInitiated ? 2 : 1)) {
            red = !red;
            timePassed = 0;
        }

        if (LockDownInitiated) {
            if (red) {
                lerpedColor = Color.Lerp(lerpedColor, Color.red, timePassed * Time.deltaTime * 2.5f);
                mat.SetColor("_EmissionColor", lerpedColor * 2f);
            } else {
                lerpedColor = Color.Lerp(lerpedColor, Color.gray, timePassed * Time.deltaTime);
                mat.SetColor("_EmissionColor", lerpedColor * 2f);
            }
        } else if (lerpedColor !=  Color.white) {
            lerpedColor = Color.Lerp(lerpedColor, Color.white, timePassed * Time.deltaTime);
            mat.SetColor("_EmissionColor", lerpedColor * 1.1f);
            if (source.isPlaying) {
                float volume = source.volume - (Time.deltaTime / 4);
                source.volume = volume < 0 ? 0 : volume;
            }
        }


    }

    private void CancelLockDown() {
        source.loop = false;
        LockDownInitiated = false;
        stopped = false;
        doorManager.LockDownExit();
        NPCSpawner.GetInstance().DespawnNPCS();
    }

    IEnumerator LockDownCoroutine() {
        var timer = Random.Range(intervalRange.x, intervalRange.y);
        yield return new WaitForSeconds(timer);

        if (GameManager.GetInstance().InPlayingState()) {
            source.volume = 0.5f;
            source.loop = true;
            source.Play();
            LockDownInitiated = true;

            doorManager.LockDownEnter();
            timer = Random.Range(timerRange.x, timerRange.y);
            NPCSpawner.GetInstance().SpawnNPCS();
        }

        yield return new WaitForSeconds(timer);
        CancelLockDown();
        StartCoroutine(LockDownCoroutine());
    }

}
