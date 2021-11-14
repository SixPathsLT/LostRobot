using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stun")]
public class StunAbility : Ability
{

    public override void Activate() {
        base.Activate();

        Collider[] colliders = Physics.OverlapSphere(AbilitiesManager.player.transform.position, 10f);
        foreach (var collider in colliders) {
            AIManager aiManager  = collider.GetComponent<AIManager>();
            if (aiManager != null)
                aiManager.StartCoroutine("Stun", durationTime);
        }

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
