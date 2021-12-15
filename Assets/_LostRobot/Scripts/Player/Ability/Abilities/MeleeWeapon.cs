
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

   MeleeAbility meleeAbility;
   CapsuleCollider weapon;

   private void Start() {
        weapon = GetComponent<CapsuleCollider>();
        meleeAbility = FindObjectOfType<AbilitiesManager>().abilities[3] as MeleeAbility;//AbilitiesManager.player.GetComponent<AbilitiesManager>().abilities[3] as MeleeAbility;
   }


    private void OnTriggerStay(Collider other) {
        if (!meleeAbility.IsActive())
            return;
        AIManager enemy = other.GetComponent<AIManager>();

        if (enemy != null)
            enemy.TakeDamage();
       
    }
}
