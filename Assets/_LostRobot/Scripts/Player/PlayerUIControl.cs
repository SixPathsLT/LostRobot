
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIControl : MonoBehaviour
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
    private float tempH;
    private float tempC;
    private float tempCD;
    private float targetH;
    private float targetC;
    private float targetCD;
    private float tC, tH, tCD;

    public Renderer[] healthIndicators;
    public int[] hIndexes;
    public Renderer[] concioussnessIndicators;
    public Text pcCounter;
    public int[] cIndexes;
    public Renderer[] coolDownIndicators;
    public int[] cDIndexes;
    public GameObject abilityIcon;

    private AnimationCurve pulse;
    public float maxEmission;
    public float minEmission;
    public float pulseTime;
    private float emission;
    private float time = 0;

    private void Start()
    {
        pulse = new AnimationCurve();
        pulse.AddKey(0, minEmission);
        pulse.AddKey(pulseTime / 2, maxEmission);
        pulse.AddKey(pulseTime, minEmission);

        tempH = data.GetHealth();
        tempC = data.GetConcioussness();
        //tempCD;
        targetH = tempH;
        targetC = tempC;
        //targetCD;
}
    // Update is called once per frame
    void Update()
    {
        emission = pulse.Evaluate(time);
        if (time < pulseTime)
            time += Time.deltaTime;
        else
            time = 0;

        UpdateHealthIndicators();
        UpdateConciousnessIndicators();
        UpdateCurrentAbility();

        if (targetC != data.GetConcioussness())
            targetC = data.GetConcioussness();
        if (targetH != data.GetHealth())
            targetH = data.GetHealth();
        var state = AbilitiesManager.selectedAbility.state;
        switch (state)
        {
            case AbilitiesManager.AbilityState.Ready:
                targetCD = 1;
                break;
            case AbilitiesManager.AbilityState.Cooldown:
                targetCD = 0;
                break;
            case AbilitiesManager.AbilityState.Active:
                targetCD = .5f;
                break;
        }

        if (tempH != targetH)
        {
            float dif = 0;
            dif = tempH - targetH;

            dif *= Time.deltaTime;
            tempH -= dif;
        }
        if (tempC != targetC)
        {
            float dif = 0;
            dif = tempC - targetC;

            dif *= Time.deltaTime;
            tempC -= dif;
        }
        if (tempCD != targetCD)
        {
            float dif = 0;
            dif = tempCD - targetCD;

            dif *= Time.deltaTime;
            tempCD -= dif;
        }
        conciousness = conciousnessGradient.Evaluate(ConvertValue(tempC, data.GetMaxConcioussness()));
        coolDown = coolDownGradient.Evaluate(tempCD);
        health = healthGradient.Evaluate(ConvertValue(tempH, data.GetMaxHealth()));
        
        
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
            healthIndicators[i].materials[hIndexes[i]].SetColor("_EmissionColor", health * emission);
        }
    }

    private void UpdateConciousnessIndicators()
    {
        for (int i = 0; i < concioussnessIndicators.Length; i++)
        {
            concioussnessIndicators[i].materials[cIndexes[i]].color = conciousness;
            concioussnessIndicators[i].materials[cIndexes[i]].SetColor("_EmissionColor", conciousness * emission);
        }
        pcCounter.text = "" + data.GetEmailsCount();//.interactedPCs;
        pcCounter.material.color = conciousness;
        pcCounter.color = conciousness;
        pcCounter.material.SetColor("_EmissionColor", conciousness * emission);
    }

    private void UpdateCurrentAbility()
    {      

        selectedAbility = AbilitiesManager.selectedAbility.icon;
        abilityIcon.GetComponent<Renderer>().material.SetTexture("_MainTex", selectedAbility);

        bool canModify = !GameManager.GetInstance().InCutsceneState() || CutsceneManager.timeElapsed > 10f;

            for (int i = 0; i < concioussnessIndicators.Length; i++)
            {
                coolDownIndicators[i].materials[cDIndexes[i]].color = coolDown;
                if (canModify)
                    coolDownIndicators[i].materials[cDIndexes[i]].SetColor("_EmissionColor", coolDown * emission * 2);
            }

            abilityIcon.GetComponent<Renderer>().material.color = coolDown;
        if (canModify)
            abilityIcon.GetComponent<Renderer>().material.SetColor("_EmissionColor", coolDown * emission * .5f);
        
    }
}
