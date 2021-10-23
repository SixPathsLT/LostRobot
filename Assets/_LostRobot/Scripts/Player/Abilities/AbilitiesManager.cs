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
  
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update() {
        foreach (Ability ability in abilities) {
            ability.Process();
        }
    }

}
