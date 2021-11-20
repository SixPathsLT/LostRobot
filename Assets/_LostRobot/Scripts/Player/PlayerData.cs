using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {

    public event Action<int> UIUpdater;
    public int currentHealth, maxHealth, currentConciousness, maxConciousness;

    public void SetHealth(int health) {
        this.currentHealth = health;
        UIUpdater?.Invoke(health);
    }

    public void SetConciousness(int value) {
        this.currentConciousness = value;
        UIUpdater?.Invoke(currentConciousness);
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