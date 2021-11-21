using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stun")]
public class StunAbility : Ability
{

    public override void Activate() {
        base.Activate();

        AbilitiesManager.playerAnim.SetTrigger("StunTrigger");

        Collider[] colliders = Physics.OverlapSphere(AbilitiesManager.player.transform.position, 10f);
        bool foundEnemies = false;
        foreach (var collider in colliders) {
            AIManager aiManager  = collider.GetComponent<AIManager>();
            if (aiManager != null)
            {
                foundEnemies = true;
                aiManager.StartCoroutine("Stun", durationTime);
                FindObjectOfType<Notification>().SendNotification("Stunned nearby enemies for " + (int)durationTime + " seconds.");
            }
        }

        if (!foundEnemies)
            FindObjectOfType<Notification>().SendNotification("No enemies near you to Stun.");

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
