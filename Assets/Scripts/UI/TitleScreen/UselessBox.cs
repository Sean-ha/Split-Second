using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UselessBox : MonoBehaviour
{
    private Toggle toggle;
    private int clickCount;
    public Text toggleText;

    private static bool first;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        if(PlayerPrefs.GetInt("Toggle") == 1)
        {
            first = true;
        }
        toggle.isOn = PlayerPrefs.GetInt("Toggle") != 0;
        
    }

    public void OnBoxClick()
    {
        PlayerPrefs.SetInt("Toggle", (toggle.isOn ? 1 : 0));
        if(!first)
        {
            SoundManager.PlaySound(SoundManager.Sound.UISound);
        }
        clickCount++;

        switch(clickCount)
        {
            case 50: toggleText.text = "WHAT ARE YOU DOING?"; break;
            case 100: toggleText.text = "STOP"; break;
            case 150: toggleText.text = "YOU'RE WASTING YOUR TIME"; break;
            case 200: toggleText.text = "..."; break;
            case 250: toggleText.text = "WELL, SINCE YOU'RE ALREADY HERE..."; break;
            case 300: toggleText.text = "THANKS FOR PLAYING MY GAME."; break;
            case 350: toggleText.text = "HAVE A GREAT DAY."; break;
            case 500: toggleText.text = "YOU'RE STILL GOING?"; break;
            case 550: toggleText.text = "YOU'RE PRETTY DEDICATED TO THIS."; break;
            case 600: toggleText.text = "THERE'S NOTHING AFTER THIS."; break;
            case 1000: toggleText.text = "USEFUL BOX"; break;
            case 1050: toggleText.text = "OK THIS IS REALLY THE END."; break;
            case 1065: toggleText.text = "THANKS FOR JOINING ME."; break;
            case 1080: toggleText.text = "FAREWELL. IT WAS FUN."; break;
        }
        first = false;
    }
}
