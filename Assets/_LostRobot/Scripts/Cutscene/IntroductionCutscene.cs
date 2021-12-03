using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionCutscene : Cutscene {

    public GameObject coolDown;
    public GameObject abilityIcon;

    PlayerData playerData;

    struct Data {
        public bool canGain;
        public float value;
    }

    Data health, consciousness;

    public override void Init() {
        base.Init();

        health.value = 1f;
        playerData = player.GetComponent<PlayerController>().data;
        coolDown.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        abilityIcon.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        playerData.SetConciousness(0);
        playerData.SetHealth(1);
        StartCoroutine(StartWakeup());
    }

    
    Color startColor = Color.black;
    public override void Process(){
        float speed = 10;
        if (consciousness.canGain) {
            consciousness.value += speed * Time.deltaTime;
            if (consciousness.value > 50)
                consciousness.value = 50;
           
            playerData.SetConciousness((int) consciousness.value);
        }

        if (health.canGain) {
            health.value += speed * Time.deltaTime;
            if (health.value > 100)
                health.value = 100;

            Color lerpedColor = Color.Lerp(startColor, player.GetComponent<PlayerUIControll>().coolDown, (health.value * 1.5f) * Time.deltaTime);

            coolDown.GetComponent<Renderer>().material.SetColor("_EmissionColor", lerpedColor * 12f);
            abilityIcon.GetComponent<Renderer>().material.SetColor("_EmissionColor", lerpedColor * 6f);

            playerData.SetHealth((int) health.value);
        }
    }

    public override void Stop() {
        base.Stop();

        AbilitiesManager.selectedAbility = player.GetComponent<AbilitiesManager>().abilities[0];
    }
        

    IEnumerator StartWakeup() {
        yield return new WaitForSeconds(6);
        health.canGain = true;
        yield return new WaitForSeconds(2);
        consciousness.canGain = true;

        Ability[] abilities = player.GetComponent<AbilitiesManager>().abilities;

        foreach (var ability in abilities) {
            AbilitiesManager.selectedAbility = ability;
            yield return new WaitForSeconds(1);
        }
    }

}
