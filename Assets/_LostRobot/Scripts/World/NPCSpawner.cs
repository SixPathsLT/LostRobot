using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] npcPrefabs;

    static private NPCSpawner instance;
    static private List<GameObject> spawnedNPCs = new List<GameObject>();
    
    void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void SpawnNPC(int id, Vector3 position) {
        SpawnNPC(id, position, Quaternion.identity);
    }

    public void SpawnNPC(int id, Vector3 position, Quaternion rotation) {
        if (id >= npcPrefabs.Length || npcPrefabs[id] == null) {
            Debug.Log(this + " Failed to spawn NPC. Enemy ID " + id + " does not exist.");
            return;
        }

        GameObject npc = Instantiate(npcPrefabs[id], position, rotation);
        spawnedNPCs.Add(npc);
    }

    public static NPCSpawner GetInstance() {
        return instance;
    }

}
