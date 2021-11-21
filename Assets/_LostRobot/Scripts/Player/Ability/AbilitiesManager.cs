using System;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [HideInInspector]
    public enum AbilityState {
        Ready, Cooldown, Active
    }
    [SerializeField]
    public Ability[] abilities;
    public static GameObject player;
    public static Animator playerAnim;

    public Ability selectedAbility;
  
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponentInChildren<Animator>();
    }
    
    void Update() {
        foreach (Ability ability in abilities) {
            if (Input.GetKey(ability.keyCode)) {
                if (selectedAbility != null)
                    selectedAbility.End();

                selectedAbility = ability;
                break;
            }
        }
        
        if (selectedAbility != null)
            selectedAbility.Process();
    }

    internal bool UsingCloakingAbility() {
        return selectedAbility == abilities[3] && selectedAbility.IsActive() && ((CloakingAbility)selectedAbility).hidingSpot != null && ((CloakingAbility)selectedAbility).IsActive();
    }
}
