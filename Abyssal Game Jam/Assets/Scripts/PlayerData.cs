using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    PlayerMovement playerMovement;

    [System.NonSerialized] public int hauntingMeter;
    public float cooldownTimer = 10;
    float timer;
    [System.NonSerialized] public bool isHaunting = false;
    [System.NonSerialized] public GameObject possessedObject;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        timer = cooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerMovement.isPossessing)
        {
            isHaunting = true;
            playerMovement.animator.SetBool("IsHaunt", true);
        }
        else
        {
            isHaunting = false;
            if (playerMovement.animator != null)
            {
                playerMovement.animator.SetBool("IsHaunt", false);
            }
        }

        if (playerMovement.isPossessing)
        {
            possessedObject = playerMovement.possessedObject;
        }
        else
        {
            possessedObject = null;
        }

        //cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            AddToHaunt(1);
            cooldownTimer = timer;
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
