using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilitiesManager;

public abstract class Ability : ScriptableObject
{
    [SerializeField] public KeyCode keyCode;
    [SerializeField] protected float durationTime, coolDownTime;
    public int requiredReadEmails;
    [SerializeField] public Texture icon;

    [HideInInspector]
    public AbilityState state = AbilityState.Ready;
    protected float elapsedTime;

    public virtual void Activate() {
        AbilitiesManager.player.GetComponent<AbilitiesManager>().PlayAudio(0);
        SetState(AbilityState.Active);
    }

    public virtual void Process() {
        
        switch (state) {
            case AbilityState.Ready:
                if (AbilitiesManager.selectedAbility == this &&  Input.GetMouseButton(0) && GameManager.GetInstance().InPlayingState()) {
                    if (player.GetComponent<PlayerController>().data.GetConcioussness() < 1)
                        FindObjectOfType<Notification>().SendNotification("You need to increase your consciousness to be able to use abilities.");
                    else if (player.GetComponent<PlayerController>().data.GetEmailsCount() < requiredReadEmails)
                        FindObjectOfType<Notification>().SendNotification("You need to read " + requiredReadEmails+ " emails to unlock this ability.");
                    else
                        Activate();
                }
                break;
            default:
                if (state == AbilityState.Active && elapsedTime >= durationTime)
                    StartCooldown();                
                else if (state == AbilityState.Cooldown && elapsedTime >= coolDownTime)
                {
                    AbilitiesManager.player.GetComponent<AbilitiesManager>().PlayAudio(1);
                    SetState(AbilityState.Ready);
                }                    
                else
                    elapsedTime += Time.deltaTime;
                break;
        }
    }

    public virtual void End() {}

    public virtual void StartCooldown() {
        SetState(AbilityState.Cooldown);
    }

    protected void SetState(AbilityState state) {
        elapsedTime = 0;
        this.state = state;
    }

    public bool IsActive() {
        return state == AbilityState.Active;
    }

}
