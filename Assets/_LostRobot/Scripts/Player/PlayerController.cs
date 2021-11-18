using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PlayerData data;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update() {
        HandleDeath();
    }

    public void HandleCapture() {
        ReduceConsciousness(10);
        Respawn();
    }

    private void HandleDeath() {
       if (data.GetHealth() < 1) {
            ReduceConsciousness(10);
            Respawn();
       }
    }

    private void Respawn() {
        transform.position = GameObject.Find("RespawnLocation").transform.position;
        data.SetHealth(100);
    }

    private void ReduceConsciousness(int amount) {
        int consciousness = data.GetConcioussness() - amount;
        if (consciousness < 1)
            consciousness = 0;

        data.SetConciousness(consciousness);
    }
}
