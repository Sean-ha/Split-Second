using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] allObstacles;
    private Vector3[] obstaclesInitialPositions;

    private MovingPlatform[] allMovingPlatforms;

    private void Start()
    {
        allMovingPlatforms = FindObjectsOfType<MovingPlatform>();

        obstaclesInitialPositions = new Vector3[allObstacles.Length];

        // Acquires all initial positions of obstacles at level start.
        for(int i = 0; i < allObstacles.Length; i++)
        {
            obstaclesInitialPositions[i] = allObstacles[i].transform.position;
        }
    }

    // Called after the 5 second timer finishes. Resets all obstacles to their original positions
    public void ResetObstacles()
    {
        for (int i = 0; i < allObstacles.Length; i++)
        {
            allObstacles[i].SetActive(true);
            allObstacles[i].transform.position = obstaclesInitialPositions[i];
        }

        foreach(MovingPlatform platform in allMovingPlatforms)
        {
            platform.SetCanMove(false);
        }
    }

    public void ActivateObstacles()
    {
        foreach (MovingPlatform platform in allMovingPlatforms)
        {
            platform.SetCanMove(true);
        }
    }
}
