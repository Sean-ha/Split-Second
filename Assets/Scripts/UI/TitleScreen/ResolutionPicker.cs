using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionPicker : MonoBehaviour
{
    private Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    private void Awake()
    {
        resolutions = new Resolution[4];

        Resolution res1 = new Resolution();
        res1.width = 768;
        res1.height = 432;

        Resolution res2 = new Resolution();
        res2.width = 1024;
        res2.height = 576;

        Resolution res3 = new Resolution();
        res3.width = 1280;
        res3.height = 720;

        Resolution res4 = new Resolution();
        res4.width = 1920;
        res4.height = 1080;

        resolutions[0] = res1;
        resolutions[1] = res2;
        resolutions[2] = res3;
        resolutions[3] = res4;
        
        resolutionDropdown = GetComponent<Dropdown>();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = PlayerPrefs.GetInt("Resolution", 2);
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("Resolution", resolutionIndex);

        CanvasScaler[] scalers = FindObjectsOfType<CanvasScaler>();
        

        Resolution resolution = resolutions[resolutionIndex];

        foreach (CanvasScaler scaler in scalers)
        {
            scaler.scaleFactor = resolution.width / 1280f;
        }

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
