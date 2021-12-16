using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Abilities/Cloaking")]
public class CloakingAbility : Ability
{
    public GameObject hidingPrefab;
    public bool cloaked;
    public float speed = .5f;

    private float t;
    private float tt;
    public float pcAlpha;

    [HideInInspector]
    public GameObject hidingSpot;

    public override void Activate() {
        base.Activate();

        cloaked = true;
        //hidingSpot = Instantiate(hidingPrefab);
    }

    public override void StartCooldown() {
        base.StartCooldown();

        End();
    }

    public override void Process() {
        base.Process();
        if (state != AbilitiesManager.AbilityState.Active)
            return;

        //if (IsActive() && hidingSpot != null)
        //{
        //    hidingSpot.transform.position = AbilitiesManager.player.transform.position;
        //    hidingSpot.SetActive(!(AbilitiesManager.player.GetComponent<PlayerMovement>().direction.magnitude > 0.1f));
        //}
        if (AbilitiesManager.player.GetComponent<PlayerMovement>().direction.magnitude > 0.1f || elapsedTime >= durationTime-1f)
            cloaked = false;
        else
            cloaked = true;
        foreach(Renderer mesh in AbilitiesManager.player.GetComponent<AbilitiesManager>().cloakMesh)
            SetMesh(mesh);
        SetMesh(AbilitiesManager.player.GetComponent<AbilitiesManager>().cloakPC);
        SetMesh(AbilitiesManager.player.GetComponent<AbilitiesManager>().cloakAbility, 1);
    }

    public override void End() {
        //if (hidingSpot != null)
        //    Destroy(hidingSpot);
        cloaked = false;
    }

    private void SetMesh(Renderer mesh)
    {
        Material[] mats = mesh.materials;

        for (int i = 0; i < mats.Length; i++)
        {
            if (cloaked && t <= (Mathf.PI * 5) / 4)
            {
                if (t <= (Mathf.PI * 5) / 4)
                {                    
                    mats[i].SetFloat("_Cutoff", .75f * Mathf.Sin(t * speed));
                    t += Time.deltaTime;
                }
            }

            if (!cloaked)
            {
                if (t >= 0)
                {                    
                    mats[i].SetFloat("_Cutoff", Mathf.Sin(t * speed));
                    t -= Time.deltaTime;
                    if (t < 0)
                        t = 0;
                }
            }
        }
        mesh.materials = mats;
    }

    private void SetMesh(Text text)
    {
        float alpha = 1 - Mathf.Sin(t * speed);        
        Color colorT = text.color;
        Color colorR = text.material.color;
        colorT.a = alpha;
        colorR.a = alpha;
        text.color = colorT;
        text.material.color = colorR;

        if (cloaked && tt <= ((Mathf.PI * 5) / 4) + .1f)
        {
            if (tt <= (Mathf.PI * 5) / 4)
            {
                tt += Time.deltaTime;
            }
        }
        if (!cloaked)
        {
            if (tt >= 0)
            {
                tt -= Time.deltaTime;
                if (tt < 0)
                    tt = 0;                
            }
        }
        if (tt < .7f)
            alpha = 0;
        else if (tt > .7f)
            alpha = 1;
    }

    private void SetMesh(Renderer mesh, int empty)
    {
        float alpha = 1 - Mathf.Sin(t * speed);
        Color colorR = mesh.material.color;
        colorR.a = alpha;
        mesh.material.color = colorR;

        if (cloaked && tt <= ((Mathf.PI * 5) / 4) + .1f)
        {
            if (tt <= (Mathf.PI * 5) / 4)
            {
                tt += Time.deltaTime;
            }
        }
        if (!cloaked)
        {
            if (tt >= 0)
            {
                tt -= Time.deltaTime;
                if (tt < 0)
                    tt = 0;
            }
        }
        if (tt < .7f)
            alpha = 0;
        else if (tt > .7f)
            alpha = 1;
    }
}
