
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControll : MonoBehaviour
{
    public PlayerData data;
    public AbilitiesManager abilityInfo;

    public Gradient healthGradient;
    public Gradient conciousnessGradient;
    public Gradient coolDownGradient;

    public Color health;
    public Color conciousness;
    public Color coolDown;
    public Texture selectedAbility;

    public Renderer[] healthIndicators;
    public int[] hIndexes;
    public Renderer[] concioussnessIndicators;
    public Text pcCounter;
    public int[] cIndexes;
    public Renderer[] coolDownIndicators;
    public int[] cDIndexes;
    public GameObject abilityIcon;

    // Update is called once per frame
    void Update()
    {
        health = healthGradient.Evaluate(ConvertValue(data.GetHealth(), data.GetMaxHealth()));
        conciousness = conciousnessGradient.Evaluate(ConvertValue(data.GetConcioussness(), data.GetMaxConcioussness()));
        UpdateHealthIndicators();
        UpdateConciousnessIndicators();
        UpdateCurrentAbility();
    }

    private float ConvertValue(float current, float max)
    {
        float percent = (current / max);
        return percent;
    }

    private void UpdateHealthIndicators()
    {
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            healthIndicators[i].materials[hIndexes[i]].color = health;
            healthIndicators[i].materials[hIndexes[i]].SetColor("_EmissionColor", health * 4f);
        }
    }

    private void UpdateConciousnessIndicators()
    {
        for (int i = 0; i < concioussnessIndicators.Length; i++)
        {
            concioussnessIndicators[i].materials[cIndexes[i]].color = conciousness;
            concioussnessIndicators[i].materials[cIndexes[i]].SetColor("_EmissionColor", conciousness * 4f);
        }
        pcCounter.text = "" + data.GetEmailsCount();//.interactedPCs;
        pcCounter.material.color = conciousness;
        pcCounter.material.SetColor("_EmissionColor", conciousness * 4f);
    }

    private void UpdateCurrentAbility()
    {

        var state = AbilitiesManager.selectedAbility.state;
        switch (state)
        {
            case AbilitiesManager.AbilityState.Ready:
                coolDown = coolDownGradient.Evaluate(1);
                break;
            case AbilitiesManager.AbilityState.Cooldown:
                coolDown = coolDownGradient.Evaluate(0);
                break;
            case AbilitiesManager.AbilityState.Active:
                coolDown = coolDownGradient.Evaluate(.5f);
                break;
        }
        selectedAbility = AbilitiesManager.selectedAbility.icon;
        abilityIcon.GetComponent<Renderer>().material.SetTexture("_MainTex", selectedAbility);

        bool canModify = !GameManager.GetInstance().InCutsceneState() || CutsceneManager.timeElapsed > 10f;

            for (int i = 0; i < concioussnessIndicators.Length; i++)
            {
                coolDownIndicators[i].materials[cDIndexes[i]].color = coolDown;
                if (canModify)
                    coolDownIndicators[i].materials[cDIndexes[i]].SetColor("_EmissionColor", coolDown * 12f);
            }

            abilityIcon.GetComponent<Renderer>().material.color = coolDown;
        if (canModify)
            abilityIcon.GetComponent<Renderer>().material.SetColor("_EmissionColor", coolDown);
        
    }
}
