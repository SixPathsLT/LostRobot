
using System.Collections;
using UnityEngine;

public class LockDown : MonoBehaviour
{
    public Vector2 timerRange;
    public Vector2 intervalRange;
    public static bool LockDownInitiated = false;
    private bool stopped = false;

    DoorManager doorManager;
    
    private void Start() {
        StartCoroutine(LockDownCoroutine());
        doorManager = FindObjectOfType<DoorManager>();
    }
    
    private void Update()
    {
        /*if (LockDownInitiated && !playing)
            GetComponent<AudioSource>().Play();
        else if (!LockDownInitiated && playing)
            GetComponent<AudioSource>().Stop();*/

        if (LockDownInitiated && GameManager.GetInstance().InPausedState())
        {
            GetComponent<AudioSource>().Stop();
            stopped = true;
        }

        if (LockDownInitiated && stopped && GameManager.GetInstance().InPlayingState())
        {
            GetComponent<AudioSource>().Play();
            stopped = false;
        }

       
        

    }

    private void CancelLockDown()
    {
        GetComponent<AudioSource>().loop = false;
        LockDownInitiated = false;
        stopped = false;
        doorManager.LockDownExit();
        NPCSpawner.GetInstance().DespawnNPCS();
    }

    IEnumerator LockDownCoroutine() {
        var timer = Random.Range(intervalRange.x, intervalRange.y);
        yield return new WaitForSeconds(timer);

        if (GameManager.GetInstance().InPlayingState())
        {
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
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
