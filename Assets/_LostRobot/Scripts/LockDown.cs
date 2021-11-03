using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDown : MonoBehaviour
{
    public int timer;
    public bool LockDownInitiated = false;
    private void Start()
    {
        StartCoroutine(LockDownCoroutine());
    }
    IEnumerator LockDownCoroutine ()
    {
        timer = Random.Range(4, 10);
        yield return new WaitForSeconds(timer);
        LockDownInitiated = true;

        FindObjectOfType<DoorManager>().LockDown();
        Debug.Log(LockDownInitiated);

        timer = Random.Range(4, 10);


        yield return new WaitForSeconds(timer);

        LockDownInitiated = false;
        Debug.Log(LockDownInitiated);

        StartCoroutine(LockDownCoroutine());

    }
    void Update()
    {
            

        
    }
}
