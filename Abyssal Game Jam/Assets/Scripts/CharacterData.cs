using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterData : MonoBehaviour
{
    public int fearMeter;
    public int leaveValue;
    public GameObject fearMeterObject;
    public GameObject leavepointObject;
    [System.NonSerialized] public bool isScared = false;

    public MetresScript fearMetreGUI;

    public float fearCooldown;
    protected float fearTimer;

    protected NavMeshAgent agent;
    protected CharacterMovement characterMovement;

    [System.NonSerialized] public Vector2 directionToPlayer;
    
    [SerializeField] protected float playerAwarenessDistance;

    protected PlayerData player;

    void Start()
    {
        fearMeter = 0;

        player = FindAnyObjectByType<PlayerData>().GetComponent<PlayerData>();
        fearTimer = fearCooldown;
        //fearMeterObject.transform.localScale = new Vector3(fearMeter,1,1);

        characterMovement = GetComponent<CharacterMovement>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector2 enemyToPlayerVector = player.gameObject.transform.position - transform.position;
        directionToPlayer = enemyToPlayerVector.normalized;

        if(enemyToPlayerVector.magnitude <= playerAwarenessDistance && player.isHaunting && isScared == false)
        {
            fearMeter += 1;
            float FearValue = fearMeter / leaveValue;
            fearMetreGUI.SetBars(FearValue);

            isScared = true;
            AudioManager.Instance.PlayRunAway();
            characterMovement.GoToNextSpot();
        }

        //cooldown timer
        if (fearCooldown > 0 && isScared == true)
        {
            fearCooldown -= Time.deltaTime;
        }
        else
        {
            fearCooldown = fearTimer;
            isScared = false;
        }

        if(fearMeter == leaveValue)
        {
            //turns off GUI for fear metre
            fearMetreGUI.gameObject.SetActive(false);
            characterMovement.MakeTarget(leavepointObject);
        }
    }
}
