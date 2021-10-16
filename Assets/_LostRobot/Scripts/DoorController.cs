using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isopen;
    public void OpenChest()
    {
        if (isopen)
        {
            isopen = true;
            Debug.Log("Door is Open");

        }



    }
}
    
