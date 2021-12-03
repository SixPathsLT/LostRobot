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
    public AudioSource abilitySounds;
    public AudioClip[] audioClips;

    public static Ability selectedAbility;
  
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponentInChildren<Animator>();

        selectedAbility = abilities[0];

    }
    
    void Update() {
        if (CutsceneManager.GetActiveCutscene() != null)
            return;
        foreach (Ability ability in abilities) {
            ability.Process();

            if (Input.GetKey(ability.keyCode)) {
                if (selectedAbility != null)
                    selectedAbility.End();

                selectedAbility = ability;
                break;
            }
        }
    }


    public void PlayAudio(int clip)
    {
        abilitySounds.clip = audioClips[clip];
        abilitySounds.Play();
    }
    internal bool UsingCloakingAbility() {
        return selectedAbility.GetType() == typeof(CloakingAbility) && selectedAbility.IsActive() && ((CloakingAbility)selectedAbility).hidingSpot != null && ((CloakingAbility)selectedAbility).hidingSpot.activeSelf;
    }
}
