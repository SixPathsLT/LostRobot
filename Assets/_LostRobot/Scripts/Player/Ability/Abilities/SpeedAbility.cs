using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed")]
public class SpeedAbility : Ability
{ 
    float initialSpeed;
    float increasedSpeed = 10f;
    public override void Activate() {
        if (AbilitiesManager.player.GetComponent<PlayerController>().data.interactedPCs < 6) {
            FindObjectOfType<Notification>().SendNotification("You haven't unlocked this ability yet.");
            return;
        }
        base.Activate();
        initialSpeed = AbilitiesManager.player.GetComponent<PlayerMovement>().speed;
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = increasedSpeed;

        //AbilitiesManager.playerAnim.SetBool("SpeedBoost", true);
    }

    public override void StartCooldown() {
        base.StartCooldown();
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = initialSpeed;

        AbilitiesManager.playerAnim.SetBool("SpeedBoost", false);
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Active)
            return;
        AbilitiesManager.playerAnim.SetBool("SpeedBoost", AbilitiesManager.player.GetComponent<PlayerMovement>().direction.magnitude > 0.1f);
    }

}
