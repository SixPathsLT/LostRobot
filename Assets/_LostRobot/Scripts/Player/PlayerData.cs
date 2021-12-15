using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{

    public event Action<int> UIUpdater;
    public int currentHealth, maxHealth, currentConciousness, maxConciousness;//, interactedPCs;

    public List<string> obtainedKeyInfo = new List<string>();
    public List<string> obtainedKeyCards = new List<string>();
    public List<string> readEmails = new List<string>();
    public int level = 1;

    public void SetHealth(int health) {
        if (health > maxHealth)
            health = maxHealth;
        else if (health < 0)
            health = 0;

        currentHealth = health;
        UIUpdater?.Invoke(health);
    }

    public void SetConciousness(int value) {
        if (value > maxConciousness)
            value = maxConciousness;
        else if (value < 0)
            value = 0;

        currentConciousness = value;
        UIUpdater?.Invoke(currentConciousness);
    }

    /*public void IncreasePCCount()
    {
        interactedPCs++;
    }*/


    public void AddEmail(string id) {
        if (readEmails == null)
            readEmails = new List<string>();

        if (readEmails.Contains(id))
            return;

        readEmails.Add(id);
        SetConciousness(currentConciousness + 5);

        foreach (var ability in GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitiesManager>().abilities) {
            if (GetEmailsCount() == ability.requiredReadEmails) {
                FindObjectOfType<CutsceneManager>().PlayCutscene(CutsceneManager.ABILITY_UNLOCKED_CUTSCENE);
                //FindObjectOfType<Notification>().SendNotification(ability.name.Replace("Ability", "").Replace("Data", "") + " Ability Unlocked!");
                break;
            }
        }

        GameManager.GetInstance().Save();
    }

    public void CompletedLevel() {
        level++;
        GameManager.GetInstance().Save();
    }

    public void AddKeyCard(string id) {
        if (obtainedKeyCards == null)
            obtainedKeyCards = new List<string>();

        if (obtainedKeyCards.Contains(id))
            return;

        obtainedKeyCards.Add(id);
        GameManager.GetInstance().Save();
    }

    public void AddObtainedInfo(string id) {
        if (obtainedKeyInfo == null)
            obtainedKeyInfo = new List<string>();

        if (obtainedKeyInfo.Contains(id))
            return;

        obtainedKeyInfo.Add(id);

        GameManager.GetInstance().Save();
    }

    internal void Reset() {
        currentHealth = 1;
        currentConciousness = 0;
        obtainedKeyInfo.Clear();
        obtainedKeyCards.Clear();
        readEmails.Clear();
        level = 1;
    }

    public int GetEmailsCount() { return readEmails.Count; }
    public int GetHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }
    public int GetConcioussness() { return currentConciousness; }
    public int GetMaxConcioussness() { return maxConciousness; }

}