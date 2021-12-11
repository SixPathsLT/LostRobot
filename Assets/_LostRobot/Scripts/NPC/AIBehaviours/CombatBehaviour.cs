using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/CombatBehaviour")]
public class CombatBehaviour : AIBehaviour {

    Transform player;

    public override void Init(AIManager aiManager)
    {
    }

    public override void Process(AIManager aiManager) {



        /*
         animator.transform.LookAt(player);
         float distance = Vector3.Distance(animator.transform.position, player.position);
         if (distance > ?)
         */

         
        /*
        player = GameObject.FindGameObjectsWithTag("Player").transform;
        */
    }
   
    

    public override void End(AIManager aiManager) 
{
    
    }



}
