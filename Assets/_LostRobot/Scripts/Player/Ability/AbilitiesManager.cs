
using UnityEngine;
using UnityEngine.UI;

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
    public Renderer[] cloakMesh;
    public Text cloakPC;
    public Renderer cloakAbility;

    public static Ability selectedAbility;
  
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponentInChildren<Animator>();
        selectedAbility = abilities[0];
    }
    
    void Update() {
        foreach (Ability ability in abilities) {
            ability.Process();

            if (Input.GetKey(ability.keyCode) && GameManager.GetInstance().InPlayingState()) {
                if (selectedAbility != null && selectedAbility.IsActive())
                    selectedAbility.StartCooldown();

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
        return selectedAbility.GetType() == typeof(CloakingAbility) && selectedAbility.IsActive();
    }
    internal bool UsingSpeedAbility()
    {
        return selectedAbility.GetType() == typeof(SpeedAbility) && selectedAbility.IsActive();
    }
}
