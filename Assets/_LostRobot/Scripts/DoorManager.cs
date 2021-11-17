using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;
    
    public void LockDownEnter()
    {

        foreach (GameObject door in doors)
        {
            if (Random.Range(0, doors.Length) == 0)
            {
                door.GetComponentInChildren<DoorController>().Lock();
            }            
        }
    }
    public void LockDownExit()
    {

        foreach (GameObject door in doors)
        {
            door.GetComponentInChildren<DoorController>().Unlock();
        }
    }

    void Update()
    {
        
    }
}
