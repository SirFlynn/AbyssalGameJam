using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestScript : CharacterData
{
    public float holyRadiance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerData>().GetComponent<PlayerData>();
        fearTimer = fearCooldown;
        //fearMeterObject.transform.localScale = new Vector3(fearMeter,1,1);

        characterMovement = GetComponent<CharacterMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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




    }
}
