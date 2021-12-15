using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    public VolumeData data;
    public List<VolumeSlider> sliders = new List<VolumeSlider>();

    public void SetData(VolumeData data)
    {
        //Settings.volData = data;
    }

    private void Awake()
    {
        data.GetVolumeLevels();
        if (data != null)
        {
            SetData(data);
        }
    }

    private void Start()
    {
        data.GetVolumeLevels();
    }

    public void ApplyChanges()
    {
        data.SaveVolumeLevels();
        PlayerPrefs.Save();
    }
}
