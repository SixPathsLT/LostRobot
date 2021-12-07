using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Cloaking")]
public class CloakingAbility : Ability
{
    public GameObject hidingPrefab;

    [HideInInspector]
    public GameObject hidingSpot;

    public override void Activate() {
        base.Activate();

        hidingSpot = Instantiate(hidingPrefab);
    }

    public override void StartCooldown() {
        base.StartCooldown();

        End();
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Active)
            return;

        if (IsActive() && hidingSpot != null) {
            hidingSpot.transform.position = AbilitiesManager.player.transform.position;
            hidingSpot.SetActive(!(AbilitiesManager.player.GetComponent<PlayerMovement>().direction.magnitude > 0.1f));
        }
    }

    public override void End() {
        if (hidingSpot != null)
            Destroy(hidingSpot);
    }


}
