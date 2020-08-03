using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject doorToOpen;

    public void PushButton()
    {
        // Play sound for opening door

        doorToOpen.SetActive(false);
    }

    public void UnpushButton()
    {
        // Play sound for closing door

        doorToOpen.SetActive(true);
    }
}
