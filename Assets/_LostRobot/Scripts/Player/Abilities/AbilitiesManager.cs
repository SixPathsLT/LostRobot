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
                selectedAbility = ability;
                break;
            }
        }
        
        if (selectedAbility != null)
            selectedAbility.Process();
    }

}
