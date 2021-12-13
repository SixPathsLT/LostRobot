
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;
    Dictionary<GameObject, DoorController> entries = new Dictionary<GameObject, DoorController>();

    private void Start() {
        foreach (GameObject door in doors)
            entries.Add(door, door.GetComponentInChildren<DoorController>());
        
    }

    public void LockDownEnter() {

        foreach (GameObject door in doors) {
            if (Random.Range(0, doors.Length) == 0)
                entries[door].Lock();
        }
    }
    public void LockDownExit() {
        foreach (GameObject door in doors)
            entries[door].Unlock();
    }


}
