﻿using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Patrolling : MonoBehaviour
{

    [SerializeField]
    public List<Vector3> patrolpositions;
    private NavMeshAgent agent;

    private int patrolindex = 0;
    void Start()
    {
        agent = Util.AddOrGetComponent<NavMeshAgent>(gameObject);
        agent.destination = patrolpositions[0];
        agent.speed = 1;

        GetComponent<Zombie>().setAnimStat(Zombie.MoveStat.Walk);
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, patrolpositions[patrolindex]) < 0.1)
        {
            patrolindex += 1;
            patrolindex %= patrolpositions.Count; 
            GetComponent<NavMeshAgent>().destination = patrolpositions[patrolindex];
        }
    }
}
