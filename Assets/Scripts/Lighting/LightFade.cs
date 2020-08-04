using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    private Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(Fader());
    }

    IEnumerator Fader()
    {
        float time = 0.75f;

        while(time > 0)
        {
            light2D.intensity = (time / 0.75f) * .2f;
            time -= Time.deltaTime;

            yield return null;
        }
    }
}
