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
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            //damage enemy
        }

    }
}
