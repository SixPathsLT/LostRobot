using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject {

    public event Action<int> UIUpdater;
    [SerializeField] int health, conciousness;

    public void SetHealth(int health) {
        this.health = health;
        UIUpdater?.Invoke(health);
    }

    public void SetConciousness(int value) {
        this.conciousness = value;
        UIUpdater?.Invoke(conciousness);
    }

}