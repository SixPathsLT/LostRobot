using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : MonoBehaviour
{
    public AIBehaviour combat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && combat == transform.root.GetComponent<AIManager>().currentBehaviour) 
        {
            other.GetComponent<PlayerController>().data.SetHealth(0);
            Debug.Log("Damage player");


        }
    }

    void Update()
    {
        
    }
}
