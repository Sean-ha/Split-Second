using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }
}
