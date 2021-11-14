using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

   public MeleeAbility meleeAbility;

    void Update()
    { 
       GetComponent<BoxCollider>().isTrigger = meleeAbility.IsActive(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        AIManager enemy = other.GetComponent<AIManager>();

        if (enemy != null)
        {
            //damage enemy
        }

    }
}
