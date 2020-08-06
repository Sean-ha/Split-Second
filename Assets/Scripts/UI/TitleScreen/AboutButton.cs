using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButton : MonoBehaviour
{
    public GameObject aboutPanel;

    public void OnAboutButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);

        aboutPanel.SetActive(true);
    }

    public void OnCloseButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISoundReverse);

        aboutPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(aboutPanel.activeInHierarchy)
            {
                SoundManager.PlaySound(SoundManager.Sound.UISoundReverse);
                aboutPanel.SetActive(false);
            }
        }
    }
}
