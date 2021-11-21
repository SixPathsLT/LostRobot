using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed")]
public class SpeedAbility : Ability
{ 
    float initialSpeed;
    float increasedSpeed = 10f;
    public override void Activate() {
        base.Activate();
        initialSpeed = AbilitiesManager.player.GetComponent<PlayerMovement>().speed;
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = increasedSpeed;

        AbilitiesManager.playerAnim.SetBool("SpeedBoost", true);
        
        FindObjectOfType<Notification>().SendNotification("Speed boost activated for " + (int) durationTime + " seconds.");
    }

    public override void StartCooldown() {
        base.StartCooldown();
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = initialSpeed;

        AbilitiesManager.playerAnim.SetBool("SpeedBoost", false);
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

    }

}
