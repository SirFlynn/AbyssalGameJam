using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestScript : CharacterData
{
    [SerializeField] GameObject relic;

    public float holyRadiance;
    public bool isExorcising;

    public float exorcising;
    private float timeExorcising;

    public float timeToSetUpRelics;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerData>().GetComponent<PlayerData>();
        fearTimer = fearCooldown;
        //fearMeterObject.transform.localScale = new Vector3(fearMeter,1,1);

        characterMovement = GetComponent<CharacterMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        timeExorcising = exorcising;
        characterMovement.waitInSpot = timeToSetUpRelics;
    }

    // Update is called once per frame
    void Update()
    {
        //placing crucifix in each room then going for relics
        //place in relics in Garage and grandpa then mother room
        //if see possess object, scared then move to exorcise 
        //has a raidance that affects haunting metre

        Vector2 enemyToPlayerVector = player.gameObject.transform.position - transform.position;
        directionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= holyRadiance)
        {
            player.RemoveFromHaunt(1);
        }

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance && player.isHaunting && isScared == false)
        {
            fearMeter -= 1;
            //fearMeterObject.transform.localScale = new Vector3(fearMeter, 1, 1);
            isScared = true;

            characterMovement.MakeTarget(player.possessedObject);
        }        

        if(isScared && Vector2.Distance(player.possessedObject.transform.position, transform.position) <= 1)
        {
            isExorcising = true;
            characterMovement.enabled = false;
        }

        if (isExorcising)
        {
            //Timer
            if (exorcising > 0)
            {
                exorcising -= Time.deltaTime;
            }
            else
            {
                characterMovement.ResetTarget();
                exorcising = timeExorcising;
                isScared = false;
                characterMovement.enabled = true;
            }
        }

        if (characterMovement.targets.Count == 0)
        {
            GameManager.Instance.GameOver(true);   
        }
        
    }

    public void PlaceRelic()
    {
        Instantiate(relic, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayHolySFX();
    }
}
