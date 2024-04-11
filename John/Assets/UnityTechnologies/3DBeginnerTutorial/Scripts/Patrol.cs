using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] points;
    
    private int _destPoint = 0;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(points[0].position);
    }

    
    private void Update()
    {
        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            _destPoint = (_destPoint + 1) % points.Length;
            _agent.SetDestination(points[_destPoint].position);
        }
    }
    
    
}
