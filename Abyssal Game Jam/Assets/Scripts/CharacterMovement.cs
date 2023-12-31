using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    public float waitInSpot;
    private float timeScore;

    CharacterData characterData;

    public List<GameObject> targets;
    private Vector3 currentTarget;
    NavMeshAgent agent;
    float orignalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentTarget = targets[0].transform.position;

        characterData = GetComponent<CharacterData>();

        timeScore = waitInSpot;

        orignalSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (characterData.isScared)
        {
            agent.speed *= 2;
        }
        else
        {
            agent.speed = orignalSpeed;
        }

        RotateTowardsTarget();
        SetTargetPosition();
        SetAgentPosition();
    }

    void RotateTowardsTarget()
    {
        if(agent.destination == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, agent.destination);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, orignalSpeed * Time.deltaTime);

        transform.rotation = rotation;
    }

    void SetTargetPosition()
    {
        

        Vector2 currentPos = transform.position;
        Vector2 targetPos = new Vector2((int)currentTarget.x, (int)currentTarget.y) ;

        //Checks the current position of the character with the target position
        //If both positions are the same, move to the next one in the array
        bool _x = Mathf.Approximately(currentPos.x,targetPos.x);
        bool _y = Mathf.Approximately(currentPos.y, targetPos.y);

        bool _X = (int)currentPos.x == (int)targetPos.x;
        bool _Y = (int)currentPos.y == (int)targetPos.y;
        if ((_x && _y)||(_X && _Y))
        {
            //Timer
            if (waitInSpot > 0)
            {
                waitInSpot -= Time.deltaTime;
            }
            
            else if (characterData.GetType() == typeof(PriestScript))
            {
                PriestScript priest = (PriestScript)characterData;
                GoToNextSpot();
                priest.PlaceRelic();
                if (targets.Count != 0)
                {
                    targets.RemoveAt(targets.Count - 1);
                }                
            }

            else if (characterData.fearMeter == characterData.leaveValue)
            {
                gameObject.SetActive(false);
            }

            else
            {
                GoToNextSpot();

                waitInSpot = timeScore;
            }            
        }
    }

    public void MakeTarget(GameObject go)
    {
        currentTarget = go.transform.position;
    }

    public void ResetTarget()
    {
        currentTarget = targets[0].transform.position;
    }

    public void GoToNextSpot()
    {
        if (targets.Count > 1)
        {
            GameObject temp = targets[0];
            for (int i = 0; i < targets.Count - 1; i++)
            {
                targets[i] = targets[i + 1];
            }
            targets[targets.Count - 1] = temp;

            currentTarget = targets[0].transform.position;
        }
        
    }

    public void RemoveAllTargets()
    {
        targets.Clear();
    }

    void SetAgentPosition()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(currentTarget);
        }        
    }

    

}
