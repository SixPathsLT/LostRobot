using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed")]
public class SpeedAbility : Ability
{ 
    float initialSpeed;
    float increasedSpeed = 16f;
    public override void Activate() {
        base.Activate();
        initialSpeed = AbilitiesManager.player.GetComponent<PlayerMovement>().speed;
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = increasedSpeed;

        AbilitiesManager.player.GetComponentInChildren<Animator>().SetBool("SpeedBoost", true);
    }

    public override void StartCooldown() {
        base.StartCooldown();
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = initialSpeed;

        AbilitiesManager.player.GetComponentInChildren<Animator>().SetBool("SpeedBoost", false);
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Ready)
            return;

       

    }

}
