using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] npcPrefabs;

    static private NPCSpawner instance;
    static private List<GameObject> spawnedNPCs = new List<GameObject>();
    
    Transform[] patrolPoints;

    void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        patrolPoints = GameObject.Find("PatrolPoints").GetComponentsInChildren<Transform>();
        patrolPoints[0] = patrolPoints[patrolPoints.Length - 1];
    }

    private void SpawnNPC(int id, Vector3 position) {
        SpawnNPC(id, position, Quaternion.identity);
    }

    private void SpawnNPC(int id, Vector3 position, Quaternion rotation) {
        if (id >= npcPrefabs.Length || npcPrefabs[id] == null) {
            Debug.Log(this + " Failed to spawn NPC. Enemy ID " + id + " does not exist.");
            return;
        }

        GameObject npc = Instantiate(npcPrefabs[id], position, rotation);

        if (Utils.CanSeeTransform(npc.transform, AbilitiesManager.player.transform, 360))
            npc.transform.position = patrolPoints[Random.Range(0, patrolPoints.Length)].position;

        npc.GetComponent<AIManager>().nodes = patrolPoints;
        spawnedNPCs.Add(npc);
    }

    internal void SpawnNPCS() {
        int floor = GameManager.GetInstance().data.level;
        int npcId = floor < 3 ? 0 : floor < 5 ? 1 : 2;

        for (int i = 0; i < floor; i++) {
            Vector3 spawnPosition = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
            if (Vector3.Distance(spawnPosition, AbilitiesManager.player.transform.position) < 20)
                continue;

            SpawnNPC(npcId, spawnPosition);
        }
    }

    private void DeSpawnNPC(GameObject npc) {
        spawnedNPCs.Remove(npc);
        Destroy(npc);
    }

    public void DespawnNPCS() {
        for (int i = spawnedNPCs.Count - 1; i >= 0; i--) {
            GameObject npc = spawnedNPCs[i];
            if (Utils.CanSeeTransform(npc.transform, AbilitiesManager.player.transform, 360))
                continue;

            DeSpawnNPC(npc);
        }


    }

    public static NPCSpawner GetInstance() {
        return instance;
    }

}
