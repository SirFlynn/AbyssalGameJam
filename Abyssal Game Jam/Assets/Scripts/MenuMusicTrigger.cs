using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A function to start Menu music

public class MenuMusicTrigger : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMainMenuMusic();
    }
}
