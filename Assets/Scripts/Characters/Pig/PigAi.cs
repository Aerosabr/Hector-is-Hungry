using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PigAi : MonoBehaviour
{

    public Transform target;
    public NavMeshAgent agent;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }


}
