using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private int health = 100; //defining the Enemys health variable as well as setting it to 100
    public NavMeshAgent agent;//making the navmesh an agent that can be applied in engine
    public Transform target;//defining/declaring the target as a variable that can be referenced to in engine


    public Transform waypointParent;//setting the waypoint parent as something that can be referenced to in engine (use 'emptyGO' to make a parent and have waypoints inside
    private Transform[] waypoints;//sets up an index to cycle through the child 'empties'
    private int currentIndex = 1;//allows waypoints to be selected, determines wich waypoint is currently selected, ie setting the first waypoint to waypoint 1
    private float distanceToWaypoint = 1;//declares as a float that determines how close it needs to be to the waypoint before it moveds to the next one
    public bool pingPong = false;//a bool used to set the ai to then reverse through the index upon reaching the end
    public bool loop = false;// 

    public int Health
    {
        get
        {
            return health;
        }
    }

    private void Start()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        if (target)
        {// Update the AI's target position
            agent.SetDestination(target.position);
        }

        else
        {
            if (currentIndex >= waypoints.Length)
            {
                if (loop)
                {
                    currentIndex = 1;
                }

                else
                {
                    currentIndex = waypoints.Length - 1;

                    pingPong = true;
                }
            }
            if (currentIndex <= 0)
            {
                if (loop)
                {
                    currentIndex = waypoints.Length - 1;

                }
                else
                {
                    currentIndex = 1;

                    pingPong = false;
                }
            }

            Transform point = waypoints[currentIndex];

            float distance = Vector3.Distance(transform.position, point.position);
            if (distance <= distanceToWaypoint)
            {
                if (pingPong)
                {
                    currentIndex--;
                }
                else
                {
                    currentIndex++;
                }
            }

            agent.SetDestination(point.position);
        }

    }

    public void DealDamage(int damageDealt)
    {
        health -= damageDealt;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}