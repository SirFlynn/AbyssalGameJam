using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public float time;
    private float timeScore;

    public GameObject[] targets;
    private Vector3 currentTarget;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentTarget = targets[0].transform.position;

        timeScore = time;
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }

    void SetTargetPosition()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = currentTarget;

        //Checks the current position of the character with the target position
        //If both positions are the same, move to the next one in the array
        if (currentPos == targetPos)
        {
            //Timer
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                GameObject temp = targets[0];
                for (int i = 0; i < targets.Length - 1; i++)
                {
                    targets[i] = targets[i + 1];
                }
                targets[targets.Length - 1] = temp;

                currentTarget = targets[0].transform.position;

                time = timeScore;
            }            
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(currentTarget);
    }

}
