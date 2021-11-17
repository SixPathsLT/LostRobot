using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CombatBehaviour")]
public class CombatBehaviour : AIBehaviour {

    Transform player;

    public override void Init(GameObject gameObject) {
        base.Init(gameObject);
        /*
        player = GameObject.FindGameObjectsWithTag("Player").transform;
        */
    }

    public override void Process() {

        /*
         animator.transform.LookAt(player);
         float distance = Vector3.Distance(animator.transform.position, player.position);
         if (distance > ?)
         */

         
   
    }

    public override void End() {

    }

}
