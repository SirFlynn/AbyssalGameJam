using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChecker : MonoBehaviour
{
    //Runs when the player moves into this trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlayGrandPiano();
        }
    }
}
