using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;
    
    public void LockDown()
    {

        foreach (GameObject door in doors)
        {
            if(Random.Range(0, 2) == 0)
            {

                door.GetComponent<DoorController>().Close();

                Debug.Log("closeddd");
            }

            
        }
    }

    
    void Update()
    {
        
    }
}
