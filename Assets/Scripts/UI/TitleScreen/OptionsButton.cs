using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public GameObject optionsPanel;

    private void Start()
    {
        optionsPanel.SetActive(false);
    }

    public void OnOptionsButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);

        optionsPanel.SetActive(true);
    }

    public void CloseOptionsPanelButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISoundReverse);

        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(optionsPanel.activeInHierarchy)
            {
                SoundManager.PlaySound(SoundManager.Sound.UISoundReverse);
                optionsPanel.SetActive(false);
            }
        }
    }
}
