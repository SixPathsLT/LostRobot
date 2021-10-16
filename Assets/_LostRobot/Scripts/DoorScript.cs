using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform PlayerCamera;
    public float MaxDistance = 5;

    private bool opened = false;
    



    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Pressed();
            
            Debug.Log("You Press E");
        }
    }

    void Pressed()
    {
        //This will name the Raycasthit and came information of which object the raycast hit.
        RaycastHit doorhit;

        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out doorhit, MaxDistance))
        {

            // if raycast hits, then it checks if it hit an object with the tag Door.
            if (doorhit.transform.tag == "Door")
            {

                

                //This will set the bool the opposite of what it is.
                opened = !opened;

               
            }
        }
    }
}