using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardTrigger : MonoBehaviour
{
    public KeyCardLogic main;
    public GameObject mesh;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && !main.obtainedKey)
        {
            main.gotKey(true);
            GetComponent<AudioSource>().Play();
            FindObjectOfType<Notification>().SendNotification("Access Card Acquired");            
            main.checkKey();

            mesh.SetActive(false);
        }                
    }
}
