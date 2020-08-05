using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Timer timer;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        gameObject.SetActive(false);
    }

    // Called when the restart button is pressed
    public void OnRestartButton()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);

        Time.timeScale = 1;
        // Destroys all clones
        CloneMovement[] allClones = FindObjectsOfType<CloneMovement>();

        foreach(CloneMovement clone in allClones)
        {
            Destroy(clone.gameObject);
        }
        PlayerController.isPaused = false;

        timer.RestartLevelNewClone(false);
        gameObject.SetActive(false);
    }
}
