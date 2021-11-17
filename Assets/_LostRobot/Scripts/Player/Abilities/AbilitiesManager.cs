using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [HideInInspector]
    public enum AbilityState {
        Ready, Cooldown, Active
    }
    [SerializeField]
    private Ability[] abilities;
    public static GameObject player;

    private Ability selectedAbility;
  
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
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
