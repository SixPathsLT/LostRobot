using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Door[] doors;
    
    public void LockDown()
    {
        doors[1].Close();
        
    }

    
    void Update()
    {
        
    }
}
