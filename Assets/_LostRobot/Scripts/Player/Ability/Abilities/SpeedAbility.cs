using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed")]
public class SpeedAbility : Ability
{ 
    float initialSpeed;
    float increasedSpeed = 10f;

    PlayerMovement playerMovement;

    public override void Activate() {
        base.Activate();
        playerMovement = AbilitiesManager.player.GetComponent<PlayerMovement>();
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
        if (playerMovement == null)
            playerMovement = AbilitiesManager.player.GetComponent<PlayerMovement>();

        AbilitiesManager.playerAnim.SetBool("SpeedBoost", playerMovement.direction.magnitude > 0.1f);
    }

}
