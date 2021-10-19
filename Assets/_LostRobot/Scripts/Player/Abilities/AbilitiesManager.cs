using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    [HideInInspector]
    public enum AbilityState {
        Ready, Cooldown, Active
    }
    [SerializeField]
    private Ability[] abilities;
  
    void Start() { }
    
    void Update() {
        foreach (Ability ability in abilities) {
            ability.Process();
        }
    }

}
