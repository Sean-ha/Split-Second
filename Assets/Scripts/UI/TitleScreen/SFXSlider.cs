using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Text sfxValue;

    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void OnSliderChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", slider.value);
        sfxValue.text = (slider.value * 100).ToString("n0") + "%";
    }
}
