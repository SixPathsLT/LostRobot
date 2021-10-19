using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAbility : Ability
{
    
    public override void Activate() {
        base.Activate();

    }

    public override void StartCooldown() {
        base.StartCooldown();

    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

    }
    
}
