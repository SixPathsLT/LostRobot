using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stun")]
public class StunAbility : Ability
{
    float initialSpeed;
    public float stunTime;

    public override void Activate() {
        base.Activate();
        initialSpeed = AbilitiesManager.player.GetComponent<PlayerMovement>().speed;
        AbilitiesManager.player.GetComponent<PlayerMovement>().speed = 0;
        AbilitiesManager.playerAnim.SetTrigger("StunTrigger");

        Collider[] colliders = Physics.OverlapSphere(AbilitiesManager.player.transform.position, 10f);
        foreach (var collider in colliders) {
            AIManager aiManager  = collider.GetComponent<AIManager>();
            if (aiManager != null)
                aiManager.StartCoroutine("Stun", stunTime);
        }

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