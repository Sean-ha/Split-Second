using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int levelToEnter;

    private FadeBlack fader;

    private void Awake()
    {
        fader = FindObjectOfType<FadeBlack>();
    }

    public void EnterPortal()
    {
        StartCoroutine("FadeScreen");
    }

    IEnumerator FadeScreen()
    {
        PlayerPrefs.SetInt("CurrentLevel", levelToEnter);

        yield return new WaitForSeconds(1.5f);

        fader.gameObject.SetActive(true);
        fader.FadeIn();

        yield return new WaitForSeconds(2f);

        fader.FullFadeIn();

        yield return new WaitForSeconds(0.75f);
       
        SceneManager.LoadScene(levelToEnter);
    }
}
