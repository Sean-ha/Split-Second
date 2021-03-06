﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    private PostProcessing postProcessor;
    private int currentLevel;

    private MenuFadeBlack fader;

    private void Awake()
    {
        fader = FindObjectOfType<MenuFadeBlack>();
    }

    void Start()
    {
        postProcessor = FindObjectOfType<PostProcessing>();

        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;

        if (PlayerPrefs.GetInt("CurrentLevel", -1) != -1)
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }

    // Called when continue button is clicked.
    public void OnContinueButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);

        // Disables all other buttons so player can't click them during the transition
        GetComponent<UnityEngine.UI.Button>().interactable = false;
        FindObjectOfType<NewGameButton>().gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;

        postProcessor.LensDistortionMainMenu(1);
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        yield return new WaitForSeconds(.35f);

        fader.FadeIn();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(currentLevel);
    }
}
