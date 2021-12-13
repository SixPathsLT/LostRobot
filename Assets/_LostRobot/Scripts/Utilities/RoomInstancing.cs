using UnityEngine;

public class RoomInstancing : MonoBehaviour
{
    public GameObject roomPrefab;
    private GameObject room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            room = Instantiate(roomPrefab, gameObject.transform) as GameObject;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && room == null)
            room = Instantiate(roomPrefab, gameObject.transform) as GameObject; 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(room);
        
    }
}
