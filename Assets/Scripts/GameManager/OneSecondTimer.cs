using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSecondTimer : MonoBehaviour
{
    private Image timerImage;

    void Awake()
    {
        timerImage = GetComponent<Image>();
        timerImage.enabled = false;
    }

    public void StartOneSecondTimer()
    {
        timerImage.enabled = true;
        StartCoroutine("TimerCountDown");
    }

    // The circle counts down for one second
    IEnumerator TimerCountDown()
    {
        float startTime = Time.time;
        float time = 1;

        while(Time.time - startTime <= 1)
        {
            time -= Time.deltaTime;

            timerImage.fillAmount = time;
            yield return null;
        }
        timerImage.enabled = false;
    }
}
