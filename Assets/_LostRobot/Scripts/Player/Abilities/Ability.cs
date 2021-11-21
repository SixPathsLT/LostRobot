using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilitiesManager;

public abstract class Ability : ScriptableObject
{
    [SerializeField] public KeyCode keyCode;
    [SerializeField] protected float durationTime, coolDownTime;
    [SerializeField] public Texture icon;

    [HideInInspector]
    public AbilityState state = AbilityState.Ready;
    protected float elapsedTime;

    public virtual void Activate() {
        SetState(AbilityState.Active);
    }

    public virtual void Process() {
        switch (state) {
            case AbilityState.Ready:
                if (Input.GetMouseButton(0))
                    Activate();
                break;
            default:
                if (state == AbilityState.Active && elapsedTime >= durationTime)
                    StartCooldown();                
                else if (state == AbilityState.Cooldown && elapsedTime >= coolDownTime)
                    SetState(AbilityState.Ready);
                else
                    elapsedTime += Time.deltaTime;
                break;
        }
    }

    public virtual void StartCooldown() {
        SetState(AbilityState.Cooldown);
    }

    protected void SetState(AbilityState state) {
        elapsedTime = 0;
        this.state = state;
    }

    public bool IsActive()
    {
        return state == AbilityState.Active;
    }

}
