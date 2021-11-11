using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAbility : Ability
{
    
    public override void Activate() {
        base.Activate();

        AbilitiesManager.player.GetComponentInChildren<Animator>().SetInteger("Attack_Index", Random.Range(0, AbilitiesManager.player.GetComponentInChildren<Animator>().GetInteger("Attack_Max_Index")));
        AbilitiesManager.player.GetComponentInChildren<Animator>().SetTrigger("AttackTrigger");
    }

    public override void StartCooldown() {
        base.StartCooldown();
        //AbilitiesManager.player.GetComponentInChildren<Animator>().SetBool("Attack", false);
       
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

    }
    
}
