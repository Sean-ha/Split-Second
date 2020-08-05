using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFadeBlack : MonoBehaviour
{
    public void FadeIn()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 1f, 1f);
    }
}
