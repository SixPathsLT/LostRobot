using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAbility : Ability
{
    
    public override void Activate() {
        base.Activate();

        AbilitiesManager.player.GetComponentInChildren<Animator>().SetBool("Attack", true);
    }

    public override void StartCooldown() {
        base.StartCooldown();
        AbilitiesManager.player.GetComponentInChildren<Animator>().SetBool("Attack", false);
       
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

    }
    
}
