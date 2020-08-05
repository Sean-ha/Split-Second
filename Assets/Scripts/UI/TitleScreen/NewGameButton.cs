using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    private PostProcessing postProcessor;
    private MenuFadeBlack fader;

    private void Awake()
    {
        fader = FindObjectOfType<MenuFadeBlack>();
        postProcessor = FindObjectOfType<PostProcessing>();
    }

    public void OnNewGameButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);

        // Disables all other buttons so player can't click them during the transition
        GetComponent<UnityEngine.UI.Button>().interactable = false;
        FindObjectOfType<ContinueButton>().gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;


        postProcessor.LensDistortionMainMenu(1);
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        yield return new WaitForSeconds(.35f);

        fader.FadeIn();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }
}
