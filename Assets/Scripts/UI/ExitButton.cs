using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    private PostProcessing postProcessor;
    private MenuFadeBlack fader;

    private void Awake()
    {
        postProcessor = FindObjectOfType<PostProcessing>();
        fader = FindObjectOfType<MenuFadeBlack>();
    }

    public void OnExitButtonClick()
    {
        PlayerController.SetCanMove(false);
        PlayerController.isPaused = false;

        Time.timeScale = 1;

        SoundManager.PlaySound(SoundManager.Sound.UISound);

        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;

        postProcessor.LensDistortion(1);
        postProcessor.BendScreen(.75f);

        StartCoroutine("FadeScreen");
    }

    IEnumerator FadeScreen()
    {
        yield return new WaitForSeconds(.35f);

        fader.gameObject.SetActive(true);
        fader.FadeIn();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }
}
