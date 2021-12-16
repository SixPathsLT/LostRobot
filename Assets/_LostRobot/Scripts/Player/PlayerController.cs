
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PlayerData data;
    public PlayerData checkpoint;
    [HideInInspector] public Vector3 respawnPoint;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update() {
       // if (Input.GetKeyDown(KeyCode.H))
       //     GameManager.GetInstance().forcestart();
        if (!GameManager.GetInstance().InPlayingState())
            return;

       // HandleDeath();

    }

    public void HandleCapture() {
        ReduceConsciousness(10);
        Respawn();
        FindObjectOfType<Notification>().SendNotification("You were detected by the AI. Your consciousness level is now " + data.GetConcioussness() + ".");
    }

    public void HandleDeath() {
        Respawn();
        ReduceConsciousness(1);
        FindObjectOfType<Notification>().SendNotification("You were injured by the AI. " + " Your consciousness level is now " + data.GetConcioussness() + ".");
        
    }

    private void Respawn() {
        GameManager.GetInstance().ChangeState(GameManager.State.Captured);
        transform.position = GameObject.Find("RespawnLocation").transform.position;
        data.currentHealth = data.maxHealth;
        /*transform.position = respawnPoint;
        data.currentHealth = checkpoint.currentHealth;
        data.maxHealth = checkpoint.maxHealth;
        data.currentConciousness = checkpoint.currentConciousness;
        data.maxConciousness = checkpoint.maxConciousness;*/
        //data.SetHealth(100);
        GameManager.GetInstance().OnRespawn();
        StartCoroutine(Resume());
    }

    private void ReduceConsciousness(int amount) {
        int consciousness = data.GetConcioussness() - amount;
        if (consciousness < 1)
            consciousness = 0;

        data.SetConciousness(consciousness);
    }


    public IEnumerator Resume() {
        yield return new WaitForSeconds(1);
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);
    }
}
