using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Melee")]
public class MeleeAbility : Ability
{
    float initialSpeed;

    public override void Activate() {
        base.Activate();
        initialSpeed = AbilitiesManager.player.GetComponent<PlayerMovement>().speed;
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = 0.1f;
        AbilitiesManager.playerAnim.SetInteger("Attack_Index", Random.Range(0, AbilitiesManager.playerAnim.GetInteger("Attack_Max_Index")));
        AbilitiesManager.playerAnim.SetTrigger("AttackTrigger");
    }

    public override void StartCooldown() {
        base.StartCooldown();
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = initialSpeed;
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

    }
    
}
