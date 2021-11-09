using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardLogic : MonoBehaviour
{
    private bool obtainedInfo = false;
    private bool obtainedKey = false;

    public Collider infoPCTrigger;
    public GameObject keyObject;
    public DoorController door;

    private void Start()
    {
        door.Locked = true;
    }

    private void Update()
    {
        checkInfo();
        checkKey();
    }

    private void checkInfo()
    {
        if (!obtainedInfo)
        {
            keyObject.GetComponent<MeshRenderer>().material.color = Color.red;       //For Testing Purposes
        }
        else
        {
            keyObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    private void checkKey()
    {
        if (!obtainedKey)
        {
            door.Close();
        }
        else
        {
            door.Locked = false;
            keyObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    public void setInfo(bool obtained)
    {
        obtainedInfo = obtained;
    }

    public void gotKey(bool key)
    {
        if (obtainedInfo)
        {
            obtainedKey = key;
        }            
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!obtainedInfo)
            {
                obtainedInfo = true;
            }
        }
    }

}
