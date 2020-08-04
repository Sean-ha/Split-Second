using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] doorsToOpen;

    public void PushButton()
    {
        // Play sound for opening door

        foreach(GameObject door in doorsToOpen)
        {
            door.SetActive(false);
        }
    }

    public void UnpushButton()
    {
        // Play sound for closing door

        foreach(GameObject door in doorsToOpen)
        {
            door.SetActive(true);
        }
    }
}
