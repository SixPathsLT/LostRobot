using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDown : MonoBehaviour
{
    public Vector2 timerRange;
    public Vector2 intervalRange;
    public bool LockDownInitiated = false;

    private void Start()
    {
        //StartCoroutine(LockDownCoroutine());
    }
    IEnumerator LockDownCoroutine ()
    {
        var timer = Random.Range(intervalRange.x, intervalRange.y);
        yield return new WaitForSeconds(timer);
        LockDownInitiated = true;

        FindObjectOfType<DoorManager>().LockDownEnter();
        //Debug.Log(LockDownInitiated);

        timer = Random.Range(timerRange.x, timerRange.y);


        yield return new WaitForSeconds(timer);

        LockDownInitiated = false;
        FindObjectOfType<DoorManager>().LockDownExit();
       // Debug.Log(LockDownInitiated);

        StartCoroutine(LockDownCoroutine());

    }
    void Update()
    {
            

        
    }
}
