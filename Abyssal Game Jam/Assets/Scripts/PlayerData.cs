using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    PlayerMovement playerMovement;

    [System.NonSerialized] public int hauntingMeter;
    float cooldownTimer;
    [System.NonSerialized] public bool isHaunting = false;
    [System.NonSerialized] public GameObject possessedObject;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerMovement.isPossessing)
        {
            isHaunting = true;
        }
        else
        {
            isHaunting = false;
        }

        if (playerMovement.isPossessing)
        {
            possessedObject = playerMovement.possessedObject;
        }
        else
        {
            possessedObject = null;
        }
    }

    public void AddToHaunt(int value)
    {
        hauntingMeter += value;
    }

    public void RemoveFromHaunt(int value)
    {
        hauntingMeter -= value;
    }
}
