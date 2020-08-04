using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    public RectTransform levelCompleteImage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), .75f, 0.5f);
        LeanTween.alpha(levelCompleteImage, 1, 0.5f);
    }

    public void FullFadeIn()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 1, 0.65f);
        LeanTween.alpha(levelCompleteImage, 0, 0.65f);
    }
}
