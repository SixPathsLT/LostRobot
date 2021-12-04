using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardLogic : MonoBehaviour
{
    private bool obtainedInfo = false;
    public bool obtainedKey = false;

    public GameObject keyObject;
    public DoorController[] doors;

    private void Start()
    {
        foreach (DoorController door in doors)
        {
            door.Locked = true;
        }
    }

    public void checkInfo()
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

    public void checkKey()
    {
        if (!obtainedKey)
        {
            foreach (DoorController door in doors)
            {
                door.Close();
            }
        }
        else
        {
            foreach (DoorController door in doors)
            {
                door.Locked = false;
            }
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
            checkInfo();
        }
    }

}
