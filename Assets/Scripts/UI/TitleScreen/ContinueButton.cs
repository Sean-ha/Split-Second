using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.GetInt("CurrentLevel", -1) != -1)
        {
            gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
