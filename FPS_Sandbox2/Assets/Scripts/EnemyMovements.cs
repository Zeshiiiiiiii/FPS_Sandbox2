//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovements : MonoBehaviour
{
    public NavMeshAgent agent;

    private GameObject player;
    private GameObject ground;

    private float targetTime = 0.1f;

    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("updateTarget(player)", 0.2f, 7f);
        player = GameObject.FindWithTag("Player");
        //ground = GameObject.FindWithTag("Ground");
    }

    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 20)
        {
            passiveWandering();
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
        //Debug.Log(player.transform.localScale);
    }

    //different movement styles
    void passiveWandering()
    {
        targetTime -= Time.deltaTime;

        if (targetTime < 0.0f)
        {
            Vector3 targetPosition = new Vector3(Random.Range(-50, 51), Random.Range(-50, 51));
            agent.SetDestination(targetPosition);
            targetTime = 5f;
        }
    }
}
