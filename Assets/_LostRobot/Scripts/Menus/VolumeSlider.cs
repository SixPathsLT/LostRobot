using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public VolumeData data;
    private Slider slider;
    public TMPro.TextMeshProUGUI label;
    public string volumeName;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        ResetSliderValue();
    }

    public void OnChange(float volume)
    {
        //Settings.volData.mixer.SetFloat(volumeName, Mathf.Log(volume) * 20f);
        label.text = Mathf.Round(volume * 100).ToString();

        data.SetVolumeLevels(volumeName, volume);

        data.SaveVolumeLevels();
    }

    public void ResetSliderValue()
    {
        if (data)
        {
            if (PlayerPrefs.HasKey(data.prefPrefix + volumeName))
            {
                float volume = data.GetVolumeLevels(volumeName);

                OnChange(volume);
                slider.value = volume;
            }            
        }
    }
}
