using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PostProcessing : MonoBehaviour
{
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    void Start()
    {
        chromaticAberration = GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>();
        lensDistortion = GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>();

        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.StartLevel);
        }

        StartCoroutine(DistortLensEntrance2(.5f));
    }

    public void BendScreen(float duration)
    {
        StartCoroutine(Bend(duration));
    }

    private IEnumerator Bend(float duration)
    {
        float time = 0;
        float halfDuration = duration / 2;

        while(time < halfDuration)
        {
            chromaticAberration.intensity.value = time / halfDuration;
            time += Time.deltaTime;
            yield return null;
        }

        while(time > 0)
        {
            chromaticAberration.intensity.value = time / halfDuration;
            time -= Time.deltaTime;
            yield return null;
        }
    }

    public void LensDistortion(float duration)
    {
        SoundManager.PlaySound(SoundManager.Sound.ExitLevel);
        FindObjectOfType<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        StartCoroutine(DistortLensExit(duration));
    }

    public void LensDistortionMainMenu(float duration)
    {
        SoundManager.PlaySound(SoundManager.Sound.ExitLevel);
        StartCoroutine(DistortLensExit(duration));
        //StartCoroutine(MainMenuFade());
    }

    private IEnumerator DistortLensExit(float duration)
    {
        float time = 0;
        float distortionAmount = -35f;

        while (time < duration)
        {
            distortionAmount -= (time / duration) * 65;
            lensDistortion.intensity.value = distortionAmount;

            lensDistortion.scale.value = 1 - (time / duration);

            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DistortLensEntrance(float duration)
    {
        float time = 0;
        float distortionAmount = -100f;
        float scaleAmount = .01f;

        lensDistortion.scale.value = scaleAmount;
        lensDistortion.intensity.value = distortionAmount;

        while (time < duration)
        {
            lensDistortion.intensity.value = distortionAmount + ((time / duration) * 65);

            lensDistortion.scale.value = scaleAmount + (time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        lensDistortion.intensity.value = -35f;
        lensDistortion.scale.value = 1f;
    }

    IEnumerator DistortLensEntrance2(float duration)
    {
        float time = duration;
        float distortionAmount = -35f;

        while (time > 0)
        {
            lensDistortion.intensity.value = distortionAmount - (time / duration) * 65; ;

            lensDistortion.scale.value = 1 - (time / duration);

            time -= Time.deltaTime;
            yield return null;
        }

        lensDistortion.intensity.value = -35f;
        lensDistortion.scale.value = 1f;
    }

}
