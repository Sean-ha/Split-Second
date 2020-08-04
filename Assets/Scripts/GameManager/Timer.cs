using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private Vector3 initialPosition;
    private static int timerNum;

    private GameObject playerController;
    private ObjectManager objectManager;
    private OneSecondTimer oneSecondTimer;

    private void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        oneSecondTimer = FindObjectOfType<OneSecondTimer>();
        objectManager = FindObjectOfType<ObjectManager>();

        // Gets the starting position of the player. This is where they will reappear after 5 secs.
        playerController = FindObjectOfType<PlayerController>().gameObject;

        initialPosition = playerController.transform.position;

        NewLevelSetup();
    }

    private void CountDown()
    {
        if(timerNum == 6)
        {
            PlayerController.SetCanMove(true);
            CloneMovement.SetCanMove(true);

            // Set other obstacles can move here
            objectManager.ActivateObstacles();
        }

        timerNum--;
        timerText.text = timerNum.ToString();

        if(timerNum == 0)
        {
            RestartLevelNewClone();
        }
    }

    // Called when the 5 seconds are up. Moves the player back to the starting location and spawns a new clone
    // That copies the previous life's movement.
    public void RestartLevelNewClone()
    {
        CancelInvoke();

        CloneMovement[] clones = FindObjectsOfType<CloneMovement>();

        foreach(CloneMovement clone in clones)
        {
            clone.RestartClone(initialPosition);
        }

        playerController.GetComponent<PlayerController>().CreateClone(initialPosition);

        // Places player at initial position and reverts all velocity to zero
        playerController.GetComponent<PlayerController>().ResetPlayer(initialPosition);

        // Resets obstacle positions
        objectManager.ResetObstacles();

        NewLevelSetup();
    }

    // The second before this round begins
    private void NewLevelSetup()
    {
        oneSecondTimer.StartOneSecondTimer();

        timerNum = 6;
        timerText.text = "READY";

        PlayerController.SetCanMove(false);
        CloneMovement.SetCanMove(false);

        InvokeRepeating("CountDown", 1, 1);
    }

    public void StopTimer()
    {
        CancelInvoke();
    }
}
