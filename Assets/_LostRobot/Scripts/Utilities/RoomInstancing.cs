using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstancing : MonoBehaviour
{
    public GameObject roomPrefab;

    private GameObject room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            room = Instantiate<GameObject>(roomPrefab, gameObject.transform) as GameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(room);
        }
    }
}
