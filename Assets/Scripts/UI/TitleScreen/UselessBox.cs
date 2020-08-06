using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessBox : MonoBehaviour
{
    public void OnBoxClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.UISound);
    }
}
