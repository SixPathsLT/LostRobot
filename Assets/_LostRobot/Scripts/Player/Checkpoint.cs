
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerData checkpointData;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointData.currentConciousness = player.GetComponent<PlayerController>().data.currentConciousness;
            checkpointData.maxConciousness = player.GetComponent<PlayerController>().data.maxConciousness;
            checkpointData.currentHealth = player.GetComponent<PlayerController>().data.currentHealth;
            checkpointData.maxHealth = player.GetComponent<PlayerController>().data.maxHealth;
            player.GetComponent<PlayerController>().respawnPoint = gameObject.transform.position;
        }
    }
}
