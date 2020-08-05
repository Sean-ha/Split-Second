using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] doorsToOpen;

    public void PushButton()
    {
        foreach (GameObject door in doorsToOpen)
        {
            door.SetActive(false);
        }
    }

    public void UnpushButton()
    {
        foreach (GameObject door in doorsToOpen)
        {
            door.SetActive(true);
        }
    }
}
