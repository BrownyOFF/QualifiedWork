using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

public class SettingsScript : MonoBehaviour
{
    public GameObject sfx;
    public GameObject mfx;
    public GameObject vsync;
    private Slider sfxComponent;
    private Slider mfxComponent;
    private Toggle vsyncComponent;

    public void SFXChange(float value)
    {
        PlayerPrefs.SetFloat("SFX", value);
        PlayerPrefs.Save();
    }
    public void MFXChange(float value)
    {
        PlayerPrefs.SetFloat("MFX", value);
        PlayerPrefs.Save();
    }
    public void VSyncChange(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("VSync", 1);
        }
        else
        {
            PlayerPrefs.SetInt("VSync", 0);
        }
        PlayerPrefs.Save();
    }
    public void ChangeSettings()
    {
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("Vsync");
        PlayerPrefs.Save();
    }
    public void UpDateSettings()
    {
        sfxComponent.value = PlayerPrefs.GetFloat("SFX");
        mfxComponent.value = PlayerPrefs.GetFloat("MFX");
        bool vsyncEnabled = PlayerPrefs.GetInt("Vsync") == 1;
        vsyncComponent.isOn = vsyncEnabled;
    }
    void Start()
    {
        sfxComponent = sfx.GetComponentInChildren<Slider>();
        mfxComponent = mfx.GetComponentInChildren<Slider>();
        vsyncComponent = vsync.GetComponentInChildren<Toggle>();
        UpDateSettings();
    }

    void Update()
    {
        
    }
}
