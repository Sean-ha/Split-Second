using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    public Text bgmValue;

    private AudioSource bgm;
    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();

        slider.value = PlayerPrefs.GetFloat("BGMVolume", .75f);
    }

    public void ChangeBGMVolume()
    {
        bgm.volume = slider.value;
        PlayerPrefs.SetFloat("BGMVolume", slider.value);
        bgmValue.text = (slider.value * 100).ToString("n0") + "%";
    }
}
