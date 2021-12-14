
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
        if (!GameManager.GetInstance().InPlayingState())
            return;

        HandleDeath();

    }

    public void HandleCapture() {
        ReduceConsciousness(10);
        Respawn();
        FindObjectOfType<Notification>().SendNotification("You were detected by the AI. Your consciousness level is now " + data.GetConcioussness() + ".");
        GameManager.GetInstance().ChangeState(GameManager.State.Playing);

    }

    private void HandleDeath() {
       if (data.GetHealth() < 1) {
            ReduceConsciousness(10);
            Respawn();
       }
    }

    private void Respawn() {
        transform.position = GameObject.Find("RespawnLocation").transform.position;
        /*transform.position = respawnPoint;
        data.currentHealth = checkpoint.currentHealth;
        data.maxHealth = checkpoint.maxHealth;
        data.currentConciousness = checkpoint.currentConciousness;
        data.maxConciousness = checkpoint.maxConciousness;*/
        //data.SetHealth(100);
        GameManager.GetInstance().OnRespawn();
    }

    private void ReduceConsciousness(int amount) {
        int consciousness = data.GetConcioussness() - amount;
        if (consciousness < 1)
            consciousness = 0;

        data.SetConciousness(consciousness);
    }
}
