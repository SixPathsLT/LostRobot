using System.Collections;
using UnityEngine;

public class LockDown : MonoBehaviour
{
    public Vector2 timerRange;
    public Vector2 intervalRange;
    public bool LockDownInitiated = false;

    DoorManager doorManager;

    private void Start() {
        StartCoroutine(LockDownCoroutine());
        doorManager = FindObjectOfType<DoorManager>();
    }

    IEnumerator LockDownCoroutine() {
        var timer = Random.Range(intervalRange.x, intervalRange.y);
        yield return new WaitForSeconds(timer);
        LockDownInitiated = true;

        doorManager.LockDownEnter();
        timer = Random.Range(timerRange.x, timerRange.y);

        yield return new WaitForSeconds(timer);

        LockDownInitiated = false;
        doorManager.LockDownExit();

        StartCoroutine(LockDownCoroutine());
    }

}
