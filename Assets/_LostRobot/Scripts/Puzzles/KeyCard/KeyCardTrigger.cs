using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{
    public KeyCardLogic main;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            main.gotKey(true);
            main.checkKey();
        }                
    }
}
