
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;
    Dictionary<GameObject, DoorController> entries = new Dictionary<GameObject, DoorController>();

    private void Start() {
        foreach (GameObject door in doors)
            entries.Add(door, door.GetComponentInChildren<DoorController>());

        SetDoorStates();
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

    private void SetDoorStates()
    {
        
        HashSet<GameObject> lockedDoors = new HashSet<GameObject>();
        while (lockedDoors.Count < doors.Length / 2) {
            GameObject door = doors[Random.Range(0, doors.Length)];
            if (!lockedDoors.Contains(door)) {
                if (!entries[door].cardRequired)
                    entries[door].Locked = false;
                entries[door].triggerPuzzle = false;
                entries[door].UpdateDoorColors();
                lockedDoors.Add(door);
            }
        }
       /* foreach (GameObject door in doors)
        {
            if (Random.Range(0, 2) == 0)
            {
                if (!entries[door].cardRequired)
                entries[door].Locked = false;
                entries[door].triggerPuzzle = false;
                entries[door].UpdateDoorColors();
            }
        }*/
    }
}
