using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {

    public event Action<int> UIUpdater;
    public int currentHealth, maxHealth, currentConciousness, maxConciousness, interactedPCs;

    public void SetHealth(int health) {
        this.currentHealth = health;
        UIUpdater?.Invoke(health);
    }

    public void SetConciousness(int value) {
        this.currentConciousness = value;
        UIUpdater?.Invoke(currentConciousness);
    }

    public void IncreasePCCount()
    {
        interactedPCs++;
        switch (interactedPCs) {
            case 3:
                FindObjectOfType<Notification>().SendNotification("Unlocked Cloaking Ability!");
                break;
            case 6:
                FindObjectOfType<Notification>().SendNotification("Unlocked Speed Ability!");
                break;
            case 9:
                FindObjectOfType<Notification>().SendNotification("Unlocked Stun Ability!");
                break;
            case 12:
                FindObjectOfType<Notification>().SendNotification("Unlocked Melee Ability!");
                break;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetConcioussness()
    {
        return currentConciousness;
    }

    public int GetMaxConcioussness()
    {
        return maxConciousness;
    }
}