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

    public float fearCooldown;
    protected float fearTimer;

    protected NavMeshAgent agent;
    protected CharacterMovement characterMovement;

    [System.NonSerialized] public Vector2 directionToPlayer;
    
    [SerializeField] protected float playerAwarenessDistance;

    protected PlayerData player;

    void Start()
    {
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
            //fearMeterObject.transform.localScale = new Vector3(fearMeter, 1, 1);
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
            characterMovement.MakeTarget(leavepointObject);
        }
    }
}
